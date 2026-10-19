using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using TP1_LSDA.Repositories;

namespace TP1_LSDA.UI
{
    /// <summary>
    /// Onglet Personnages : CRUD en mode déconnecté.
    /// DataSet (SqlDataAdapter.Fill) → BindingSource → DataGridView + zones d'édition,
    /// puis SqlCommandBuilder + SqlDataAdapter.Update() lors de la sauvegarde (via PersonnageRepository).
    /// </summary>
    public partial class FrmPersonnages : Form
    {
        private readonly PersonnageRepository _repository;
        private DataSet _dataSet;

        public FrmPersonnages(PersonnageRepository repository)
        {
            InitializeComponent();
            _repository = repository;
        }

        private DataTable Table => _dataSet?.Tables[_repository.TableName];

        public bool ADesModificationsNonSauvegardees
        {
            get
            {
                if (Table == null) return false;
                bsPersonnages.EndEdit();
                return Table.GetChanges() != null;
            }
        }

        private void FrmPersonnages_Load(object sender, EventArgs e)
        {
            ChargerDonnees();
        }

        public void RechargerSiAucuneModification()
        {
            if (!ADesModificationsNonSauvegardees) ChargerDonnees();
        }

        private void ChargerDonnees(int? idASelectionner = null)
        {
            try
            {
                _dataSet = _repository.GetDataSet();

                DataTable table = Table;
                table.Columns["Nom"].DefaultValue = string.Empty;
                table.Columns["Race"].DefaultValue = string.Empty;
                table.Columns["Faction"].DefaultValue = string.Empty;
                table.RowChanged += (s, ev) => MettreAJourStatut();
                table.RowDeleted += (s, ev) => MettreAJourStatut();

                LierControles();
                SelectionnerLigne(idASelectionner);
                MettreAJourStatut();
            }
            catch (Exception ex)
            {
                UiHelper.Erreur(ex, "Impossible de charger les personnages.");
            }
        }

        private void LierControles()
        {
            txtNom.DataBindings.Clear();
            txtRace.DataBindings.Clear();
            txtFaction.DataBindings.Clear();

            bsPersonnages.DataSource = _dataSet;
            bsPersonnages.DataMember = _repository.TableName;
            bsPersonnages.Sort = "Nom ASC";
            dgvPersonnages.DataSource = bsPersonnages;

            txtNom.DataBindings.Add("Text", bsPersonnages, "Nom", true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
            txtRace.DataBindings.Add("Text", bsPersonnages, "Race", true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
            txtFaction.DataBindings.Add("Text", bsPersonnages, "Faction", true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);

            if (dgvPersonnages.Columns.Count > 0)
            {
                dgvPersonnages.Columns["Id"].HeaderText = "Id";
                dgvPersonnages.Columns["Id"].Width = 60;
                dgvPersonnages.Columns["Nom"].HeaderText = "Nom";
                dgvPersonnages.Columns["Nom"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvPersonnages.Columns["Race"].HeaderText = "Race";
                dgvPersonnages.Columns["Race"].Width = 150;
                dgvPersonnages.Columns["Faction"].HeaderText = "Faction";
                dgvPersonnages.Columns["Faction"].Width = 230;
            }
        }

        private void SelectionnerLigne(int? id)
        {
            if (id == null) return;
            int index = bsPersonnages.Find("Id", id.Value);
            if (index >= 0) bsPersonnages.Position = index;
        }

        private void MettreAJourStatut()
        {
            if (Table == null) return;
            int nb = Table.Select(null, null, DataViewRowState.CurrentRows).Length;
            DataTable changes = Table.GetChanges();
            lblStatut.Text = changes == null
                ? $"{nb} personnage(s) — aucune modification en attente."
                : $"{nb} personnage(s) — {changes.Rows.Count} modification(s) en attente : cliquez sur « Sauvegarder ».";
        }

        // =====================================================================
        //  Boutons
        // =====================================================================

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            if (Table == null) return;
            bsPersonnages.EndEdit();
            bsPersonnages.AddNew();
            txtNom.Focus();
            lblStatut.Text = "Nouveau personnage : remplissez les champs puis cliquez sur « Sauvegarder ».";
        }

        private void btnSauvegarder_Click(object sender, EventArgs e)
        {
            Sauvegarder(afficherConfirmation: true);
        }

        private bool Sauvegarder(bool afficherConfirmation)
        {
            if (Table == null) return false;

            try
            {
                bsPersonnages.EndEdit();
            }
            catch (Exception ex)
            {
                UiHelper.Avertissement("Valeur invalide : " + ex.Message);
                return false;
            }

            if (Table.GetChanges() == null)
            {
                if (afficherConfirmation) UiHelper.Info("Aucune modification à sauvegarder.");
                return true;
            }

            List<string> erreurs = Valider();
            if (erreurs.Count > 0)
            {
                UiHelper.Avertissement("Impossible de sauvegarder :\n\n• " + string.Join("\n• ", erreurs));
                return false;
            }

            int? idCourant = (bsPersonnages.Current as DataRowView)?.Row.RowState == DataRowState.Added
                ? (int?)null
                : (bsPersonnages.Current as DataRowView)?["Id"] as int?;

            try
            {
                int nb = _repository.SaveChanges(_dataSet);
                ChargerDonnees(idCourant);
                if (afficherConfirmation) UiHelper.Info($"{nb} modification(s) enregistrée(s) dans la base de données.", "Sauvegarde");
                return true;
            }
            catch (DBConcurrencyException ex)
            {
                UiHelper.Erreur(ex, "La sauvegarde a échoué.");
                ChargerDonnees();
                return false;
            }
            catch (Exception ex)
            {
                UiHelper.Erreur(ex, "La sauvegarde a échoué. Aucune modification n'a été enregistrée.");
                return false;
            }
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (!(bsPersonnages.Current is DataRowView vue))
            {
                UiHelper.Avertissement("Sélectionnez d'abord un personnage à supprimer.");
                return;
            }

            DataRow ligne = vue.Row;
            string nom = Convert.ToString(vue["Nom"]);

            if (ligne.RowState == DataRowState.Added)
            {
                // Ligne jamais enregistrée : on l'abandonne simplement.
                bsPersonnages.CancelEdit();
                if (ligne.RowState != DataRowState.Detached) bsPersonnages.RemoveCurrent();
                MettreAJourStatut();
                return;
            }

            if (!UiHelper.Confirmer($"Supprimer définitivement le personnage « {nom} » ?\n\n" +
                                    "Toutes les modifications en attente de cet onglet seront aussi enregistrées."))
                return;

            bsPersonnages.EndEdit();
            ligne.Delete();

            if (!Sauvegarder(afficherConfirmation: false))
            {
                // Échec (ex. : personnage affecté à une mission) : on annule la suppression de cette ligne.
                if (ligne.RowState == DataRowState.Deleted) ligne.RejectChanges();
                MettreAJourStatut();
                return;
            }

            UiHelper.Info($"Le personnage « {nom} » a été supprimé.", "Suppression");
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            if (ADesModificationsNonSauvegardees &&
                !UiHelper.Confirmer("Annuler toutes les modifications non sauvegardées et recharger les personnages ?"))
                return;

            bsPersonnages.CancelEdit();
            ChargerDonnees();
        }

        // =====================================================================
        //  Validation
        // =====================================================================

        private List<string> Valider()
        {
            var erreurs = new List<string>();
            var noms = new HashSet<string>(StringComparer.CurrentCultureIgnoreCase);

            foreach (DataRow row in Table.Rows)
            {
                if (row.RowState == DataRowState.Deleted) continue;

                string nom = Convert.ToString(row["Nom"]).Trim();
                string race = Convert.ToString(row["Race"]).Trim();
                string libelle = string.IsNullOrEmpty(nom) ? "(personnage sans nom)" : $"« {nom} »";

                if (nom.Length == 0) erreurs.Add("Le nom du personnage est obligatoire.");
                else if (nom.Length > 100) erreurs.Add($"{libelle} : le nom ne doit pas dépasser 100 caractères.");
                else if (!noms.Add(nom)) erreurs.Add($"{libelle} : ce nom de personnage est en double.");

                if (race.Length == 0) erreurs.Add($"{libelle} : la race est obligatoire.");
                else if (race.Length > 50) erreurs.Add($"{libelle} : la race ne doit pas dépasser 50 caractères.");

                string faction = Convert.ToString(row["Faction"]).Trim();
                if (faction.Length == 0) erreurs.Add($"{libelle} : la faction est obligatoire.");
                else if (faction.Length > 50) erreurs.Add($"{libelle} : la faction ne doit pas dépasser 50 caractères.");

                // Normalisation : on enregistre les valeurs sans espaces superflus.
                if (row.RowState != DataRowState.Unchanged)
                {
                    if (!Equals(row["Nom"], nom)) row["Nom"] = nom;
                    if (!Equals(row["Race"], race)) row["Race"] = race;
                    if (!Equals(row["Faction"], faction)) row["Faction"] = faction;
                }
            }

            return erreurs;
        }

        private void dgvPersonnages_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            UiHelper.Avertissement("Valeur invalide : " + e.Exception?.Message);
            e.ThrowException = false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using TP1_LSDA.Repositories;

namespace TP1_LSDA.UI
{
    /// <summary>
    /// Onglet Lieux : CRUD en mode déconnecté.
    /// DataSet (SqlDataAdapter.Fill) → BindingSource → DataGridView + zones d'édition,
    /// puis SqlCommandBuilder + SqlDataAdapter.Update() lors de la sauvegarde (via LieuRepository).
    /// </summary>
    public partial class FrmLieux : Form
    {
        private const int DangerositeMin = 1;
        private const int DangerositeMax = 10;

        private readonly LieuRepository _repository;
        private DataSet _dataSet;

        public FrmLieux(LieuRepository repository)
        {
            InitializeComponent();
            _repository = repository;
            nudDangerosite.Minimum = DangerositeMin;
            nudDangerosite.Maximum = DangerositeMax;
        }

        private DataTable Table => _dataSet?.Tables[_repository.TableName];

        public bool ADesModificationsNonSauvegardees
        {
            get
            {
                if (Table == null) return false;
                bsLieux.EndEdit();
                return Table.GetChanges() != null;
            }
        }

        private void FrmLieux_Load(object sender, EventArgs e)
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
                table.Columns["NomLieu"].DefaultValue = string.Empty;
                table.Columns["Royaume"].DefaultValue = string.Empty;
                table.Columns["Dangerosite"].DefaultValue = DangerositeMin;
                table.RowChanged += (s, ev) => MettreAJourStatut();
                table.RowDeleted += (s, ev) => MettreAJourStatut();

                LierControles();
                SelectionnerLigne(idASelectionner);
                MettreAJourStatut();
            }
            catch (Exception ex)
            {
                UiHelper.Erreur(ex, "Impossible de charger les lieux.");
            }
        }

        private void LierControles()
        {
            txtNomLieu.DataBindings.Clear();
            txtRoyaume.DataBindings.Clear();
            nudDangerosite.DataBindings.Clear();

            bsLieux.DataSource = _dataSet;
            bsLieux.DataMember = _repository.TableName;
            bsLieux.Sort = "NomLieu ASC";
            dgvLieux.DataSource = bsLieux;

            txtNomLieu.DataBindings.Add("Text", bsLieux, "NomLieu", true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
            txtRoyaume.DataBindings.Add("Text", bsLieux, "Royaume", true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
            nudDangerosite.DataBindings.Add("Value", bsLieux, "Dangerosite", true, DataSourceUpdateMode.OnPropertyChanged, DangerositeMin);

            if (dgvLieux.Columns.Count > 0)
            {
                dgvLieux.Columns["Id"].HeaderText = "Id";
                dgvLieux.Columns["Id"].Width = 60;
                dgvLieux.Columns["NomLieu"].HeaderText = "Nom du lieu";
                dgvLieux.Columns["NomLieu"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvLieux.Columns["Royaume"].HeaderText = "Royaume";
                dgvLieux.Columns["Royaume"].Width = 180;
                dgvLieux.Columns["Dangerosite"].HeaderText = "Dangerosité (1-10)";
                dgvLieux.Columns["Dangerosite"].Width = 150;
            }
        }

        private void SelectionnerLigne(int? id)
        {
            if (id == null) return;
            int index = bsLieux.Find("Id", id.Value);
            if (index >= 0) bsLieux.Position = index;
        }

        private void MettreAJourStatut()
        {
            if (Table == null) return;
            int nb = Table.Select(null, null, DataViewRowState.CurrentRows).Length;
            DataTable changes = Table.GetChanges();
            lblStatut.Text = changes == null
                ? $"{nb} lieu(x) — aucune modification en attente."
                : $"{nb} lieu(x) — {changes.Rows.Count} modification(s) en attente : cliquez sur « Sauvegarder ».";
        }

        // =====================================================================
        //  Boutons
        // =====================================================================

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            if (Table == null) return;
            bsLieux.EndEdit();
            bsLieux.AddNew();
            txtNomLieu.Focus();
            lblStatut.Text = "Nouveau lieu : remplissez les champs puis cliquez sur « Sauvegarder ».";
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
                bsLieux.EndEdit();
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

            int? idCourant = (bsLieux.Current as DataRowView)?.Row.RowState == DataRowState.Added
                ? (int?)null
                : (bsLieux.Current as DataRowView)?["Id"] as int?;

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
            if (!(bsLieux.Current is DataRowView vue))
            {
                UiHelper.Avertissement("Sélectionnez d'abord un lieu à supprimer.");
                return;
            }

            DataRow ligne = vue.Row;
            string nom = Convert.ToString(vue["NomLieu"]);

            if (ligne.RowState == DataRowState.Added)
            {
                // Ligne jamais enregistrée : on l'abandonne simplement.
                bsLieux.CancelEdit();
                if (ligne.RowState != DataRowState.Detached) bsLieux.RemoveCurrent();
                MettreAJourStatut();
                return;
            }

            if (!UiHelper.Confirmer($"Supprimer définitivement le lieu « {nom} » ?\n\n" +
                                    "Toutes les modifications en attente de cet onglet seront aussi enregistrées."))
                return;

            bsLieux.EndEdit();
            ligne.Delete();

            if (!Sauvegarder(afficherConfirmation: false))
            {
                // Échec (ex. : lieu utilisé par une mission) : on annule la suppression de cette ligne.
                if (ligne.RowState == DataRowState.Deleted) ligne.RejectChanges();
                MettreAJourStatut();
                return;
            }

            UiHelper.Info($"Le lieu « {nom} » a été supprimé.", "Suppression");
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            if (ADesModificationsNonSauvegardees &&
                !UiHelper.Confirmer("Annuler toutes les modifications non sauvegardées et recharger les lieux ?"))
                return;

            bsLieux.CancelEdit();
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

                string nom = Convert.ToString(row["NomLieu"]).Trim();
                string royaume = Convert.ToString(row["Royaume"]).Trim();
                string libelle = string.IsNullOrEmpty(nom) ? "(lieu sans nom)" : $"« {nom} »";

                if (nom.Length == 0) erreurs.Add("Le nom du lieu est obligatoire.");
                else if (nom.Length > 100) erreurs.Add($"{libelle} : le nom ne doit pas dépasser 100 caractères.");
                else if (!noms.Add(nom)) erreurs.Add($"{libelle} : ce nom de lieu est en double.");

                if (royaume.Length == 0) erreurs.Add($"{libelle} : le royaume est obligatoire.");
                else if (royaume.Length > 50) erreurs.Add($"{libelle} : le royaume ne doit pas dépasser 50 caractères.");

                if (row["Dangerosite"] == DBNull.Value ||
                    !int.TryParse(Convert.ToString(row["Dangerosite"]), out int danger) ||
                    danger < DangerositeMin || danger > DangerositeMax)
                {
                    erreurs.Add($"{libelle} : la dangerosité doit être un entier entre {DangerositeMin} et {DangerositeMax}.");
                }

                // Normalisation : on enregistre les valeurs sans espaces superflus.
                if (row.RowState != DataRowState.Unchanged)
                {
                    if (!Equals(row["NomLieu"], nom)) row["NomLieu"] = nom;
                    if (!Equals(row["Royaume"], royaume)) row["Royaume"] = royaume;
                }
            }

            return erreurs;
        }

        private void dgvLieux_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            UiHelper.Avertissement("Valeur invalide : " + e.Exception?.Message);
            e.ThrowException = false;
        }
    }
}

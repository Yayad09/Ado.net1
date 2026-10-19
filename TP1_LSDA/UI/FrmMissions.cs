using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TP1_LSDA.Domain;
using TP1_LSDA.Repositories;

namespace TP1_LSDA.UI
{
    /// <summary>
    /// Onglet Missions :
    ///  • création d'une mission avec vérification de conflit en mode connecté (SqlDataReader);
    ///  • recherche filtrée en mode connecté (requêtes paramétrées);
    ///  • liste globale en mode déconnecté (DataSet + jointure Lieu/Personnage).
    /// </summary>
    public partial class FrmMissions : Form
    {
        private const string FormatDate = "yyyy-MM-dd HH:mm";
        private const int DureeMaximaleJours = 365;

        private enum TypeRecherche
        {
            MissionsParPeriode,
            LieuxParRoyaume,
            PersonnagesParRace,
            LieuxDisponibles
        }

        private readonly MissionRepository _missionRepository;
        private readonly LieuRepository _lieuRepository;
        private readonly PersonnageRepository _personnageRepository;
        private DataSet _dsMissions;
        private bool _chargementEnCours;

        public FrmMissions(MissionRepository missionRepository, LieuRepository lieuRepository,
                           PersonnageRepository personnageRepository)
        {
            InitializeComponent();
            _missionRepository = missionRepository;
            _lieuRepository = lieuRepository;
            _personnageRepository = personnageRepository;
        }

        private void FrmMissions_Load(object sender, EventArgs e)
        {
            foreach (DateTimePicker dtp in new[] { dtpDebut, dtpFin, dtpRechDebut, dtpRechFin })
            {
                dtp.Format = DateTimePickerFormat.Custom;
                dtp.CustomFormat = FormatDate;
            }

            DateTime prochaineHeure = DateTime.Today.AddHours(DateTime.Now.Hour + 1);
            dtpDebut.Value = prochaineHeure;
            dtpFin.Value = prochaineHeure.AddHours(2);
            dtpRechDebut.Value = DateTime.Today;
            dtpRechFin.Value = DateTime.Today.AddMonths(2);

            cbTypeRecherche.DisplayMember = "Value";
            cbTypeRecherche.ValueMember = "Key";
            cbTypeRecherche.DataSource = new List<KeyValuePair<TypeRecherche, string>>
            {
                new KeyValuePair<TypeRecherche, string>(TypeRecherche.MissionsParPeriode, "Missions par période"),
                new KeyValuePair<TypeRecherche, string>(TypeRecherche.LieuxParRoyaume, "Lieux par royaume"),
                new KeyValuePair<TypeRecherche, string>(TypeRecherche.PersonnagesParRace, "Personnages par race"),
                new KeyValuePair<TypeRecherche, string>(TypeRecherche.LieuxDisponibles, "Lieux disponibles sur une période")
            };

            RafraichirDonnees();
        }

        /// <summary>Recharge les combos (lieux/personnages) et la liste globale des missions.</summary>
        public void RafraichirDonnees()
        {
            if (!IsHandleCreated) return;
            ChargerCombos();
            ChargerMissions();
            MettreAJourFiltres();
        }

        // =====================================================================
        //  Chargements
        // =====================================================================

        private void ChargerCombos()
        {
            try
            {
                _chargementEnCours = true;
                object lieuSelectionne = cbLieu.SelectedValue;
                object personnageSelectionne = cbPersonnage.SelectedValue;

                List<Lieu> lieux = _lieuRepository.GetAll();
                cbLieu.DataSource = null;
                cbLieu.ValueMember = nameof(Lieu.Id);
                cbLieu.DataSource = lieux;

                List<Personnage> personnages = _personnageRepository.GetAll();
                cbPersonnage.DataSource = null;
                cbPersonnage.ValueMember = nameof(Personnage.Id);
                cbPersonnage.DataSource = personnages;

                if (lieuSelectionne != null && lieux.Any(l => Equals(l.Id, lieuSelectionne))) cbLieu.SelectedValue = lieuSelectionne;
                else cbLieu.SelectedIndex = -1;

                if (personnageSelectionne != null && personnages.Any(p => Equals(p.Id, personnageSelectionne))) cbPersonnage.SelectedValue = personnageSelectionne;
                else cbPersonnage.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                UiHelper.Erreur(ex, "Impossible de charger les lieux et les personnages.");
            }
            finally
            {
                _chargementEnCours = false;
            }
        }

        /// <summary>Liste globale en mode déconnecté : DataSet rempli par SqlDataAdapter (jointure).</summary>
        private void ChargerMissions(int? idASelectionner = null)
        {
            try
            {
                _dsMissions = _missionRepository.GetMissionsDetailDataSet();
                dgvMissions.DataSource = _dsMissions.Tables["MissionsDetail"];

                FormaterColonne(dgvMissions, "Id", "Id", 60);
                FormaterColonne(dgvMissions, "NomLieu", "Lieu", 0);
                FormaterColonne(dgvMissions, "NomPersonnage", "Personnage", 0);
                FormaterColonne(dgvMissions, "DateDebut", "Date de début", 170);
                FormaterColonne(dgvMissions, "DateFin", "Date de fin", 170);
                MasquerColonne(dgvMissions, "LieuId");
                MasquerColonne(dgvMissions, "PersonnageId");

                lblNbMissions.Text = $"{_dsMissions.Tables["MissionsDetail"].Rows.Count} mission(s) planifiée(s).";

                if (idASelectionner != null)
                {
                    foreach (DataGridViewRow row in dgvMissions.Rows)
                    {
                        if (Equals(row.Cells["Id"].Value, idASelectionner.Value))
                        {
                            row.Selected = true;
                            dgvMissions.CurrentCell = row.Cells["NomLieu"];
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UiHelper.Erreur(ex, "Impossible de charger la liste des missions.");
            }
        }

        private static void FormaterColonne(DataGridView dgv, string nom, string entete, int largeur)
        {
            DataGridViewColumn col = dgv.Columns[nom];
            if (col == null) return;

            col.HeaderText = entete;
            if (largeur > 0) col.Width = largeur;
            else col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            if (col.ValueType == typeof(DateTime)) col.DefaultCellStyle.Format = FormatDate;
        }

        private static void MasquerColonne(DataGridView dgv, string nom)
        {
            if (dgv.Columns[nom] != null) dgv.Columns[nom].Visible = false;
        }

        // =====================================================================
        //  Création de mission + vérification de conflit (mode connecté)
        // =====================================================================

        private static DateTime TronquerALaMinute(DateTime d) =>
            new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, 0);

        /// <summary>Valide la saisie. Retourne null si tout est correct, sinon la mission n'est pas construite.</summary>
        private Mission LireSaisie()
        {
            var erreurs = new List<string>();

            if (!(cbLieu.SelectedItem is Lieu lieu)) { erreurs.Add("Veuillez choisir un lieu."); lieu = null; }
            if (!(cbPersonnage.SelectedItem is Personnage personnage)) { erreurs.Add("Veuillez choisir un personnage."); personnage = null; }

            DateTime debut = TronquerALaMinute(dtpDebut.Value);
            DateTime fin = TronquerALaMinute(dtpFin.Value);

            if (fin <= debut)
                erreurs.Add("La date de fin doit être strictement postérieure à la date de début.");
            else if ((fin - debut).TotalDays > DureeMaximaleJours)
                erreurs.Add($"Une mission ne peut pas durer plus de {DureeMaximaleJours} jours.");

            if (erreurs.Count > 0)
            {
                UiHelper.Avertissement("Saisie invalide :\n\n• " + string.Join("\n• ", erreurs));
                return null;
            }

            return new Mission
            {
                LieuId = lieu.Id,
                PersonnageId = personnage.Id,
                DateDebut = debut,
                DateFin = fin,
                NomLieu = lieu.NomLieu,
                NomPersonnage = personnage.Nom
            };
        }

        /// <summary>Vérification de conflit en mode connecté (requête paramétrée + SqlDataReader).</summary>
        private bool VerifierConflit(Mission mission, bool afficherSiLibre)
        {
            List<Mission> conflits = _missionRepository.TrouverConflits(mission.LieuId, mission.DateDebut, mission.DateFin);

            if (conflits.Count == 0)
            {
                lblResultatConflit.ForeColor = Color.DarkGreen;
                lblResultatConflit.Text = $"✔ {mission.NomLieu} est libre du {mission.DateDebut.ToString(FormatDate)} au {mission.DateFin.ToString(FormatDate)}.";
                if (afficherSiLibre) UiHelper.Info("Aucun conflit : le lieu est disponible sur cette plage.", "Vérification de conflit");
                return false;
            }

            lblResultatConflit.ForeColor = Color.DarkRed;
            lblResultatConflit.Text = $"✖ Conflit avec {conflits.Count} mission(s) existante(s) à {mission.NomLieu}.";

            string details = string.Join("\n", conflits.Select(c =>
                $"• Mission n° {c.Id} — {c.NomPersonnage} : du {c.DateDebut.ToString(FormatDate)} au {c.DateFin.ToString(FormatDate)}"));

            UiHelper.Avertissement(
                $"Impossible de planifier cette mission : le lieu « {mission.NomLieu} » est déjà réservé " +
                $"sur une plage qui chevauche celle demandée ({mission.DateDebut.ToString(FormatDate)} → {mission.DateFin.ToString(FormatDate)}).\n\n" +
                details + "\n\nChoisissez une autre plage horaire ou un autre lieu.",
                "Conflit de réservation");
            return true;
        }

        private void btnVerifierConflit_Click(object sender, EventArgs e)
        {
            Mission mission = LireSaisie();
            if (mission == null) return;

            try
            {
                VerifierConflit(mission, afficherSiLibre: true);
            }
            catch (Exception ex)
            {
                UiHelper.Erreur(ex, "La vérification de conflit a échoué.");
            }
        }

        private void btnCreerMission_Click(object sender, EventArgs e)
        {
            Mission mission = LireSaisie();
            if (mission == null) return;

            if (mission.DateDebut < DateTime.Now &&
                !UiHelper.Confirmer("La date de début est déjà passée. Créer quand même cette mission ?"))
                return;

            try
            {
                // 1) Vérification du conflit AVANT l'insertion (mode connecté).
                if (VerifierConflit(mission, afficherSiLibre: false)) return;

                // 2) Insertion (requête paramétrée).
                int id = _missionRepository.Add(mission);

                ChargerMissions(id);
                UiHelper.Info($"Mission n° {id} créée : {mission.NomPersonnage} à {mission.NomLieu}\n" +
                              $"du {mission.DateDebut.ToString(FormatDate)} au {mission.DateFin.ToString(FormatDate)}.",
                              "Mission créée");
            }
            catch (Exception ex)
            {
                UiHelper.Erreur(ex, "La mission n'a pas pu être créée.");
                ChargerMissions();
            }
        }

        private void SaisieMission_Changed(object sender, EventArgs e)
        {
            if (_chargementEnCours) return;
            lblResultatConflit.Text = string.Empty;
        }

        // =====================================================================
        //  Suppression d'une mission
        // =====================================================================

        private void btnSupprimerMission_Click(object sender, EventArgs e)
        {
            if (dgvMissions.CurrentRow == null || dgvMissions.CurrentRow.Cells["Id"].Value == null)
            {
                UiHelper.Avertissement("Sélectionnez d'abord une mission dans la liste.");
                return;
            }

            DataGridViewRow row = dgvMissions.CurrentRow;
            int id = Convert.ToInt32(row.Cells["Id"].Value);
            string resume = $"{row.Cells["NomPersonnage"].Value} à {row.Cells["NomLieu"].Value}";

            if (!UiHelper.Confirmer($"Supprimer la mission n° {id} ({resume}) ?")) return;

            try
            {
                if (!_missionRepository.Delete(id))
                    UiHelper.Avertissement("Cette mission n'existe plus dans la base de données.");
                ChargerMissions();
            }
            catch (Exception ex)
            {
                UiHelper.Erreur(ex, "La mission n'a pas pu être supprimée.");
            }
        }

        private void btnActualiserMissions_Click(object sender, EventArgs e)
        {
            RafraichirDonnees();
        }

        // =====================================================================
        //  Recherche filtrée (mode connecté)
        // =====================================================================

        private TypeRecherche TypeSelectionne =>
            cbTypeRecherche.SelectedValue is TypeRecherche t ? t : TypeRecherche.MissionsParPeriode;

        private void cbTypeRecherche_SelectedIndexChanged(object sender, EventArgs e)
        {
            MettreAJourFiltres();
        }

        /// <summary>Active seulement les filtres utiles au type de recherche choisi.</summary>
        private void MettreAJourFiltres()
        {
            TypeRecherche type = TypeSelectionne;
            bool parTexte = type == TypeRecherche.LieuxParRoyaume || type == TypeRecherche.PersonnagesParRace;

            cbCritere.Enabled = parTexte;
            dtpRechDebut.Enabled = !parTexte;
            dtpRechFin.Enabled = !parTexte;
            lblCritere.Text = type == TypeRecherche.PersonnagesParRace ? "Race :" : "Royaume :";

            if (!parTexte)
            {
                cbCritere.DataSource = null;
                cbCritere.Text = string.Empty;
                return;
            }

            try
            {
                string texte = cbCritere.Text;
                cbCritere.DataSource = type == TypeRecherche.PersonnagesParRace
                    ? _personnageRepository.GetRaces()
                    : _lieuRepository.GetRoyaumes();
                cbCritere.SelectedIndex = -1;
                cbCritere.Text = texte;
            }
            catch (Exception ex)
            {
                UiHelper.Erreur(ex, "Impossible de charger les valeurs du filtre.");
            }
        }

        private void btnRechercher_Click(object sender, EventArgs e)
        {
            TypeRecherche type = TypeSelectionne;
            DateTime debut = TronquerALaMinute(dtpRechDebut.Value);
            DateTime fin = TronquerALaMinute(dtpRechFin.Value);
            string critere = (cbCritere.Text ?? string.Empty).Trim();

            if ((type == TypeRecherche.MissionsParPeriode || type == TypeRecherche.LieuxDisponibles) && fin <= debut)
            {
                UiHelper.Avertissement("La date de fin de la période doit être postérieure à la date de début.");
                return;
            }

            if (critere.Length > 50)
            {
                UiHelper.Avertissement("Le critère de recherche ne doit pas dépasser 50 caractères.");
                return;
            }

            try
            {
                int nb;
                dgvResultats.DataSource = null;

                switch (type)
                {
                    case TypeRecherche.LieuxParRoyaume:
                        List<Lieu> lieux = _lieuRepository.RechercherParRoyaume(critere);
                        AfficherLieux(lieux);
                        nb = lieux.Count;
                        break;

                    case TypeRecherche.PersonnagesParRace:
                        List<Personnage> personnages = _personnageRepository.RechercherParRace(critere);
                        dgvResultats.DataSource = personnages;
                        FormaterColonne(dgvResultats, "Id", "Id", 50);
                        FormaterColonne(dgvResultats, "Nom", "Nom", 0);
                        FormaterColonne(dgvResultats, "Race", "Race", 120);
                        FormaterColonne(dgvResultats, "Faction", "Faction", 200);
                        nb = personnages.Count;
                        break;

                    case TypeRecherche.LieuxDisponibles:
                        List<Lieu> libres = _lieuRepository.RechercherDisponibles(debut, fin);
                        AfficherLieux(libres);
                        nb = libres.Count;
                        break;

                    default:
                        List<Mission> missions = _missionRepository.RechercherParPeriode(debut, fin);
                        dgvResultats.DataSource = missions;
                        FormaterColonne(dgvResultats, "Id", "Id", 50);
                        FormaterColonne(dgvResultats, "NomLieu", "Lieu", 0);
                        FormaterColonne(dgvResultats, "NomPersonnage", "Personnage", 0);
                        FormaterColonne(dgvResultats, "DateDebut", "Début", 140);
                        FormaterColonne(dgvResultats, "DateFin", "Fin", 140);
                        MasquerColonne(dgvResultats, "LieuId");
                        MasquerColonne(dgvResultats, "PersonnageId");
                        nb = missions.Count;
                        break;
                }

                lblNbResultats.Text = nb == 0 ? "Aucun résultat." : $"{nb} résultat(s).";
                if (nb == 0) UiHelper.Info("Aucun résultat ne correspond à ces critères.", "Recherche");
            }
            catch (Exception ex)
            {
                UiHelper.Erreur(ex, "La recherche a échoué.");
            }
        }

        private void AfficherLieux(List<Lieu> lieux)
        {
            dgvResultats.DataSource = lieux;
            FormaterColonne(dgvResultats, "Id", "Id", 50);
            FormaterColonne(dgvResultats, "NomLieu", "Lieu", 0);
            FormaterColonne(dgvResultats, "Royaume", "Royaume", 150);
            FormaterColonne(dgvResultats, "Dangerosite", "Dangerosité", 110);
        }
    }
}

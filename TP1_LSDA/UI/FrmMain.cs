using System;
using System.Windows.Forms;
using TP1_LSDA.Repositories;

namespace TP1_LSDA.UI
{
    /// <summary>Fenêtre principale : exactement 3 onglets (Lieux, Personnages, Missions).</summary>
    public partial class FrmMain : Form
    {
        private readonly FrmLieux _frmLieux;
        private readonly FrmPersonnages _frmPersonnages;
        private readonly FrmMissions _frmMissions;

        public FrmMain(string connectionString)
        {
            InitializeComponent();

            var lieuRepository = new LieuRepository(connectionString);
            var personnageRepository = new PersonnageRepository(connectionString);
            var missionRepository = new MissionRepository(connectionString);

            _frmLieux = new FrmLieux(lieuRepository);
            _frmPersonnages = new FrmPersonnages(personnageRepository);
            _frmMissions = new FrmMissions(missionRepository, lieuRepository, personnageRepository);

            Integrer(_frmLieux, tabLieux);
            Integrer(_frmPersonnages, tabPersonnages);
            Integrer(_frmMissions, tabMissions);
        }

        /// <summary>Affiche un formulaire enfant à l'intérieur d'un onglet.</summary>
        private static void Integrer(Form enfant, TabPage onglet)
        {
            enfant.TopLevel = false;
            enfant.FormBorderStyle = FormBorderStyle.None;
            enfant.Dock = DockStyle.Fill;
            onglet.Controls.Add(enfant);
            enfant.Show();
        }

        private void tabControlPrincipal_Selected(object sender, TabControlEventArgs e)
        {
            // Les lieux/personnages ont pu changer : on rafraîchit l'onglet affiché.
            if (e.TabPage == tabMissions) _frmMissions.RafraichirDonnees();
            else if (e.TabPage == tabLieux) _frmLieux.RechargerSiAucuneModification();
            else if (e.TabPage == tabPersonnages) _frmPersonnages.RechargerSiAucuneModification();
        }

        private void tabControlPrincipal_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            bool modifs = (e.TabPage == tabLieux && _frmLieux.ADesModificationsNonSauvegardees) ||
                          (e.TabPage == tabPersonnages && _frmPersonnages.ADesModificationsNonSauvegardees);

            if (modifs && !UiHelper.Confirmer(
                    "Des modifications ne sont pas sauvegardées dans cet onglet.\n" +
                    "Voulez-vous quand même changer d'onglet ? (elles resteront en attente)"))
            {
                e.Cancel = true;
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((_frmLieux.ADesModificationsNonSauvegardees || _frmPersonnages.ADesModificationsNonSauvegardees) &&
                !UiHelper.Confirmer("Des modifications ne sont pas sauvegardées. Quitter quand même ?"))
            {
                e.Cancel = true;
            }
        }
    }
}

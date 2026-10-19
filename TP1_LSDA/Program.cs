using System;
using System.Configuration;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using TP1_LSDA.Data;
using TP1_LSDA.UI;

namespace TP1_LSDA
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            // Toute erreur imprévue est affichée et journalisée au lieu de fermer l'application en silence.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) => AfficherErreurFatale(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (s, e) => AfficherErreurFatale(e.ExceptionObject as Exception);

            try
            {
                Demarrer();
            }
            catch (Exception ex)
            {
                AfficherErreurFatale(ex);
            }
        }

        // Méthode séparée : une erreur de chargement d'assembly est ainsi interceptée par Main.
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void Demarrer()
        {
            ApplicationConfiguration.Initialize();

            string connectionString = ConfigurationManager
                .ConnectionStrings["DefaultConnection"]?.ConnectionString;

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                MessageBox.Show("La chaîne de connexion « DefaultConnection » est introuvable dans app.config.",
                    "Configuration", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Si LocalDB n'est pas joignable normalement, on s'y connecte par son canal nommé.
                connectionString = LocalDbHelper.ResoudreConnexion(connectionString);

                // SQL FIRST : la BD est créée par les scripts .sql (exécutés via ADO.NET si elle n'existe pas).
                var initializer = new DatabaseInitializer(connectionString);
                if (initializer.EnsureDatabase())
                {
                    MessageBox.Show("La base de données TP1_LSDA a été créée et alimentée à partir des scripts SQL.",
                        "Initialisation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible d'initialiser la base de données :\n\n" + ex.Message +
                                "\n\nVérifiez que SQL Server LocalDB (mssqllocaldb) est installé, " +
                                "ou exécutez les scripts du dossier DatabaseScripts dans SSMS.",
                    "Erreur de base de données", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.Run(new FrmMain(connectionString));
        }

        private static int _erreurAffichee;

        private static void AfficherErreurFatale(Exception ex)
        {
            if (Interlocked.Exchange(ref _erreurAffichee, 1) == 1) return;

            string details = ex?.ToString() ?? "Erreur inconnue.";
            string fichier = Path.Combine(AppContext.BaseDirectory, "erreur_demarrage.txt");
            try { File.WriteAllText(fichier, DateTime.Now + Environment.NewLine + details); }
            catch { /* journalisation facultative */ }

            Exception racine = ex;
            while (racine?.InnerException != null) racine = racine.InnerException;

            MessageBox.Show("Erreur inattendue :\n\n" + racine?.GetType().Name + " : " + racine?.Message +
                            "\n\nDétails complets enregistrés dans :\n" + fichier,
                "TP1_LSDA — Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

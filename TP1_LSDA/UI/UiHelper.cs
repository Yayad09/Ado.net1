using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TP1_LSDA.UI
{
    /// <summary>Messages utilisateur communs (MessageBox) et traduction des erreurs SQL Server.</summary>
    internal static class UiHelper
    {
        public static void Info(string message, string titre = "Information")
        {
            MessageBox.Show(message, titre, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Avertissement(string message, string titre = "Validation")
        {
            MessageBox.Show(message, titre, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static bool Confirmer(string message, string titre = "Confirmation")
        {
            return MessageBox.Show(message, titre, MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                       MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }

        public static void Erreur(Exception ex, string contexte)
        {
            MessageBox.Show(contexte + "\n\n" + TraduireErreur(ex), "Erreur",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>Transforme une exception technique en message clair pour l'utilisateur.</summary>
        public static string TraduireErreur(Exception ex)
        {
            if (ex is SqlException sql)
            {
                switch (sql.Number)
                {
                    case 547:
                        return sql.Message.Contains("REFERENCE")
                            ? "Cet élément est utilisé par au moins une mission : supprimez d'abord les missions associées."
                            : "Une contrainte de la base de données n'est pas respectée (valeur hors bornes ou dates invalides).";
                    case 2601:
                    case 2627:
                        return "Cette valeur existe déjà : le nom doit être unique.";
                    case 8152:
                    case 2628:
                        return "Un texte saisi est trop long pour la colonne correspondante.";
                    case 50000:
                        return sql.Message;
                    case -2:
                        return "Délai d'attente dépassé lors de la communication avec SQL Server.";
                    case -1:
                    case 2:
                    case 53:
                    case -1983577832:
                        return "Impossible de se connecter à SQL Server (LocalDB). Vérifiez que le serveur est démarré.";
                    default:
                        return $"Erreur SQL Server n° {sql.Number} : {sql.Message}";
                }
            }

            if (ex is System.Data.DBConcurrencyException)
                return "La ligne a été modifiée ou supprimée par ailleurs. Les données vont être rechargées.";

            return ex.Message;
        }
    }
}

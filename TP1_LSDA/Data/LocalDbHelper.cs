using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;

namespace TP1_LSDA.Data
{
    /// <summary>
    /// Contournement pour certains postes où le pilote ne parvient pas à charger SQLUserInstance.dll
    /// (erreur 56 « Unable to load the SQLUserInstance.dll »). On démarre l'instance LocalDB avec
    /// l'utilitaire sqllocaldb.exe, on lit son « Instance pipe name » et on s'y connecte directement.
    /// La chaîne de connexion d'app.config reste inchangée : seule la source de données est
    /// remplacée en mémoire, et uniquement si la connexion normale échoue.
    /// </summary>
    public static class LocalDbHelper
    {
        private static readonly Regex PipeRegex =
            new Regex(@"(np:\\\\\.\\pipe\\LOCALDB#[^\s]+\\tsql\\query)", RegexOptions.IgnoreCase);

        /// <summary>Retourne une chaîne de connexion utilisable (l'originale si elle fonctionne).</summary>
        public static string ResoudreConnexion(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);
            string source = builder.DataSource ?? string.Empty;

            if (!source.StartsWith("(localdb)", StringComparison.OrdinalIgnoreCase))
                return connectionString;

            if (PeutSeConnecter(connectionString)) return connectionString;

            string instance = source.Substring(source.IndexOf('\\') + 1);
            string pipe = ObtenirPipe(instance);

            builder.DataSource = pipe;
            // Connexion locale par canal nommé : certificat auto-signé de LocalDB accepté.
            builder.TrustServerCertificate = true;
            return builder.ConnectionString;
        }

        private static bool PeutSeConnecter(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString)
            {
                InitialCatalog = "master",
                ConnectTimeout = 30
            };

            try
            {
                using (var connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (SqlException)
            {
                return false;
            }
        }

        private static string ObtenirPipe(string instance)
        {
            string exe = TrouverSqlLocalDb();

            // « create » échoue sans conséquence si l'instance existe déjà.
            Executer(exe, $"create \"{instance}\"");
            string demarrage = Executer(exe, $"start \"{instance}\"");
            string info = Executer(exe, $"info \"{instance}\"");

            Match match = PipeRegex.Match(info);
            if (!match.Success)
                throw new InvalidOperationException(
                    $"L'instance LocalDB « {instance} » n'a pas pu être démarrée.\n\n" +
                    $"sqllocaldb start :\n{demarrage.Trim()}\n\nsqllocaldb info :\n{info.Trim()}");

            return match.Groups[1].Value;
        }

        private static string TrouverSqlLocalDb()
        {
            var candidats = new List<string>();
            foreach (string racine in new[]
                     {
                         Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                         Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)
                     })
            {
                string dossier = Path.Combine(racine, "Microsoft SQL Server");
                if (!Directory.Exists(dossier)) continue;

                foreach (string version in Directory.GetDirectories(dossier))
                {
                    string exe = Path.Combine(version, "Tools", "Binn", "SqlLocalDB.exe");
                    if (File.Exists(exe)) candidats.Add(exe);
                }
            }

            // Version la plus récente en premier (ex. 170 avant 160 avant 150).
            candidats.Sort((a, b) => string.Compare(b, a, StringComparison.OrdinalIgnoreCase));
            return candidats.Count > 0 ? candidats[0] : "SqlLocalDB.exe";
        }

        private static string Executer(string exe, string arguments)
        {
            var info = new ProcessStartInfo(exe, arguments)
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            try
            {
                using (Process process = Process.Start(info))
                {
                    string sortie = process.StandardOutput.ReadToEnd() + process.StandardError.ReadToEnd();
                    process.WaitForExit(60000);
                    return sortie;
                }
            }
            catch (System.ComponentModel.Win32Exception)
            {
                throw new InvalidOperationException(
                    "SQL Server LocalDB est introuvable sur ce poste (SqlLocalDB.exe absent). " +
                    "Installez « SQL Server Express LocalDB » avec Visual Studio Installer.");
            }
        }
    }
}

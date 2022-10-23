using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace BashSystem
{
    public class BashWrapper
    {
        /// <summary>
        /// Executes a shell-bash command or script from a spesific directory. Default file name is "/bin/bash" for unix operating systems.
        /// </summary>
        /// <param name="command">The Unix command to execute</param>
        /// <param name="directory">The Working Directory your bash script is placed</param>
        /// <param name="osFile">Operating System file</param>
        /// <returns></returns>
        public static bool Run(string command, string directory, string osFile = "/bin/bash")
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo(osFile);
                psi.WorkingDirectory = Path.GetFullPath(directory);
                psi.Arguments = command;

                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;

                using (Process process = new Process())
                {
                    process.StartInfo = psi;
                    process.Start();
                    process.WaitForExit();

                    string result = process.StandardOutput.ReadToEnd();
                    string err = process.StandardError.ReadToEnd();

                    // Logs
                    if (!string.IsNullOrEmpty(result))
                        UnityEngine.Debug.Log(result);

                    if (!string.IsNullOrEmpty(err))
                    {
                        UnityEngine.Debug.LogError("Error: " + err);
                        return false;
                    }
                }
                //Uncomment if you are using Unity Editor GUI
                //UnityEditor.AssetDatabase.Refresh();
                return true;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public static bool Run(string command, string osFile = "/bin/bash")
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo(osFile);

                psi.Arguments = "-c " + command + "'";
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;

                using (Process process = new Process())
                {
                    process.StartInfo = psi;
                    process.Start();
                    process.WaitForExit();

                    string result = process.StandardOutput.ReadToEnd();
                    string err = process.StandardError.ReadToEnd();

                    // Logs
                    if (!string.IsNullOrEmpty(result))
                        UnityEngine.Debug.Log(result);

                    if (!string.IsNullOrEmpty(err))
                    {
                        UnityEngine.Debug.LogError("Error: " + err);
                        return false;
                    }
                }
                //Uncomment if you are using Unity Editor GUI
                //UnityEditor.AssetDatabase.Refresh();
                return true;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Set a bash script (or scripts) to executable.
        /// </summary>
        public static bool Shmod(string bashFileName, string directory, string osFile = "/bin/bash")
        {
            string command = "-c 'chmod +x" + bashFileName + "'";
            return Run(command, directory, osFile);
        }
        /// <summary>
        /// Get the list of files in a spesific folder. Return a list of file names.
        /// </summary>
        /// <param name="directoryPath"></param>
        public static List<string> ListFiles(string directoryPath)
        {
            List<string> files = new List<string>();
            foreach (string file in Directory.GetFiles(directoryPath))
            {
                string filename = Path.GetFileName(file);
                files.Add(filename);
                Print(filename);
            }
            return files;
        }

        //Uncomment the appropriate print function based on your application type
        #region Logs
        private static void Print(string text = "")
        {
#if UNITY_EDITOR
            UnityEngine.Debug.Log(text);
#elif (NETFRAMEWORK || NETSTANDARD || NET)
            System.Console.WriteLine(text);
#endif
        }
#endregion
    }
}

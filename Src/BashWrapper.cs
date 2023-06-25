using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System;

namespace BashSystem
{
    public class BashWrapper
    {
        /// <summary>
        /// Executes a single cmd command.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command">The command to execute</param>
        /// <param name="debug">Whether or not to print logs</param>
        /// <returns></returns>
        public static async Task<T> Run<T>(string command, bool debug = false)
        {
            string osFile = "/bin/bash";
            string arg = "-c ";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                osFile = "cmd.exe";
                arg = "/C ";
            }

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo(osFile);
                psi.Arguments = arg + "'" + command + "'";
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                using (Process process = new Process())
                {
                    process.StartInfo = psi;

                    if(debug)
                        Print("Executing command...");

                    process.Start();
                    await Task.Run(() => { process.WaitForExit(); });

                    string result = await process.StandardOutput.ReadToEndAsync();
                    string error = await process.StandardError.ReadToEndAsync();


                    if (!string.IsNullOrEmpty(error))
                    {
                        if (typeof(T) == typeof(string))
                        {
                            T res = (T)(object)("Error: " + error);
                            return res;
                        }
                        else if (typeof(T) == typeof(bool))
                        {
                            T res = (T)(object)false;
                            return res;
                        }
                        else if (typeof(T) == typeof(Task))
                        {
                            return default(T);
                        }
                    }

                    if (typeof(T) == typeof(string))
                    {
                        T res = (T)(object)result;
                        return res;
                    }
                    else if (typeof(T) == typeof(bool))
                    {
                        T res = (T)(object)true;
                        return res;
                    }
                    else
                    {
                        return default(T);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Set a bash script to executable.
        /// </summary>
        public static async Task<T> BashAsExecutable<T>(string bashFileName)
        {
            string command = "chmod +x" + bashFileName;
            return await Run<T>(command);
        }


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

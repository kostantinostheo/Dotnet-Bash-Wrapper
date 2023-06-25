using System.Threading.Tasks;

namespace BashSystem.Manage
{
    public class BashManager
    {
        public static async Task<T> RunAsync<T>(string command)
        {
            return await BashWrapper.Run<T>(command);
        }
        public static dynamic RunSync<dynamic>(string command)
        {
            return BashWrapper.Run<dynamic>(command).Result;
        }


        public static async Task<T> BashAsExecutableAsync<T>(string bashFile)
        {
            return await BashWrapper.BashAsExecutable<T>(bashFile);
        }
        public static dynamic BashAsExecutableSync<dynamic>(string bashFile)
        {
            return  BashWrapper.BashAsExecutable<dynamic>(bashFile).Result;
        }
    }
}
using System.IO;

namespace ReferralOrchAPITest.Helper
{
    public class Utils
    {
        public static string ExpectationFileFullPath(string filename)
        {
            return Path.GetFullPath("TestCases/" + filename);
        }
    }
}
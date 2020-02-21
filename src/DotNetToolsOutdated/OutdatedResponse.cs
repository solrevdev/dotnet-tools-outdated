using System.Diagnostics;

namespace DotNetToolsOutdated
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class OutdatedResponse
    {
        public string Directory;

        public string PackageName;

        public string CurrentVer;

        public string AvailableVer;


        public bool IsOutdated => CompareVersions(CurrentVer, AvailableVer) == -1;

        public bool ProcessedOkOutdated;

        public static int CompareVersions(string ver1, string ver2)
        {
            if (ver1 == null && ver2 == null) return 0;
            if (ver1 == null && ver2 != null) return -1;
            if (ver1 != null && ver2 == null) return 1;
            
            var ver1Arr = ver1.Split('.');
            var ver2Arr = ver2.Split('.');

            var len = ver1Arr.Length;
            if (ver2Arr.Length < len) len = ver2.Length;
            var i = 0;
            while (i < len)
            {
                // number comparison
                var num1Parsed = int.TryParse(ver1Arr[i], out var num1);
                var num2Parsed = int.TryParse(ver2Arr[i], out var num2);
                if (num1Parsed && num2Parsed)
                {
                    if (num1 < num2) return -1;
                    if (num1 > num2) return 1;
                }
                else
                {
                    // string comparison
                    var stringCmp = ver1Arr[i].CompareTo(ver2Arr[i]);
                    if (stringCmp != 0) return stringCmp;
                }
                i++;
            }
            // array length comparison
            if (ver1Arr.Length < ver2Arr.Length) return -1;
            if (ver1Arr.Length > ver2Arr.Length) return 1;
            return 0;
        }

        internal string DebuggerDisplay
        {
            get
            {
                var res = $"OutResp: \"{PackageName}\", current: {CurrentVer}, avail: {AvailableVer}";
                if (ProcessedOkOutdated)
                {
                    res = res + ", ok OUTDATED";
                }
                else if (IsOutdated)
                {
                    res = res + ", OUTDATED";
                }
                return res;
            }
        }
    }
}
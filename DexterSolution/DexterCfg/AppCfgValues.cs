namespace DexterCfg
{
    internal class AppCfgValues
    {
        internal static readonly string LogFileNameFormat = "event-{0}.log";

        internal static readonly string ErrorLogFileNameFormat = "error-{0}.log";

        internal static readonly string LogFileDateFormat = "yyyy-MM-dd-HH-mm-ss";

        internal static readonly string ErrorFileDateFormat = "yyyy-MM-dd-HH-mm-ss";

        internal static readonly string GeneralDateFormat = "yyyy-MM-dd, HH:mm:ss ffffff";

        internal static readonly string Lines = "----------------------------------";

        internal static readonly string ErrorFolderName = "Errors";

        internal static readonly string EventFolderName = "Events";

        internal static readonly string AssemblyNodeName = "namespace";

        internal static readonly string ConnectionNodeName = "name";

        internal static readonly string TypeNodeName = "typename";

        internal static readonly string ConfigMainNodeName = "dexter.configuration/dexter.configs";

        internal static readonly string ConfigAddSectionName = "dexter/add";

        internal static readonly string ConfigSettingNodesName = "dexter.configuration/dexter-settings";

        internal static readonly string ConfigSettingSectionName = "setting";

        internal static readonly string ConfigFile = "dexter.cfg.xml";

        internal static readonly string IsWriteEventLogAttribute = "writeEventLog";

        internal static readonly string IsWriteErrorLogAttribute = "writeErrorLog";

        internal static readonly string One = "1";

        internal static readonly string Key = "key";
        internal static readonly string Value = "value";
        // "dexter.configuration/dexter-settings/setting"
    }
}

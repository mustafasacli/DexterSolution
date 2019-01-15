namespace DexterCfg.Configuraton
{
    using System;
    using System.Xml;

    internal static class DxCfgConfiguratonHelper
    {
        private static object syncObj = new object();
        private static XmlNode mainNode = null;

        private static object syncObj2 = new object();
        private static XmlNode settingNode = null;

        /// <summary>
        /// Get Connection Main Node.
        /// </summary>
        /// <returns>Returns XmlNode object.</returns>
        internal static XmlNode GetMainNode()
        {
            if (mainNode == null)
            {
                lock (syncObj)
                {
                    if (mainNode == null)
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(AppCfgValues.ConfigFile);

                        mainNode = doc.SelectSingleNode(AppCfgValues.ConfigMainNodeName);
                    }
                }
            }

            return mainNode;
        }

        /// <summary>
        /// Get Setting Main Node.
        /// </summary>
        /// <returns>Returns XmlNode object.</returns>
        internal static XmlNode GetSettingNode()
        {
            if (settingNode == null)
            {
                lock (syncObj2)
                {
                    if (settingNode == null)
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(AppCfgValues.ConfigFile);

                        settingNode = doc.SelectSingleNode(AppCfgValues.ConfigSettingNodesName);
                    }
                }
            }

            return settingNode;
        }

        /// <summary>
        /// Get Connection List as XmlNodeList.
        /// </summary>
        /// <returns>Returns XmlNodeList object</returns>
        public static XmlNodeList GetConnectionNodeList()
        {
            XmlNodeList nodeList = null;

            try
            {
                nodeList = GetMainNode().SelectNodes(AppCfgValues.ConfigAddSectionName);
            }
            catch (Exception e)
            {
                throw;
            }

            return nodeList;
        }
        
        /// <summary>
        /// Checks events will be logged.
        /// </summary>
        public static bool IsWriteEventLog
        {
            get
            {
                bool b = false;

                XmlAttribute attr = GetMainNode().Attributes[AppCfgValues.IsWriteEventLogAttribute];

                string s = attr?.Value;
                s = s ?? string.Empty;
                s = s.Trim();
                b = s == AppCfgValues.One;

                return b;
            }
        }
        
        /// <summary>
        /// Checks erors will be logged.
        /// </summary>
        public static bool IsWriteErrorLog
        {
            get
            {
                bool b = false;

                XmlAttribute attr = GetMainNode().Attributes[AppCfgValues.IsWriteErrorLogAttribute];

                string s = attr?.Value;
                s = s ?? string.Empty;
                s = s.Trim();
                b = s == AppCfgValues.One;

                return b;
            }
        }

        /// <summary>
        /// Checks events will be logged.
        /// </summary>
        public static bool IsWriteSettingEventLog
        {
            get
            {
                bool b = false;

                XmlAttribute attr = GetSettingNode().Attributes[AppCfgValues.IsWriteEventLogAttribute];

                string s = attr?.Value;
                s = s ?? string.Empty;
                s = s.Trim();
                b = s == AppCfgValues.One;

                return b;
            }
        }

        /// <summary>
        /// Checks erors will be logged.
        /// </summary>
        public static bool IsWriteSettingErrorLog
        {
            get
            {
                bool b = false;

                XmlAttribute attr = GetSettingNode().Attributes[AppCfgValues.IsWriteErrorLogAttribute];

                string s = attr?.Value;
                s = s ?? string.Empty;
                s = s.Trim();
                b = s == AppCfgValues.One;

                return b;
            }
        }
    }
}
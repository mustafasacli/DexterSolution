namespace Dexter.Configuraton
{
    using System;
    using System.Configuration;
    using System.Xml;

    internal static class DxConfiguratonHelper
    {
        public static XmlNodeList GetNodeList()
        {
            XmlNodeList nodeList = null;

            try
            {
                //XmlNode mainNode = ConfigurationManager.GetSection("dapper.external.configs") as XmlNode;
                //nodeList = mainNode.SelectNodes("dapper.external/add");
                XmlNode mainNode = ConfigurationManager.GetSection(AppValues.ConfigMainSectionName) as XmlNode; //"dexter.configs") as XmlNode;
                nodeList = mainNode.SelectNodes(AppValues.ConfigAddSectionName);//"dexter/add");
            }
            catch (Exception e)
            {
                throw;
            }

            return nodeList;
        }
    }
}

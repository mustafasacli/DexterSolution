namespace Dexter.Configuraton
{
    using System.Configuration;
    using System.Xml;

    public class DxExternalConfigurationSectionHandler : IConfigurationSectionHandler
    {
        public DxExternalConfigurationSectionHandler()
        {
        }

        public object Create(object parent, object configContext, XmlNode section)
        {
            return section;
        }
    }
}

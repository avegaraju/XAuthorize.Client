using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace XAuthorize.Client.Tests.Helpers
{
    internal static class XDocumentExtension
    {
        internal static XElement GetFirstAttributesElement(this XDocument requestDocument)
        {
            return requestDocument.GetAttributesElements()
                                  .First();
        }

        internal static IEnumerable<XElement> GetAttributesElements(this XDocument requestDocument)
        {
            return requestDocument.GetRequestElements()
                                  .Elements("Attributes");
        }

        internal static IEnumerable<XElement> GetRequestElements(this XDocument requestDocument)
        {
            return requestDocument.Elements("Request");
        }
    }
}

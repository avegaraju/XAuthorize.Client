using System.Xml.Linq;

namespace XAuthorize.Client.Elements
{
    public class ElementCreator
    {
        protected ElementCreator()
        {
            RequestSchema = new RequestSchema();
        }

        internal RequestSchema RequestSchema { get; }

        protected XElement CreateElement(string name)
        {
            return new XElement(name);
        }

        protected XAttribute CreateAttribute(string name, object value)
        {
            return new XAttribute(name, value);
        }
    }
}
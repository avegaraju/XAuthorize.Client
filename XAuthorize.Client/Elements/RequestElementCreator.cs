using System.Xml.Linq;

namespace XAuthorize.Client.Elements
{
    internal class RequestElementCreator: ElementCreator
    {
        public XElement CreateRequestElement(bool returnPolicyIdList, bool combinedDecision)
        {
            var requestElement = CreateElement("Request");

            requestElement.Add(CreateAttribute("ReturnPolicy", returnPolicyIdList),
                               CreateAttribute("CombinedDecision", combinedDecision));

            return requestElement;
        }
    }
}
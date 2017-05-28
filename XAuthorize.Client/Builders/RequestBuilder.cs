using System.Xml.Linq;

using XAuthorize.Client.Elements;

namespace XAuthorize.Client.Builders
{
    public class RequestBuilder
    {
        private readonly XDocument _requestDocument;
        private readonly RequestElementCreator _requestElementCreator;

        public RequestBuilder()
        {
            _requestElementCreator = new RequestElementCreator();
            _requestDocument = new XDocument();
        }

        public XDocument Build()
        {
            return _requestDocument;
        }

        public IAttributesElementCreator AddRequestElement(bool returnPolicyIdList, bool combinedDecision)
        {
            var requestElement = _requestElementCreator.CreateRequestElement(returnPolicyIdList, combinedDecision);
            _requestDocument.Add(requestElement);

            return new AttributesElementCreator(requestElement, this);
        }
    }

    
}
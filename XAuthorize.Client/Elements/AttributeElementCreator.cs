using System.Collections.Generic;
using System.Xml.Linq;

namespace XAuthorize.Client.Elements
{
    public class AttributeElementCreator: ElementCreator
    {
        private readonly AttributeValueElementCreator _attributeValueElementCreator;
        private List<XElement> _attributeElementList = new List<XElement>();
        public IReadOnlyCollection<XElement> AttributeElementList => _attributeElementList;

        public AttributeElementCreator()
        {
            _attributeValueElementCreator = new AttributeValueElementCreator();
        }

        /// <summary>
        ///     Adds element &lt;Attribute IncludeInResult="true/false" AttributeId="urn:oasis:names:tc:xacml:1.0:subject:subject-id" />
        ///     and element &lt; AttributeValue DataType="urn:oasis:names:tc:xacml:1.0:data-type:Email" /> to the Request Document"
        /// </summary>
        /// <param name="includeInResult">Include attribute value in policy evaluation response.</param>
        /// <param name="attributeId">Value of Universal Resource Name (URN)</param>
        /// <param name="attributeValue">Value of <see cref="attributeId"/></param>
        /// <param name="attributeValueDataType">Data type of <see cref="attributeValue" /></param>
        public void AddChildAttributeElement(bool includeInResult,
                                             Subject attributeId,
                                             string attributeValue,
                                             AttributeValueDataType attributeValueDataType)
        {
            XElement attributeElement = CreateAttributeElement(includeInResult, attributeId, attributeValueDataType);

            _attributeElementList.Add(attributeElement);
        }

        private XElement CreateAttributeElement(bool includeInResult, Subject attributeId, AttributeValueDataType attributeValueDataType)
        {
            XElement attributeValueElement = CreateAttributeValueElement(attributeValueDataType);

            XElement attributeElement = CreateAttributeElementAndAttributes(includeInResult, attributeId, attributeValueElement);

            return attributeElement;
        }

        private XElement CreateAttributeElementAndAttributes(bool includeInResult, Subject attributeId, XElement attributeValueElement)
        {
            var attributeElement = CreateElement("Attribute");

            attributeElement.Add(CreateAttribute("IncludeInResult", includeInResult),
                                  CreateAttribute("AttributeId",
                                                  RequestSchema.SubjectAttributeIdUrn +
                                                  RequestSchema.GetUrnValue(attributeId.ToString())),
                                  attributeValueElement);
            return attributeElement;
        }

        private XElement CreateAttributeValueElement(AttributeValueDataType attributeValueDataType)
        {
            return _attributeValueElementCreator.CreateAttributeValueElement(attributeValueDataType);
        }
    }
}
using System.Xml.Linq;

namespace XAuthorize.Client.Elements
{
    internal class AttributeValueElementCreator: ElementCreator
    {
        internal XElement CreateAttributeValueElement(AttributeValueDataType attributeValueDataType)
        {
            var attributeValueElement = CreateElement("AttributeValue");

            attributeValueElement.Add(CreateAttribute("DataType",
                                                      RequestSchema.DataTypeUrn +
                                                      RequestSchema.GetUrnValue(attributeValueDataType.ToString())));

            return attributeValueElement;
        }
    }
}
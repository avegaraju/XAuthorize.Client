using System;
using System.Collections.Generic;
using System.Xml.Linq;

using XAuthorize.Client.Builders;

namespace XAuthorize.Client.Elements
{
    public interface IAttributesElementCreator
    {
        /// <summary>
        ///     Adds XACML 'element &lt;Attributes Category="urn:oasis:names:tc:xacml:1.0:subject-category:' to the
        ///     Request document
        /// </summary>
        /// <param name="attributeElementAction"></param>
        /// <returns>A builder to build additional elements and attributes.</returns>
        RequestBuilder AddAttributesElement(SubjectCategory subjectCategory,
                                            Action<AttributeElementCreator> attributeElementAction);
    }

    public class AttributesElementCreator: ElementCreator, IAttributesElementCreator
    {
        private readonly RequestBuilder _requestBuilder;
        private readonly SubjectCategory _subjectCategory;
        private readonly XElement _requestElement;

        public AttributesElementCreator(XElement requestElement, RequestBuilder requestBuilder)
        {
            _requestElement = requestElement;
            _requestBuilder = requestBuilder;
        }

        public RequestBuilder AddAttributesElement(SubjectCategory subjectCategory,
                                                   Action<AttributeElementCreator> attributeElementAction)
        {
            var attributesElement = CreateAttributesElement(subjectCategory, attributeElementAction);
            _requestElement.Add(attributesElement);

            return _requestBuilder;
        }

        private XElement CreateAttributesElement(SubjectCategory subjectCategory,
                                                 Action<AttributeElementCreator> attributeElementCreatorAction)
        {
            IEnumerable<XElement> attributeElementList = CreateAttributeElementList(attributeElementCreatorAction);

            return CreateAttributesElement(subjectCategory, attributeElementList);
        }

        private static IEnumerable<XElement> CreateAttributeElementList(
            Action<AttributeElementCreator> attributeElementCreatorAction)
        {

            var attributeElementCreator = ExecuteAction(attributeElementCreatorAction);

            return attributeElementCreator.AttributeElementList;
        }

        private static AttributeElementCreator ExecuteAction(
            Action<AttributeElementCreator> attributeElementCreatorAction)
        {
            var attributeElementCreator = new AttributeElementCreator();
            attributeElementCreatorAction(attributeElementCreator);

            return attributeElementCreator;
        }

        private XElement CreateAttributesElement(SubjectCategory subjectCategory,
                                                 IEnumerable<XElement> attributeElementList)
        {
            var categoryAttribute = CreateCategoryAttribute(subjectCategory);

            var attributesElement = CreateElement("Attributes");

            attributesElement.Add(categoryAttribute);
            attributesElement.Add(attributeElementList);

            return attributesElement;
        }

        private XAttribute CreateCategoryAttribute(SubjectCategory subjectCategory)
        {
            return CreateAttribute("Category",
                                   RequestSchema.SubjectCategoryUrn +
                                   RequestSchema.GetUrnValue(subjectCategory.ToString()));
        }
    }
}
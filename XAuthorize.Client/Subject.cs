using System.Reflection;
using System.Resources;
using System.Xml.Linq;

namespace XAuthorize.Client
{
    public class Subject
    {
        private readonly ResourceManager _resourceManager;
        public XElement AttributesElement { get; private set; }

        public Subject()
        {
            _resourceManager =
                    new ResourceManager("XAuthorize.Client.RequestResources", Assembly.GetExecutingAssembly());
        }
        public void WithSubjectCategory(SubjectCategory subjectCategory)
        {
            var categoryAttribute = CreateCategoryAttribute(subjectCategory);
            AttributesElement = CreateAttributesElement(categoryAttribute);
        }

        private XAttribute CreateCategoryAttribute(SubjectCategory subjectCategory)
        {
            return new XAttribute("Category",
                                  RequestSchema.SUBJECT_CATEGORY
                                  + _resourceManager.GetString(subjectCategory.ToString()));
        }

        private static XElement CreateAttributesElement(XAttribute categoryAttribute)
        {
            var attributesElement = new XElement("Attributes");

            attributesElement.Add(categoryAttribute);

            return attributesElement;
        }


    }
}

using System;
using System.Reflection;
using System.Resources;
using System.Xml.Linq;

namespace XAuthorize.Client.Builders
{
    public class RequestBuilder
    {
        private readonly XDocument _requestDocument;

        public RequestBuilder()
        {
            _requestDocument = new XDocument();
        }

        /// <summary>
        ///     Adds XACML 'element &lt;Attributes Category="urn:oasis:names:tc:xacml:1.0:subject-category:' to the
        ///     Request document
        /// </summary>
        /// <param name="subjectAction"></param>
        /// <returns>A builder to build additional elements and attributes.</returns>
        public RequestBuilder HavingSubject(Action<Subject> subjectAction)
        {
            var subject = new Subject();

            subjectAction(subject);

            _requestDocument.Add(subject.AttributesElement);

            return this;
        }

        public XDocument Build()
        {
            return _requestDocument;
        }
    }
}
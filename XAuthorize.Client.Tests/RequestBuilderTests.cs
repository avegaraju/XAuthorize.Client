using System.Linq;
using System.Xml.Linq;

using FluentAssertions;

using XAuthorize.Client.Builders;

using Xunit;

namespace XAuthorize.Client.Tests
{
    public class RequestBuilderTests
    {
        [Fact]
        public void RequestBuilder_ReturnsRequestDocument()
        {
            var request = new RequestBuilder()
                    .Build();

            request.Should().BeOfType<XDocument>();
        }

        [Fact]
        public void AllowsSubjectToBeAddedToTheRequestDocument ()
        {
            var request = new RequestBuilder()
                    .HavingSubject(s =>
                                   {
                                       s.WithSubjectCategory(SubjectCategory.AccessSubject);
                                   })
                    .Build();

            request.Elements("Attributes")
                   .Should()
                   .HaveCount(expected: 1);

            request
                    .Elements("Attributes")
                    .First()
                    .Attributes("Category")
                    .Should().HaveCount(expected: 1);
        }

        [Fact]
        public void BlankSubjectAction_DoesntAddAttributesElementToTheRequestDocument()
        {
            var request = new RequestBuilder()
                    .HavingSubject(s =>
                                   {
                                       
                                   })
                    .Build();

            request.Elements("Attributes")
                   .Should()
                   .HaveCount(expected: 0);
        }
    }
}
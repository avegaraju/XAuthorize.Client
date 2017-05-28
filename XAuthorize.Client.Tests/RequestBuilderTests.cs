using System.Xml.Linq;

using FluentAssertions;

using XAuthorize.Client.Builders;
using XAuthorize.Client.Tests.Helpers;

using Xunit;

namespace XAuthorize.Client.Tests
{
    public class RequestBuilderTests
    {
        [Fact]
        public void AllowsCategoryAttribute_ToBeAddedTo_AttributesElement()
        {
            var request = new RequestBuilder()
                    .AddRequestElement(returnPolicyIdList: false, combinedDecision: false)
                    .AddAttributesElement(SubjectCategory.AccessSubject,
                                          attr =>
                                          {
                                              attr.AddChildAttributeElement(includeInResult: false,
                                                                            attributeId: Subject.SubjectId,
                                                                            attributeValue: "email@test.com",
                                                                            attributeValueDataType:
                                                                            AttributeValueDataType.Email);
                                          })
                    .Build();

            request.GetAttributesElements()
                   .Should()
                   .HaveCount(expected: 1);

            request.GetFirstAttributesElement()
                   .Attributes("Category")
                   .Should().HaveCount(expected: 1);
        }


        [Fact]
        public void AllowsMultipleAttributeElement_ToBeAdded_UnderAttributesElement()
        {
            var request = new RequestBuilder()
                    .AddRequestElement(returnPolicyIdList: false, combinedDecision: false)
                    .AddAttributesElement(SubjectCategory.AccessSubject,
                                          attr =>
                                          {
                                              attr.AddChildAttributeElement(includeInResult: false,
                                                                            attributeId: Subject.SubjectId,
                                                                            attributeValue: "email@test.com",
                                                                            attributeValueDataType:
                                                                            AttributeValueDataType.Email);

                                              attr.AddChildAttributeElement(includeInResult: false,
                                                                            attributeId: Subject.SubjectId,
                                                                            attributeValue: "email1234@test.com",
                                                                            attributeValueDataType:
                                                                            AttributeValueDataType.Email);
                                          })
                    .Build();

            request.GetFirstAttributesElement()
                   .Elements("Attribute")
                   .Should()
                   .HaveCount(expected: 2);
        }


        [Fact]
        public void BlankSubjectAction_DoesntAddAttributeElementsToTheRequestDocument()
        {
            var request = new RequestBuilder()
                    .AddRequestElement(returnPolicyIdList: false, combinedDecision: false)
                    .AddAttributesElement(SubjectCategory.AccessSubject, s => { })
                    .Build();

            request.GetFirstAttributesElement()
                   .Elements("Attribute")
                   .Should()
                   .HaveCount(expected: 0);
        }

        [Fact]
        public void CreatesRequestElementAsARootElement()
        {
            var request = new RequestBuilder()
                    .AddRequestElement(returnPolicyIdList: true, combinedDecision: true)
                    .AddAttributesElement(SubjectCategory.AccessSubject, attr => { })
                    .Build();

            request.GetRequestElements()
                   .Should()
                   .HaveCount(expected: 1);
        }

        [Fact]
        public void RequestBuilder_ReturnsRequestDocument()
        {
            var request = new RequestBuilder()
                    .Build();

            request.Should().BeOfType<XDocument>();
        }
    }
}
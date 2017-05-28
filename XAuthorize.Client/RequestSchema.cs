using System.Reflection;
using System.Resources;

namespace XAuthorize.Client
{
    internal class RequestSchema
    {
        internal string SubjectCategoryUrn =>  _UrnResourceManager.GetString("SubjectCategory");
        internal string DataTypeUrn => _UrnResourceManager.GetString("DataType");
        public string SubjectAttributeIdUrn => _UrnResourceManager.GetString("SubjectAttributeId");

        private readonly ResourceManager _UrnResourceManager;
        private readonly ResourceManager _UrnValueResourceManager;

        internal RequestSchema()
        {
            _UrnResourceManager =
                    new ResourceManager("XAuthorize.Client.RequestResource.Urn",
                                        Assembly.GetExecutingAssembly());

            _UrnValueResourceManager = new ResourceManager("XAuthorize.Client.RequestResource.Urn.Value",
                                                           Assembly.GetExecutingAssembly());
        }

        internal string GetUrnValue(string key)
        {
            return _UrnValueResourceManager.GetString(key);
        }
    }
}
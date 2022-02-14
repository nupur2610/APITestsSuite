/*using System;
using System.Xml;

namespace RuleEngineAPITests.Helper
{

    public enum AuthorizationsType
    {
        EnterpriseApi
    }

    public class createToken
    {

        public TokenGenIml CreateTokenModel(AuthorizationsType type)

        {
            switch (type)
            {

                case AuthorizationsType.EnterpriseApi:
                    return new EnterpriseToken();
                default:
                    throw new Exception();
            }

        }
    }
}*/
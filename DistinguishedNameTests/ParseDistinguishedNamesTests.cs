﻿using NUnit.Framework;
using SkiDiveCode.Ldap.Rfc2253;

namespace Rfc2253DistinguishedNameTests
{
    [TestFixture]
    public class ParseDistinguishedNamesTests
    {
        [TestCase(@"CN=Steve Kille,O=Isode Limited,C=GB", 
            ExpectedResult = @"cn=Steve Kille,o=Isode Limited,c=GB")]
        [TestCase(@"OU=Sales+CN=J. Smith,O=Widget Inc.,C=US", 
            ExpectedResult = @"ou=Sales+cn=J. Smith,o=Widget Inc.,c=US")]
        [TestCase(@"CN=L. Eagle,O=Sue\, Grabbit and Runn,C=GB",
            ExpectedResult = @"cn=L. Eagle,o=Sue\, Grabbit and Runn,c=GB")]
        [TestCase(@"CN =Before\0DAfter,O= Test,C  =  GB", ExpectedResult = @"cn=Before\0DAfter,o=Test,c=GB")]
        [TestCase(@"1.3.6.1.4.1.1466.0=#04024869,O=Test,C=GB", 
            ExpectedResult = @"1.3.6.1.4.1.1466.0=#04024869,o=Test,c=GB")]
        [TestCase(@"SN=Lu\C4\8Di\C4\87", ExpectedResult = @"sn=Lu\C4\8Di\C4\87")]
        [TestCase(@"OID.1.3.6.1.4.1.1466.0=Bytes \+ Bytes, O=OID Prefix,C=GB", 
            ExpectedResult = @"1.3.6.1.4.1.1466.0=Bytes \+ Bytes,o=OID Prefix,c=GB")]
        [TestCase(@"", ExpectedResult = @"")]
        [TestCase(@"CN=""Quoted Last, Quoted First"", O=Space After Comma ; C = Semi's too!", 
            ExpectedResult = @"cn=Quoted Last\, Quoted First,o=Space After Comma,c=Semi's too!")]
        [TestCase(@"CN=", ExpectedResult = @"cn=")]
        [TestCase(@"CN=,sn=Smith", ExpectedResult = @"cn=,sn=Smith")]
        //[TestCase(@"", ExpectedResult = @"")]
        public string ShouldParseSimpleDN(string distinguishedName)
        {
            var dn = DistinguishedName.Create(distinguishedName);
            return dn.GetAsNormalized();
        }
    }
}

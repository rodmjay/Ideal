#region credits
// ***********************************************************************
// Assembly	: Ideal.Security
// Author	: Rod Johnson
// Created	: 03-16-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System.IdentityModel;
using System.Web.Security;

namespace Ideal.Security.Encryption
{
    #region

    

    #endregion

    public class MachineKeyCookieTransform : CookieTransform
    {
        public override byte[] Decode(byte[] encoded)
        {
            return MachineKey.Unprotect(encoded);
            //return MachineKey.Decode(Encoding.UTF8.GetString(encoded), MachineKeyProtection.All);
        }

        public override byte[] Encode(byte[] value)
        {
            return MachineKey.Protect(value);
            //return Encoding.UTF8.GetBytes(MachineKey.Encode(value, MachineKeyProtection.All));
        }
    }
}

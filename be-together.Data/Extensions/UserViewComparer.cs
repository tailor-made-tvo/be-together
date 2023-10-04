// --------------------------------------------------------------------------------
// tailor-made software GmbH
// --------------------------------------------------------------------------------
// project:      Caching
// filename:     CacheDataComparer.cs
// --------------------------------------------------------------------------------
// 
// Created:      2014-05-06   13:56
// 
// Last changed: 2014-05-06   14:06
//               Torsten Vogt (tvo)
// --------------------------------------------------------------------------------

#region

using System;
using System.Collections.Generic;
using TMS.be_together.Data.Models;
using TMS.Common.Extensions;

#endregion

namespace TMS.be_together.Data.Extensions
{
    internal class UserViewComparer : IEqualityComparer<UserView>
    {
        #region IEqualityComparer<Contact> Members

        public bool Equals(UserView x, UserView y)
        {
            if (!(x.LoginId.IsNullOrWhiteSpace() || y.LoginId.IsNullOrWhiteSpace()))
                return x.LoginId.Equals(y.LoginId) && x.LoginId.Equals(y.LoginId);

            if (!(x.LoginName.IsNullOrWhiteSpace() || y.LoginName.IsNullOrWhiteSpace()))
                return x.LoginName.Equals(y.LoginId) && x.LoginName.Equals(y.LoginId);

            return false;
        }

        public int GetHashCode(UserView obj)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(obj, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashProductName = obj.LoginId == null ? 0 : obj.LoginId.GetHashCode();

            //Get hash code for the Code field.
            int hashProductCode = obj.LoginName == null ? 0 : obj.LoginName.GetHashCode();

            //Calculate the hash code for the product.
            return hashProductName ^ hashProductCode;
        }

        #endregion
    }
}
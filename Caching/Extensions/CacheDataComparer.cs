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
using TMS.Caching.Models;

#endregion

namespace TMS.Caching.Extensions
{
    internal class CacheDataComparer : IEqualityComparer<CacheData>
    {
        #region IEqualityComparer<Contact> Members

        public bool Equals(CacheData x, CacheData y)
        {
            return x.PartitionName.Equals(y.PartitionName) && x.Key.Equals(y.Key);
        }

        public int GetHashCode(CacheData obj)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(obj, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashProductName = obj.PartitionName == null ? 0 : obj.PartitionName.GetHashCode();

            //Get hash code for the Code field.
            int hashProductCode = obj.Key.GetHashCode();

            //Calculate the hash code for the product.
            return hashProductName ^ hashProductCode;
        }

        #endregion
    }
}
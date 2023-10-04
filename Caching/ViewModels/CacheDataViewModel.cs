// --------------------------------------------------------------------------------
// tailor-made software GmbH
// --------------------------------------------------------------------------------
// project:      Caching
// filename:     CacheDataViewModel.cs
// --------------------------------------------------------------------------------
// 
// Created:      2014-04-28   22:37
// 
// Last changed: 2014-05-06   11:05
//               Torsten Vogt (tvo)
// --------------------------------------------------------------------------------

#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EntityFramework.BulkInsert.Extensions;
using TMS.Caching.Annotations;
using TMS.Caching.Extensions;
using TMS.Caching.Models;
using TMS.Common.Extensions;
using TMS.Common.ViewModels;

#endregion

namespace TMS.Caching.ViewModels
{
    public class CacheDataViewModel : ViewModelBase
    {
        private CacheDataEntities _ent;

        #region IDisposable Members

        private bool _disposed;

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Close();
                }
            }
            _disposed = true;
        }

        #endregion

        public void Load(string partitionName)
        {
            Load(partitionName, new string[] {});
        }

        public void Load(string partitionName, string key)
        {
            Load(partitionName, new []{key});
        }

        public void Load(string partitionName, string[] keys)
        {
            if (partitionName.IsNullOrWhiteSpace())
                throw new ArgumentNullException("partitionName");

            if (_ent == null)
            {
                _ent = new CacheDataEntities();
            }
            if (keys != null && keys.Length > 0)
                Values = (from c in _ent.CacheData where c.PartitionName == partitionName && keys.Contains(c.Key) select c);
            else
                Values = (from c in _ent.CacheData where c.PartitionName == partitionName select c);
        }

        public void Load(List<CacheData> data)
        {
            if (data == null || data.Count == 0)
                throw new ArgumentNullException("data");

            if (_ent == null)
            {
                _ent = new CacheDataEntities();
            }

            var comparer = new CacheDataComparer();
            Values = _ent.CacheData.ToList().Where(c => data.Contains(c, comparer)).AsQueryable();
        }

        public void Close()
        {
            Values = null;
            if (_ent != null)
            {
                _ent.Dispose();
                _ent = null;
            }
        }

        public void BulkInsert(IEnumerable<CacheData> data)
        {
            if (_ent == null)
            {
                _ent = new CacheDataEntities();
            }
            _ent.BulkInsert(data, SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.CheckConstraints);
        }

        public int SaveChanges()
        {
            if (_ent == null)
                return 0;

            return _ent.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            if (_ent == null)
                return 0;

            return await _ent.SaveChangesAsync();
        }

        public IEnumerable<CacheData> AddRange(IEnumerable<CacheData> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            if (_ent == null)
                _ent = new CacheDataEntities();

            return _ent.CacheData.AddRange(entities);
        }

        public CacheData Add(CacheData entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (_ent == null)
                _ent = new CacheDataEntities();

            return _ent.CacheData.Add(entity);
        }

        public IEnumerable<CacheData> RemoveRange(IEnumerable<CacheData> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            if (_ent == null)
                _ent = new CacheDataEntities();

            return _ent.CacheData.RemoveRange(entities);
        }

        public CacheData Remove(CacheData entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (_ent == null)
                _ent = new CacheDataEntities();

            return _ent.CacheData.Remove(entity);
        }

        public IQueryable<CacheData> Values
        {
            get { return Get<IQueryable<CacheData>>(); }
            protected set { Set(value); }
        }
    }
}
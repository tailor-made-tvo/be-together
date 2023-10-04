// --------------------------------------------------------------------------------
// tailor-made software GmbH
// --------------------------------------------------------------------------------
// project:      be_together
// filename:     LoginViewModel.cs
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
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TMS.be_together.Data.Extensions;
using TMS.be_together.Data.Models;
using TMS.Common.Extensions;
using TMS.Common.ViewModels;
//using EntityFramework.BulkInsert.Extensions;

//using TMS.Common.Extensions;
//using TMS.Common.ViewModels;

#endregion

namespace TMS.be_together.Data.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        private UserEntities _ent;

        #region ctor

        public UserViewModel()
        {
            // This line should not be deleted, because EF6 has an error and will not be run after deleting this line
            var type = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
        }

        #endregion

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

        public void Load(string loginData)
        {
            if (loginData.IsNullOrWhiteSpace())
                throw new ArgumentNullException(string.Format("loginData = '{0}'", loginData));

            if (_ent == null)
            {
                _ent = new UserEntities();
            }
            Values = (from c in _ent.UserView where c.LoginId == loginData || c.LoginName == loginData select c);
        }

        public void Load(List<UserView> data)
        {
            if (data == null || data.Count == 0)
                throw new ArgumentNullException("data");

            if (_ent == null)
            {
                _ent = new UserEntities();
            }

            var comparer = new UserViewComparer();
            Values = _ent.UserView.ToList().Where(c => data.Contains(c, comparer)).AsQueryable();
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

        public IEnumerable<UserView> AddRange(IEnumerable<UserView> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            if (_ent == null)
                _ent = new UserEntities();

            return _ent.UserView.AddRange(entities);
        }

        public UserView Add(UserView entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (_ent == null)
                _ent = new UserEntities();

            return _ent.UserView.Add(entity);
        }

        public IEnumerable<UserView> RemoveRange(IEnumerable<UserView> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            if (_ent == null)
                _ent = new UserEntities();

            return _ent.UserView.RemoveRange(entities);
        }

        public UserView Remove(UserView entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (_ent == null)
                _ent = new UserEntities();

            return _ent.UserView.Remove(entity);
        }

        public IQueryable<UserView> Values
        {
            get { return Get<IQueryable<UserView>>(); }
            protected set { Set(value); }
        }
    }
}
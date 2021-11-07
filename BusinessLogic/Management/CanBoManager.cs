﻿using BusinessLogic.Model;
using Framework.Extensions;
using SQLDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Management
{
    public class CanBoManager
    {
        #region Singleton
        private static CanBoManager _instance;
        private CanBoManager() { }
        public static CanBoManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CanBoManager();
                }
                return _instance;
            }
        }
        #endregion

        #region  public
        public CanBoModel SelectById(Guid Id)
        {
            using (var uow = new UnitOfWork())
            {
                var cb = uow.Repository<CanBo>().Query().Filter(x => x.Id == Id).FirstOrDefault();
                if (cb != null)
                {
                    return cb.CopyAs<CanBoModel>();
                }
                return null;
            }
        }
        public CanBoModel SelectByUserName(string UserName)
        {
            using (var uow = new UnitOfWork())
            {
                var cb = uow.Repository<CanBo>().Query().Filter(x => x.UserName == UserName).FirstOrDefault();

                if (cb != null)
                {
                    return cb.CopyAs<CanBoModel>();
                }
                return null;
            }
        }


        #endregion

        #region private
        #endregion

    }
}

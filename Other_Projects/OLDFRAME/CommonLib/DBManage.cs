using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DefaultConnection;
using PetaPoco;

namespace MPAPI.CommonLib
{
    public class DBManage
    {
        private static Database _db;
        public static Database DB
        {
            get
            {
                if (_db == null)
                {
                    _db = DefaultConnection.DefaultConnectionDB.GetInstance();
                }
                return _db;
            }
        }
    }
}

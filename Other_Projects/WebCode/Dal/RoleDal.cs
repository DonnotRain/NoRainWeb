﻿using DefaultConnection;
using iBangTeam.Business.IDal;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iBangTeam.Business.Dal
{
    public class RoleDal :CommonSecurityDAL, IRoleDal
    {

        public void DeleteRoleUser(int roleId)
        {
            DB.Execute("DELETE FROM User_Role Where Role_ID=@0", roleId);
        }


        public void DeleteRoleFunction(int roleId)
        {
            DB.Execute("DELETE FROM Role_Function Where Role_ID=@0", roleId);
        }
    }
}

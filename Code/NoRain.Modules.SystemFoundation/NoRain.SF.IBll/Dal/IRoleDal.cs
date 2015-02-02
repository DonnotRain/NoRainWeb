﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoRain.Business.IDal
{
    public interface IRoleDal : ICommonSecurityDAL
    {
         void DeleteRoleUser(int roleId);
         void DeleteRoleFunction(int roleId);
    }
}

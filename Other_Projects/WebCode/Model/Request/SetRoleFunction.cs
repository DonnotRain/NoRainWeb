﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.HOP.Models.Request
{
    public class RoleFunctionSetting
    {
        public int RoleId { get; set; }

        public int[] FunctionId { get; set; }
    }
}
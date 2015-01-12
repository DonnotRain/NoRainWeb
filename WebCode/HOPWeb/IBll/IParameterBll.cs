using DefaultConnection;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.IBll
{
    public interface IParameterBll : ICommonBLL
    {
        Page<Parameter> GetParameterPager(int pageIndex, int pageSize, string name, string value);
        Parameter Add(Parameter Parameter);
        Parameter Edit(Parameter Parameter);

        Parameter GetByName(string paramName);
        Parameter GetSysName(string paramName);

        Parameter SetSysName(string sysName, string paramName);
    }
}

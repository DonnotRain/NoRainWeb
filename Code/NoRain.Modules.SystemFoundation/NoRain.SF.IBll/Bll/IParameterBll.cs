using DefaultConnection;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoRain.Business.IBll
{
    public interface IParameterBll : ICommonBLL
    {
        Page<SysParameter> GetParameterPager(int pageIndex, int pageSize, string name, string value);
        SysParameter Add(SysParameter Parameter);
        SysParameter Edit(SysParameter Parameter);

        SysParameter GetByName(string paramName);
        SysParameter GetSysName(string paramName);

        SysParameter SetSysName(string sysName, string paramName);

        Page<SysParameter> GetParameterPager(Model.Request.DataTablesRequest reqestParams);
    }
}

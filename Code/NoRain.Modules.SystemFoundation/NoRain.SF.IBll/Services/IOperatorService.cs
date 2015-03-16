using DefaultConnection;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NoRain.Business.IService
{
    public interface ISysUserService : ICommonService
    {
        Page<SysUser> GetSysUserPager(int page, int rows, string name);
        SysUser Add(SysUser entity);
        SysUser Edit(SysUser entity);

        SysUser ChangePwd(string password, string newPassword, string confrimPassword);
    }
}

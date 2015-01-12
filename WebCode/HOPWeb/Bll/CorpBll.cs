using DefaultConnection;
using HuaweiSoftware.WQT.IBll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WQTRights;

namespace HuaweiSoftware.WQT.Bll
{
    public class CorpBll : CommonBLL, ICorpBll
    {
        ICommonSecurityBLL m_securityBll;
        public CorpBll(ICommonSecurityBLL securityBll)
        {
            m_securityBll = securityBll;
        }
        public Corporation Register(Corporation corp, string realName, string adminName, string password)
        {
            corp.CreateTime = DateTime.Now;
            corp.ModifyTime = DateTime.Now;
            corp.IsDeleted = false;

            //判断名字，编号是否重复
            var oldCorp = Find<Corporation>("Where Name=@0 or Code=@1", corp.Name, corp.Code);
            if (oldCorp != null)
            {
                throw new Exception("公司名称或代码已存在!");
            }
            Insert<Corporation>(corp);
            User user = new User();
            user.FullName = realName;
            user.Name = adminName;
            user.Password = CommonToolkit.CommonToolkit.GetMD5Password(password);
            user.Remark = "";
            user.CorpCode = corp.Code;

            var maxId = m_securityBll.Find<User>("select * from Users where id=(select max(id) from users)").ID;
            user.ID = maxId + 1;
            m_securityBll.Insert<User>(user);

            return corp;
        }
    }
}

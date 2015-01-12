using DefaultConnection;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.IBll
{
    public interface IStoreBll : ICommonBLL
    {
        Page<SEC_Store> GetStorePager(int page, int rows, int? begin, int? end, string name, string address, string belongTo);
        SEC_Store Add(SEC_Store store);
        SEC_Store Edit(SEC_Store store);
    }
}

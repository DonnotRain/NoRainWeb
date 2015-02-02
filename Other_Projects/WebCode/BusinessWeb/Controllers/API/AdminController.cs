using Security_ConnectionString;
using iBangTeam.Business.IBll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using WebBase;

namespace BusinessWeb.Controllers.API
{
    public class AdminController : ApiController
    {
        ICommonBLL _mBaseBll = DPResolver.Resolver<ICommonBLL>();

        // GET api/Operator
        public IEnumerable<Operator> Get()
        {
            return _mBaseBll.FindAll<Operator>(null);
        }

        // GET api/Operator/5
        public Operator Get(int id)
        {
            return _mBaseBll.Find<Operator>(id.ToString());
        }

        // POST api/Operator
        public void Post(Operator @operator)
        {
            _mBaseBll.Update(@operator);
        }

        // PUT api/Operator/5
        public void Put(int id, Operator @operator)
        {
            _mBaseBll.Insert(@operator);
        }

        // DELETE api/Operator/5
        public void Delete(int id)
        {
            _mBaseBll.Delete(Get(id));
        }
    }
}

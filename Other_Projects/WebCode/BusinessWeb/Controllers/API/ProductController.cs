using  Security_ConnectionString;
using iBangTeam.Business.IBll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using DefaultConnection;
using WebBase;

namespace BusinessWeb.Controllers.API
{
    public class ProductController : ApiController
    {
        private ICommonBLL _mBaseBll = DPResolver.Resolver<ICommonBLL>();

        // GET api/product
        public IEnumerable<Product> Get()
        {
            return _mBaseBll.FindAll<Product>(null);
        }

        // GET api/product/5
        public Product Get(int id)
        {
            return _mBaseBll.Find<Product>("id=@0", id);
        }

        // POST api/product
        public void Post(Product product)
        {
            if (string.IsNullOrEmpty(product.ProductName))
            {
                throw new ApiException(ResponseCode.SYSTEM_PARAMETER_REQUIRED, "缺少参数");
            }
            product.ProductId = Guid.NewGuid();

            _mBaseBll.Update(product);
        }

        // PUT api/product/5
        public void Put(int id, Product product)
        {
            _mBaseBll.Insert(product);
        }

        // DELETE api/product/5
        public void Delete(int id)
        {
            _mBaseBll.Delete(Get(id));
        }
    }
}

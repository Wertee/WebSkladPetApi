using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Product.Services;
using Tests.Common;

namespace Tests.ProductTests
{
    public abstract class TestProductServices : TestServicesBase
    {
        protected readonly ProductService Service;

        protected TestProductServices()
        {
            Service = ProductServiceCreator.CreateService(Context);
        }
    }
}

using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public ProductController(IAppBLL bll)
        {
            _bll = bll;
        }
    }
}
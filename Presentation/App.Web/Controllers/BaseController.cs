using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json;

namespace App.Web.Controllers
{
    public class BaseController : Controller
    {
        public virtual JsonResult DataTableJson(object model)
        {
            return Json(model, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
                WriteIndented = true
            });
        }
    }
}

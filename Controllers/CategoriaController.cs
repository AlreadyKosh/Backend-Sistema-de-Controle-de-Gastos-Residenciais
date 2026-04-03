using Microsoft.AspNetCore.Mvc;

namespace Backend_Sistema_de_Controle_de_Gastos_Residenciais.Controllers
{
    public class CategoriaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

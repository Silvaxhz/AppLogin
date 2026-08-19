using Microsoft.AspNetCore.Mvc;

namespace ProjetoNilson4.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    public class ColaboradorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
    
}


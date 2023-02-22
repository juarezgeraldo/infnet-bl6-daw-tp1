using infnet_bl6_daw_tp1.Domain.Entities;
using infnet_bl6_daw_tp1.Domain.Interfaces;
using infnet_bl6_daw_tp1.Service.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace infnet_bl6_daw_tp1.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAmigoService _amigoService;

        public HomeController(ILogger<HomeController> logger, IAmigoService amigoService)
        {
            _logger = logger;
            _amigoService = amigoService;
        }

        public async Task<IActionResult> Index(string nomePesquisa)
        {
            var amigos = _amigoService.GetAll();

            if (!string.IsNullOrEmpty(nomePesquisa))
            {
                amigos = amigos.Where(amigo => amigo.NomeCompletoPossui(nomePesquisa)).ToList();
            }
            amigos.Sort(delegate (Amigo x, Amigo y)
            {
                return x.DiasFaltantes.CompareTo(y.DiasFaltantes);
            });

            return View(amigos);
        }

        public IActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Incluir([Bind("Id,Nome,Sobrenome,Email,Nascimento")] Amigo amigo)
        {
            if (ModelState.IsValid)
            {
                _amigoService.Add(amigo);
//                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(amigo);
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
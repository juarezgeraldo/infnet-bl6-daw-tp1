using infnet_bl6_daw_tp1.Domain.Entities;
using infnet_bl6_daw_tp1.Domain.Interfaces;
using infnet_bl6_daw_tp1.Service.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity.Infrastructure;
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

        public IActionResult Index(string nomePesquisa)
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
        public IActionResult Incluir([Bind("Id,Nome,Sobrenome,Email,Nascimento")] Amigo amigo)
        {
            if (ModelState.IsValid)
            {
                _amigoService.Add(amigo);

                return RedirectToAction(nameof(Index));
            }
            return View(amigo);
        }

        public IActionResult Alterar(int? id)
        {
            if (id == null || _amigoService == null)
            {
                return NotFound();
            }

            var amigos = _amigoService.GetAll();
            var indice = amigos.FindIndex(amigo => amigo.Id == id);
            if (indice < 0) { return NotFound(); }

            return View(amigos[indice]);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(int id, [Bind("Id,Nome,Sobrenome,Email,Nascimento")] Amigo amigo)
        {
            if (id != amigo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _amigoService.Save(amigo);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExisteAmigo(amigo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(amigo);
        }

        private bool ExisteAmigo(int id)
        {
            return _amigoService.GetAll().Any(e => e.Id == id);
        }


        public IActionResult Exibir(int? id)
        {
            if (id == null || _amigoService == null)
            {
                return NotFound();
            }

            var amigos = _amigoService.GetAll();
            var indice = amigos.FindIndex(amigo => amigo.Id == id);
            if (indice < 0) { return NotFound(); }

            return View(amigos[indice]);
        }

        public IActionResult Excluir(int? id)
        {
            if (id == null || _amigoService == null)
            {
                return NotFound();
            }

            var amigos = _amigoService.GetAll();
            var indice = amigos.FindIndex(amigo => amigo.Id == id);
            if (indice < 0) { return NotFound(); }

            return View(amigos[indice]);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarExcluir(int id)
        {
            if (_amigoService == null)
            {
                return Problem("Entity set 'infnet_bl6_fdaN_atContext.Amigo'  is null.");
            }
            var amigos = _amigoService.GetAll();
            var indice = amigos.FindIndex(amigo => amigo.Id == id);
            if (indice < 0) { return NotFound(); }

            _amigoService.Remove(amigos[indice]);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Privacy()
        {
            return View();
        }

    }
}
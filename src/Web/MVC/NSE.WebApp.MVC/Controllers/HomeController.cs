using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Controllers;

    public class HomeController: MainController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("system-temporarily-unavailable")]
        public IActionResult UnavailableSystem()
        {
            var modelError = new ErrorViewModel
            {
                Message = "O sistema está temporariamente indisponível! Tente novamente mais tarde ou contate nosso suporte.",
                Title = "Sistema indisponível!",
                ErrorCode = 500,
            };

            return View("Error", modelError);
        }

        [Route("error/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelError = new ErrorViewModel();

            if (id == 500)
            {
                modelError.Message = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                modelError.Title = "Ocorreu um erro!";
                modelError.ErrorCode = id;
            }
            else if (id == 404)
            {
                modelError.Message =
                    "A página que está procurando não existe! <br />Em caso de dúvidas entre em contato com nosso suporte";
                modelError.Title = "Ops! Página não encontrada.";
                modelError.ErrorCode = id;
            }
            else if (id == 403)
            {
                modelError.Message = "Você não tem permissão para fazer isto.";
                modelError.Title = "Acesso Negado";
                modelError.ErrorCode = id;
            }
            else
            {
                return StatusCode(404);
            }

            return View("Error", modelError);
        }
    }

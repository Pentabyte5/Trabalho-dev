using Microsoft.AspNetCore.Mvc;
using Petshop5.Models;
using System.Diagnostics;

namespace Petshop5.Controllers
{
    public class ContaController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string senha)
        {
            //Users and Passwords
            string emailCorreto = "admin@email.com";
            string senhaCorreta = "123456";
            
            //Validator

            if (email == emailCorreto && senha == senhaCorreta)
            {
                //instancia

                Funcionario funcionario = new Funcionario();
                
                //return to main menu
                
                return RedirectToAction("Index", "Home");
            }
            
            ModelState.AddModelError("", "Email ou senha incorreta");
            return View();
        }
    }
}
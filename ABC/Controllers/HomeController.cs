using ABC.Models;
using ABC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace ABC.Controllers
{
    public class HomeController : Controller
    {
        private readonly EXAMPLEDATAContext _DBContext;

        public HomeController(EXAMPLEDATAContext context)
        {
            _DBContext = context;
        }

        public IActionResult Index()
        {
            List<Client> lista = _DBContext.Clients.ToList();
            return View(lista);
        }

        [HttpGet]
        public IActionResult client_details(int Idclient)
        {
            ClientVM oClientVM = new ClientVM();
            if ( Idclient != 0 ) 
            {
                oClientVM.oClient = _DBContext.Clients.Find(Idclient);
            }
            return View(oClientVM);
        }

        [HttpPost]
        public IActionResult client_details(ClientVM oclientVM)
        {
            if (oclientVM.oClient.Id == 0)
            {
                _DBContext.Clients.Add(oclientVM.oClient);
            }
            else
            {
                _DBContext.Clients.Update(oclientVM.oClient);
            }


            _DBContext.SaveChanges(true);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Delete(int Idclient)
        {
            Client oclient = _DBContext.Clients.Where(e => e.Id == Idclient).FirstOrDefault();
            return View(oclient);
        }

        [HttpPost]
        public IActionResult Delete (Client oclient)
        {
          
            _DBContext.Clients.Remove(oclient);
            _DBContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}

using ABC.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABC.Controllers
{
    public class RelatorioArmazemController : Controller
    {

        private Db db = new Db();
        // GET: RelatorioArmazem
        public ActionResult Index()
        {
            var totalpedido = db.Pedido.ToList();
            ViewBag.TotalP = totalpedido.Count;

            return View(db.Armazem.ToList());
        }

        public ActionResult Details(int id)
        {
            var ra = db.RelatorioArmazemDetalhes.Where(x => x.ArmazemId == id).ToList() ;
            var total = ra.Sum(x => x.valor);
           
            RelatorioArmazem arm = new RelatorioArmazem();
            foreach (var item in ra)
            {
                
                arm.valor = item.valor;
                arm.Nome = item.Nome;
              
            }
           
            return View(arm);
        }
    }
}
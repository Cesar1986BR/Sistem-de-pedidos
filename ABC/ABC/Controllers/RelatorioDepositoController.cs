using ABC.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABC.Controllers
{
    public class RelatorioDepositoController : Controller
    {
        private Db db = new Db();
        // GET: RelatorioDeposito
        public ActionResult Index()
        {
            return View(db.Deposito.ToList());
        }

        public ActionResult Details(int id)
        {
            var de = db.RelatorioDepositosDetalhes.Where(x => x.DepositoId == id).ToList();
            var total = de.Sum(x => x.valor);
            RelatorioDeposito dep = new RelatorioDeposito();
            foreach (var item in de)
            {
                dep.valor = item.valor;
                dep.Nome = item.Nome;
            
            }

            return View(dep);
        }
    }
}
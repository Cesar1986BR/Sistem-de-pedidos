using ABC.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABC.Controllers
{
    public class PedidosController : Controller
    {
        private Db db = new Db();
        // GET: Pedidos
        public ActionResult Index()
        {
            return View(db.Pedido.ToList());
        }

        // GET: Pedidos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Pedidos/Create
        public ActionResult Create()
        {

            Db db = new Db();

            ViewBag.ClienteId = new SelectList(db.Cliente.ToList(), "Id", "Nome");
            ViewBag.ProdutoId = new SelectList(db.Produto.ToList(), "Id", "Nome");
            return View();
        }

        // POST: Pedidos/Create
        [HttpPost]
        public ActionResult Create(Pedido model)
        {
            int qtdEstoque=0;

            var estoques = db.Estoque.Where(x => x.ProdutoId == model.ProdutoId).ToList();

            foreach (var estoque in estoques)
            {
                qtdEstoque += estoque.Quantidade;
            };

            Produto produto = db.Produto.Where(x => x.Id == model.ProdutoId).FirstOrDefault();
            decimal precoUnitario = produto.Preco;

            if (model.Quantidade > qtdEstoque)
            {
              
                TempData["erro"] = string.Format("Estoque insuficiente, Total em estoque {0} ", qtdEstoque);
               
            }
            else
            {
                Estoque es = db.Estoque.Find(model.ProdutoId);
                es.Quantidade = es.Quantidade - model.Quantidade;
                db.SaveChanges();

                model.Quantidade = qtdEstoque;
                model.PrecoUnidade = precoUnitario;

                if (ModelState.IsValid)
                {
                    db.Pedido.Add(model);

                    db.SaveChanges();
                }
                TempData["Ok"] = "Pedido cadastrado sucesso!";
            }

            ViewBag.ClienteId = new SelectList(db.Cliente.ToList(), "Id", "Nome");
            ViewBag.ProdutoId = new SelectList(db.Produto.ToList(), "Id", "Nome");

            return RedirectToAction("Create");
            
        }

        // GET: Pedidos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Pedidos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pedidos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pedidos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

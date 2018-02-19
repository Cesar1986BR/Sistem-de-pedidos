using ABC.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Collections;

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
            Pedido pedido = db.Pedido.Find(id);
            return View(pedido);
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
         
            var prod = db.Estoque.Where(x => x.ProdutoId == model.ProdutoId).ToList();

            var armazen = prod.OrderBy(x => x.Deposito.Armazem.Prazo).FirstOrDefault();
            foreach (var item in prod)
            {
                qtdEstoque += item.Quantidade;
            };

            Produto produto = db.Produto.Where(x => x.Id == model.ProdutoId).FirstOrDefault();
            decimal precoUnitario = produto.Preco;

            if (model.Quantidade > qtdEstoque)
            {
                TempData["erro"] = string.Format("Estoque insuficiente, Total em estoque {0} ", qtdEstoque); 
            }
            else
            {
                Estoque es = db.Estoque.Find(armazen.Id);
                es.Quantidade = es.Quantidade - model.Quantidade;
                db.SaveChanges();

                model.Quantidade = model.Quantidade;
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

            Pedido pedido = db.Pedido.Find(id);

            ViewBag.ClienteId = new SelectList(db.Cliente.ToList(), "Id", "Nome",pedido.ClienteId);
            ViewBag.ProdutoId = new SelectList(db.Produto.ToList(), "Id", "Nome",pedido.ProdutoId);
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id,Pedido pedido)
        {

            int qtdEstoque = 0;

            var prod = db.Estoque.Where(x => x.ProdutoId == pedido.ProdutoId).ToList();

            var armazen = prod.OrderBy(x => x.Deposito.Armazem.Prazo).FirstOrDefault();
            foreach (var item in prod)
            {
                qtdEstoque += item.Quantidade;
            };

            Produto produto = db.Produto.Where(x => x.Id == pedido.ProdutoId).FirstOrDefault();
            decimal precoUnitario = produto.Preco;

            if (pedido.Quantidade > qtdEstoque)
            {
                TempData["erro"] = string.Format("Estoque insuficiente, Total em estoque {0} ", qtdEstoque);
            }
            else
            {
                Estoque es = db.Estoque.Find(armazen.Id);
                es.Quantidade = es.Quantidade - pedido.Quantidade;
                db.SaveChanges();

                pedido.Quantidade = pedido.Quantidade;
                pedido.PrecoUnidade = precoUnitario;

                if (ModelState.IsValid)
                {
                    db.Entry(pedido).State = EntityState.Modified;
                    db.SaveChanges();
                }
                TempData["Ok"] = "Pedido Alterado sucesso!";
            }

            ViewBag.ClienteId = new SelectList(db.Cliente.ToList(), "Id", "Nome");
            ViewBag.ProdutoId = new SelectList(db.Produto.ToList(), "Id", "Nome");

            return RedirectToAction("Edit");




        }

        // GET: Pedidos/Delete/5
        public ActionResult Delete(int id)
        {
            Pedido pedido = db.Pedido.Find(id);
            return View(pedido);

        }

        // POST: Pedidos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id,Pedido pedido)
        {
            pedido = db.Pedido.Find(id);
            db.Pedido.Remove(pedido);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

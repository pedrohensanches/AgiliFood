using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AgileFood.Models;

namespace AgileFood.Controllers
{
    public class PedidosController : Controller
    {
        private AgiliFoodContext db = new AgiliFoodContext();
        private List<Produto> produtos = new List<Produto>();

        // GET: Pedidos
        public ActionResult Index()
        {
            var pedidos = db.Pedidos.Include(p => p.Funcionario);
            return View(pedidos.ToList());
        }

        // GET: Pedidos/Details/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // GET: Pedidos/Adicionar
        public ActionResult Adicionar()
        {
            Fornecedor fornecedor = TempData["Fornecedor"] as Fornecedor;

            if (fornecedor == null)
            {
                return RedirectToAction("Index", "Pedidos");
            }

            ViewBag.Marmitex = db.Produtos.Where(g => (g.Fornecedor.Id == fornecedor.Id) &&
            (g.Disponivel) && (g.Categoria == Categoria.Marmitex)).ToList();
            ViewBag.Bebidas = db.Produtos.Where(g => (g.Fornecedor.Id == fornecedor.Id) &&
            (g.Disponivel) && (g.Categoria == Categoria.Bebidas)).ToList();
            ViewBag.Sobremesas = db.Produtos.Where(g => (g.Fornecedor.Id == fornecedor.Id) &&
            (g.Disponivel) && (g.Categoria == Categoria.Sobremesas)).ToList();
            ViewBag.Outros = db.Produtos.Where(g => (g.Fornecedor.Id == fornecedor.Id) &&
            (g.Disponivel) && (g.Categoria == Categoria.Outros)).ToList();
            ViewBag.Cardapio = GetCardapioDaSemana(fornecedor.Id);
            ViewBag.FornecedorNome = fornecedor.Nome;
            ViewBag.FuncionarioId = new SelectList(db.Usuarios, "Id", "Nome");

            return View();
        }

        // POST: Pedidos/Adicionar
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar([Bind(Include = "Id,DataDeRegistro,Observacoes,FuncionarioId")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                pedido.DataDeRegistro = DateTime.Now;
                db.Pedidos.Add(pedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FuncionarioId = new SelectList(db.Usuarios, "Id", "Nome", pedido.FuncionarioId);
            return View(pedido);
        }

        // GET: Pedidos/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            ViewBag.FuncionarioId = new SelectList(db.Usuarios, "Id", "Nome", pedido.FuncionarioId);
            return View(pedido);
        }

        // POST: Pedidos/Editar/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,DataDeRegistro,Observacoes,FuncionarioId")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FuncionarioId = new SelectList(db.Usuarios, "Id", "Nome", pedido.FuncionarioId);
            return View(pedido);
        }

        // GET: Pedidos/Deletar/5
        public ActionResult Deletar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // POST: Pedidos/Deletar/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult Confirmar(int id)
        {
            Pedido pedido = db.Pedidos.Find(id);
            db.Pedidos.Remove(pedido);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void AdicionarProdutoAoPedido(Produto produto)
        {
            produtos.Add(produto);
        }

        private List<Tuple<string, string>> GetCardapioDaSemana(int id)
        {
            Cardapio cardapio = (Cardapio)db.Cardapios.Where(g => (g.Fornecedor.Id == id) && (g.Ativo)).First();

            List<Tuple<string, string>> lista = new List<Tuple<string, string>>();

            if (cardapio.SegundaFeira != null)
            {
                lista.Add(new Tuple<string, string>("Segunda-Feira", cardapio.SegundaFeira));
            }
            if (cardapio.TercaFeira != null)
            {
                lista.Add(new Tuple<string, string>("Terça-Feira", cardapio.TercaFeira));
            }
            if (cardapio.QuartaFeira != null)
            {
                lista.Add(new Tuple<string, string>("Quarta-Feira", cardapio.QuartaFeira));
            }
            if (cardapio.QuintaFeira != null)
            {
                lista.Add(new Tuple<string, string>("Quinta-Feira", cardapio.QuintaFeira));
            }
            if (cardapio.SextaFeira != null)
            {
                lista.Add(new Tuple<string, string>("Sexta-Feira", cardapio.SextaFeira));
            }
            if (cardapio.Sabado != null)
            {
                lista.Add(new Tuple<string, string>("Sábado", cardapio.Sabado));
            }
            if (cardapio.Domingo != null)
            {
                lista.Add(new Tuple<string, string>("Domingo", cardapio.Domingo));
            }

            return lista;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

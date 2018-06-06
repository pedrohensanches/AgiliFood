using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AgileFood.Models;
using AgileFood.Repositorios;

namespace AgileFood.Controllers
{
    public class PedidosController : BaseController
    {
        private AgiliFoodContext db = new AgiliFoodContext();

        // GET: Pedidos
        public ActionResult Index()
        {
            Session["Pedido"] = null;
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
            if (fornecedor == null) return RedirectToAction("Index", "Pedidos");
            AdicionandoInformacoesAoViewBag(fornecedor);
            return View();
        }

        private void AdicionandoInformacoesAoViewBag(Fornecedor fornecedor)
        {
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
        }

        // POST: Pedidos/Adicionar
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar([Bind(Include = "Id,DataDeRegistro,Observacoes,FuncionarioId")] Pedido pedido)
        {
            string observacoes = pedido.Observacoes;
            pedido = ((Pedido)Session["Pedido"]);
            if (ModelState.IsValid)
            {
                pedido.Observacoes = observacoes;
                pedido.Funcionario = RepositorioUsuarios.UsuarioLogado();
                pedido.DataDeRegistro = DateTime.Now;
                AnexandoDadosAoContexto(pedido);
                db.Pedidos.Add(pedido);
                db.SaveChanges();
                Session["Pedido"] = null;
                return RedirectToAction("Index");
            }
            return View(pedido);
        }

        // Método necessário para informar ao contexto db que esses dados já existem no banco de dados, 
        // evitando serem duplicados
        private void AnexandoDadosAoContexto(Pedido pedido)
        {
            db.Usuarios.Attach(pedido.Funcionario);
            foreach (ItemPedido ip in pedido.Itens)
            {
                db.Produtos.Attach(ip.Produto);
            }
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

        public PartialViewResult AdicionarProdutoAoPedido(int id)
        {
            Pedido pedido = (Session["Pedido"] == null) ? new Pedido() { ValorTotal = 0 } : (Pedido)Session["Pedido"];
            var produto = db.Produtos.Find(id);
            if (produto != null)
            {
                var itemPedido = new ItemPedido
                {
                    Produto = produto,
                    Pedido = pedido,
                    Quantidade = 1

                };
                if (pedido.Itens.FirstOrDefault(x => x.Produto.Equals(produto)) != null)
                {
                    pedido.Itens.FirstOrDefault(x => x.Produto.Equals(produto)).Quantidade += 1;
                }
                else
                {
                    pedido.Itens.Add(itemPedido);
                }
                pedido.ValorTotal += produto.Valor;
                Session["Pedido"] = pedido;
            }
            return PartialView("_ItensDoPedido", pedido);
        }

        public PartialViewResult RemoverProdutoDoPedido(int id)
        {
            Pedido pedido = ((Pedido)Session["Pedido"]);
            Produto produto = new Produto
            {
                Id = id
            };
            ItemPedido item = pedido.Itens.FirstOrDefault(x => x.Produto.Equals(produto));
            if (item.Quantidade > 1)
            {
                pedido.Itens.FirstOrDefault(x => x.Produto.Equals(produto)).Quantidade -= 1;
            }
            else
            {
                pedido.Itens.Remove(pedido.Itens.FirstOrDefault(x => x.Produto.Equals(produto)));
            }
            pedido.ValorTotal -= item.Produto.Valor;
            Session["Pedido"] = pedido;
            return PartialView("_ItensDoPedido", pedido);
        }

        private List<Tuple<string, string>> GetCardapioDaSemana(int id)
        {
            Cardapio cardapio = (Cardapio)db.Cardapios.Where(g => (g.Fornecedor.Id == id) && (g.Ativo)).First();
            List<Tuple<string, string>> lista = new List<Tuple<string, string>>();
            if (cardapio.SegundaFeira != null) lista.Add(new Tuple<string, string>("Segunda-Feira", cardapio.SegundaFeira));
            if (cardapio.TercaFeira != null) lista.Add(new Tuple<string, string>("Terça-Feira", cardapio.TercaFeira));
            if (cardapio.QuartaFeira != null) lista.Add(new Tuple<string, string>("Quarta-Feira", cardapio.QuartaFeira));
            if (cardapio.QuintaFeira != null) lista.Add(new Tuple<string, string>("Quinta-Feira", cardapio.QuintaFeira));
            if (cardapio.SextaFeira != null) lista.Add(new Tuple<string, string>("Sexta-Feira", cardapio.SextaFeira));
            if (cardapio.Sabado != null) lista.Add(new Tuple<string, string>("Sábado", cardapio.Sabado));
            if (cardapio.Domingo != null) lista.Add(new Tuple<string, string>("Domingo", cardapio.Domingo));
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

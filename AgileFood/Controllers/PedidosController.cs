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
        public ActionResult Index(int? pesquisaMes, int? pesquisaAno)
        {
            List<Pedido> pedidos = RepositorioPedidos.RetornaPedidosDoFuncionarioLogado(db.Pedidos.Include(p => p.Funcionario).Include(p => p.Itens), pesquisaMes, pesquisaAno);
            if (Request.IsAjaxRequest()) return PartialView("_Pedidos", pedidos);
            Session["Pedido"] = null;
            ViewBag.fornecedores = db.Fornecedores.Where(x => x.Ativo == true).ToList();
            return View(pedidos);
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
            ViewBag.Marmitex = RepositorioProdutos.RetornaMarmitex(fornecedor.Id);
            ViewBag.Bebidas = RepositorioProdutos.RetornaBebidas(fornecedor.Id);
            ViewBag.Sobremesas = RepositorioProdutos.RetornaSobremesas(fornecedor.Id);
            ViewBag.Outros = RepositorioProdutos.RetornaOutrosProdutos(fornecedor.Id);
            ViewBag.Cardapio = RepositorioCardapios.GetCardapioDaSemana(fornecedor.Id);
            ViewBag.FornecedorNome = fornecedor.Nome;
        }

        // POST: Pedidos/Adicionar
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar([Bind(Include = "Id, Observacoes")] Pedido pedido)
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
            Produto produto = new Produto { Id = id };
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

        public ActionResult Escolher(Fornecedor fornecedor)
        {
            if (fornecedor == null) return HttpNotFound();
            TempData["Fornecedor"] = fornecedor;
            return RedirectToAction("Adicionar", "Pedidos");
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

using AgileFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgileFood.Repositorios
{
    public class RepositorioProdutos
    {
        public static List<Produto> RetornaMarmitex(int id)
        {
            try
            {
                using (AgiliFoodContext db = new AgiliFoodContext())
                {
                    return db.Produtos.Where(g => (g.Fornecedor.Id == id) &&
                    (g.Disponivel) && (g.Categoria == Categoria.Marmitex)).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<Produto> RetornaProdutos(int? pesquisaStatus, int? pesquisaCategoria, IQueryable<Usuario> usuarios)
        {
            try
            {
                using (AgiliFoodContext db = new AgiliFoodContext())
                {
                    var produtos = db.Produtos.AsQueryable();
                    switch (pesquisaStatus)
                    {
                        case 0: produtos = produtos.Where(u => u.Disponivel == false); break;
                        case 1: produtos = produtos.Where(u => u.Disponivel == true); break;
                    }
                    if (pesquisaCategoria != null) produtos = produtos.Where(u => (int)u.Categoria == pesquisaCategoria);
                    Fornecedor fornecedor = RepositorioFornecedores.RecuperaFornecedorLogado(usuarios);
                    produtos = produtos.Where(u => u.FornecedorId == fornecedor.Id);
                    return produtos.OrderBy(u => u.Nome).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<Produto> RetornaBebidas(int id)
        {
            try
            {
                using (AgiliFoodContext db = new AgiliFoodContext())
                {
                    return db.Produtos.Where(g => (g.Fornecedor.Id == id) &&
                    (g.Disponivel) && (g.Categoria == Categoria.Bebidas)).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<Produto> RetornaSobremesas(int id)
        {
            try
            {
                using (AgiliFoodContext db = new AgiliFoodContext())
                {
                    return db.Produtos.Where(g => (g.Fornecedor.Id == id) &&
                    (g.Disponivel) && (g.Categoria == Categoria.Sobremesas)).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<Produto> RetornaOutrosProdutos(int id)
        {
            try
            {
                using (AgiliFoodContext db = new AgiliFoodContext())
                {
                    return db.Produtos.Where(g => (g.Fornecedor.Id == id) &&
                    (g.Disponivel) && (g.Categoria == Categoria.Outros)).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
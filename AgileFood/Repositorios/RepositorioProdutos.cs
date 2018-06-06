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
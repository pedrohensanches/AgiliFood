using AgileFood.Models;
using AgileFood.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AgileFood.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.TotalPedidosNoMes = RepositorioPedidos.TotalPedidosNoMes();
            ViewBag.ValorTotalPedidosNoMes = RepositorioPedidos.ValorTotalPedidosNoMes();
            ViewBag.TotalPedidosHoje = RepositorioPedidos.TotalPedidosHoje();
            ViewBag.ValorTotalPedidosHoje = RepositorioPedidos.ValorTotalPedidosHoje();
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Title = "Seja bem vindo(a)!";
            return View();
        }

    }
}
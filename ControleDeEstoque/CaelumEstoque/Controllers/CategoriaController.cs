using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaelumEstoque.DAO;
using CaelumEstoque.Models;

namespace CaelumEstoque.Controllers
{
    public class CategoriaController : Controller
    {
        // GET: Categoria
        public ActionResult Index()
        {
            IList<CategoriaDoProduto> categorias = new CategoriasDAO().Lista();
            ViewBag.Categorias = categorias;
            return View();
        }
    }
}
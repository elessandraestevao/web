using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaelumEstoque.DAO;
using CaelumEstoque.Models;

namespace CaelumEstoque.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        public ActionResult Index()
        {
            IList<Produto> produtos = new ProdutosDAO().Lista();
            ViewBag.Produtos = produtos;
            return View();
        }

        public ActionResult Form()
        {
            ViewBag.Categorias = new CategoriasDAO().Lista();
            ViewBag.Produto = new Produto();
            return View();
        }

        [HttpPost]
        public ActionResult Adiciona(Produto produto)
        {
            int informaticaId = 1;
            if (produto.CategoriaId.Equals(informaticaId) && produto.Preco < 100)
            {
                ModelState.AddModelError("produto.PrecoMenorQueCem", "O preço para Informática deve ser no mínimo 100 reais");
            }

            if (ModelState.IsValid)
            {
                new ProdutosDAO().Adiciona(produto);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Categorias = new CategoriasDAO().Lista();
                ViewBag.Produto = produto;
                return View("Form");
            }
            
        }
    }
}
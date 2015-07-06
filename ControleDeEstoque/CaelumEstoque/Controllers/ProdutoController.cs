using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaelumEstoque.DAO;
using CaelumEstoque.Models;
using CaelumEstoque.Filtros;

namespace CaelumEstoque.Controllers
{
    [AutorizacaoFilter]
    public class ProdutoController : Controller
    {
        // GET: Produto
        [Route("produtos", Name="ListaProdutos")]
        public ActionResult Index()
        {
            IList<Produto> produtos = new ProdutosDAO().Lista();            
            return View(produtos);
        }

        public ActionResult Form()
        {
            ViewBag.Categorias = new CategoriasDAO().Lista();
            ViewBag.Produto = new Produto();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

        [Route("produtos/{id}", Name="VisualizaProduto")]
        public ActionResult Visualiza(int id)
        {
            ViewBag.Produto = new ProdutosDAO().BuscaPorId(id);
            return View();
        }

        public ActionResult DecrementaQtd(int id)
        {
            ProdutosDAO dao = new ProdutosDAO();
            Produto produto = dao.BuscaPorId(id);
            produto.Quantidade--;
            dao.Atualiza(produto);            
            return Json(produto);
        }
    }
}
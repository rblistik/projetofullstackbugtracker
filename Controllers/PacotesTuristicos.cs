using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using marketb.Models;

namespace marketb.Controllers
{
    public class PacotesTuristicosController : Controller
    {
        public IActionResult Remover(int Id)
        {
            PacotesTuristicosRepository p = new PacotesTuristicosRepository();
            PacotesTuristicos pacoteEncontrado=p.BuscarporId(Id);
            p.Excluir(pacoteEncontrado);
            return RedirectToAction("Lista", "PacotesTuristicos");

        }
        public IActionResult Editar(int Id)
        {
            PacotesTuristicosRepository p = new PacotesTuristicosRepository();
            
            return View(p.BuscarporId(Id));

        }
        [HttpPost]
        public IActionResult Editar(PacotesTuristicos pacotes){
            PacotesTuristicosRepository p = new PacotesTuristicosRepository();
            p.Alterar(pacotes);
            return RedirectToAction("Lista", "PacotesTuristicos");
        }
        public IActionResult Lista()
        {
            PacotesTuristicosRepository p = new PacotesTuristicosRepository();
            List<PacotesTuristicos> Lista = p.Listar();
            return View(Lista);
        }

        public IActionResult Cadastro()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastro(PacotesTuristicos pacotes)
        {

            PacotesTuristicosRepository p = new PacotesTuristicosRepository();
            p.Inserir(pacotes);
            ViewBag.Mensagem = "Cadastrado";
            return View();
        }
    }
}
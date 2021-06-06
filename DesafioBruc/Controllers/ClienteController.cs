using System;
using DesafioBruc.DAL;
using DesafioBruc.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesafioBruc.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteDAO _clienteDAO;

        public ClienteController(ClienteDAO clienteDAO)
        {
            _clienteDAO = clienteDAO;
        }


        public IActionResult Cadastrar()
        {
            ViewBag.Title = "Cadastrar Cliente";
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Cliente cliente)
        {
            if (ModelState.IsValid)
            {

                if (_clienteDAO.Cadastrar(cliente))
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Cliente Já Cadastrardo !");
            }
            return View(cliente);
        }


        public IActionResult Listar(int id)
        {
            ViewBag.Title = "Listar Clientes";
            if(id == 2)
            {
                return View(_clienteDAO.Listar());
            }
            return View(_clienteDAO.ListarPorId());
        }


        public IActionResult Remover(int id)
        {
            _clienteDAO.Remover(id);
            return RedirectToAction("Listar", "Cliente");
        }


        public IActionResult Editar(int id)
        {
            ViewBag.Title = "Editar Cliente";
            return View(_clienteDAO.BuscarPorId(id));
        }

        public IActionResult Alterar(Cliente cliente)
        {
            _clienteDAO.Alterar(cliente);
            return RedirectToAction("Listar", "Cliente");
        }

    }
}

using System;
using System.Threading.Tasks;
using DesafioBruc.DAL;
using DesafioBruc.Models;
using DesafioBruc.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace DesafioBruc.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteDAO _clienteDAO;

        private readonly IEmailSender _emailSender;

        private readonly EmailModel _emailModel;



        public ClienteController(ClienteDAO clienteDAO, IEmailSender emailSender, IWebHostEnvironment env, EmailModel emailModel)
        {
            _clienteDAO = clienteDAO;

            _emailSender = emailSender;

            _emailModel = emailModel;
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
                    _emailModel.Assunto = "Desafio BRUC - Novo Cadastro";
                    _emailModel.Destino = "regis.numata@gmail.com";
                    _emailModel.Mensagem = "Novo cliente cadastrado | Nome : " + cliente.Nome + " | email : " + cliente.Email + " | Telefone : " + cliente.Telefone + " | Cidade : " + cliente.Cidade + " | Estado : " + cliente.Estado + " | Nasc: " + cliente.DataNascimento;
                    TesteEnvioEmail(_emailModel.Destino, _emailModel.Assunto, _emailModel.Mensagem).GetAwaiter();

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

            Cliente cliente = _clienteDAO.BuscarPorId(id);
            _emailModel.Assunto = "Desfaio BRUC - Exclusão de Cadastro";
            _emailModel.Destino = "regis.numata@gmail.com";
            _emailModel.Mensagem = "Cadastro Excluído | Nome : " + cliente.Nome + " | email : " + cliente.Email;
            TesteEnvioEmail(_emailModel.Destino, _emailModel.Assunto, _emailModel.Mensagem).GetAwaiter();

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
            _emailModel.Assunto = "Desafio BRUC - Cadastro Alterado";
            _emailModel.Destino = "regis.numata@gmail.com";
            _emailModel.Mensagem = "Cadastro Alterado | Nome : " + cliente.Nome + " | email : " + cliente.Email + " | Telefone : " + cliente.Telefone + " | Cidade : " + cliente.Cidade + " | Estado : " + cliente.Estado + " | Nasc: " + cliente.DataNascimento;
            TesteEnvioEmail(_emailModel.Destino, _emailModel.Assunto, _emailModel.Mensagem).GetAwaiter();

            _clienteDAO.Alterar(cliente);
            return RedirectToAction("Listar", "Cliente");

        }


        public async Task TesteEnvioEmail(string email, string assunto, string mensagem)
        {
            try
            {
                //email destino, assunto do email, mensagem a enviar
                await _emailSender.SendEmailAsync(email, assunto, mensagem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

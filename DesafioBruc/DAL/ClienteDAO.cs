using System;
using System.Collections.Generic;
using System.Linq;
using DesafioBruc.Models;

namespace DesafioBruc.DAL
{
    public class ClienteDAO
    {

        private readonly Context _context;

        public ClienteDAO(Context context) => _context = context;


        public bool Cadastrar(Cliente cliente)
        {
            if (BuscarPorNome(cliente.Nome) == null)
            {
                _context.Clientes.Add(cliente);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Cliente> Listar()
        {
            return _context.Clientes.OrderBy(c => c.Nome).ToList();
        }

        public List<Cliente> ListarPorId()
        {
            return _context.Clientes.OrderBy(c => c.Id).ToList();
        }


        public void Remover(int id)
        {
            _context.Remove(BuscarPorId(id));
            _context.SaveChanges();
        }


        public void Alterar(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            _context.SaveChanges();
        }


        public Cliente BuscarPorId(int id)
        {
            return _context.Clientes.Find(id);
        }

        public Cliente BuscarPorNome(string nome)
        {
            return _context.Clientes.FirstOrDefault(x => x.Nome == nome);
        }

    }
}

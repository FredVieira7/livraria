using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MongoDB.Driver;
using Livraria.Domain.Entidades;
using Livraria.Domain.Query;
using Livraria.Infra.Data.DataContexts;
using Livraria.Domain.Interfaces.Respositories;

namespace Livraria.Infra.Data.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly DataContext _dataContext;

        public LivroRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Livro Inserir(Livro livro)
        {
            _dataContext.LivrosDb.InsertOne(livro);
            return livro;
        }

        public void Atualizar(Livro livro)
        {
            _dataContext.LivrosDb.ReplaceOne(book => book.Id == livro.Id, livro);
        }

        public void Excluir(string id)
        {
            _dataContext.LivrosDb.DeleteOne(book => book.Id.ToString() == id);
        }

        public List<Livro> Listar()
        {
            return _dataContext.LivrosDb.Find(book => true).ToList();
        }

        public Livro Obter(string id)
        {
            return _dataContext.LivrosDb.Find(book => book.Id.ToString() == id).FirstOrDefault();
        }

        public bool CheckId(string id)
        {
            return _dataContext.LivrosDb.Find(book => book.Id.ToString() == id).Any();
        }
    }
}

using Livraria.Domain.Entidades;
using System.Collections.Generic;

namespace Livraria.Domain.Interfaces.Respositories
{
    public interface ILivroRepository
    {
        Livro Inserir(Livro livro);
        void Atualizar(Livro livro);
        void Excluir(string id);
        List<Livro> Listar();

        Livro Obter(string id);
        bool CheckId(string id);
    }
}
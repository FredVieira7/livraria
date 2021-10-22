using Livraria.Domain.Commands.Inputs;
using Livraria.Domain.Entidades;
using Livraria.Domain.Handlers;
using Livraria.Domain.Interfaces.Respositories;
using Livraria.Infra.Interfaces.Commands;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Livraria.Api.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroRepository _repository;
        private readonly LivroHandler _handler;

        public LivroController(ILivroRepository repository, LivroHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        [HttpPost]
        [Route("v1/livros")]
        public ICommandResult InserirLivro([FromBody] AdicionarLivroCommand command)
        {
            return _handler.Handle(command);
        }

        [HttpPut]
        [Route("v1/livros/{id}")]
        public ICommandResult AtualizarLivro(string id, [FromBody] AtualizarLivroCommand command)
        {
            command.Id = id;
            return _handler.Handle(command);
        }

        [HttpDelete]
        [Route("livros/{id}")]
        public ICommandResult ExcluirLivro(string id)
        {
            return _handler.Handle(new ExcluirLivroCommand() { Id = id });
        }

        [HttpGet]
        [Route("v1/livros")]
        public List<Livro> ListarLivros()
        {
            return _repository.Listar();
        }

        [HttpGet]
        [Route("v1/livro/{id}")]
        public Livro ObterLivro(string id)
        {
            return _repository.Obter(id);
        }
    }
}
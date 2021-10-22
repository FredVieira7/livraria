

using Livraria.Domain.Commands.Inputs;
using Livraria.Domain.Commands.Outputs;
using Livraria.Domain.Entidades;
using Livraria.Domain.Interfaces.Respositories;
using Livraria.Infra.Interfaces.Commands;

namespace Livraria.Domain.Handlers
{
    public class LivroHandler : ICommandHandler<AdicionarLivroCommand>, ICommandHandler<AtualizarLivroCommand>,
        ICommandHandler<ExcluirLivroCommand>
    {
        private readonly ILivroRepository _repository;

        public LivroHandler(ILivroRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(AdicionarLivroCommand command)
        {
            if (!command.ValidarCommand())
                return new LivroCommandResult(false, "Por favor corrija as inconsistências abaixo",
                    command.Notifications);

            Livro livro = new Livro(command.Nome, command.Autor, command.Edicao, command.Isbn, command.Imagem);

            _repository.Inserir(livro);

            var retorno = new LivroCommandResult(true, "Livro adicionado com sucesso!", livro);

            return retorno;
        }

        public ICommandResult Handle(AtualizarLivroCommand command)
        {
            if (!command.ValidarCommand())
                return new LivroCommandResult(false, "Por favor corrija as inconsistências abaixo",
                    command.Notifications);

            if (!_repository.CheckId(command.Id))
                return new LivroCommandResult(false, "Este livro não existe.", new { });

            string id = command.Id;

            Livro livro =
                new Livro(id, command.Nome, command.Autor, command.Edicao, command.Isbn, command.Imagem);
            _repository.Atualizar(livro);
            return new LivroCommandResult(true, "Livro atualizado com sucesso", livro);
        }

        public ICommandResult Handle(ExcluirLivroCommand command)
        {
            if (!command.ValidarCommand())
                return new LivroCommandResult(false, "Por favor corrija as inconsistências abaixo",
                    command.Notifications);

            if (!_repository.CheckId(command.Id))
                return new LivroCommandResult(false, "Este livro não existe.", new { });

            string id = command.Id;
            _repository.Excluir(id);
            return new LivroCommandResult(true, "Livro excluido com sucesso", new { });
        }
    }
}
using Flunt.Notifications;
using Livraria.Infra.Interfaces.Commands;
using System;

namespace Livraria.Domain.Commands.Inputs
{
    public class AdicionarLivroCommand : Notifiable, ICommandPadrao
    {
        public string Nome { get; set; }
        public string Autor { get; set; }
        public int Edicao { get; set; }
        public string Isbn { get; set; }
        public string Imagem { get; set; }

        public bool ValidarCommand()
        {
            if (string.IsNullOrWhiteSpace(Nome))
                AddNotification("Nome", "O nome é obrigatório");
            else if (Nome.Length > 50)
                AddNotification("Nome", "Nome maior que 50 caracteres");

            if (string.IsNullOrWhiteSpace(Autor))
                AddNotification("Autor", "O autor é obrigatório");
            else if (Autor.Length > 50)
                AddNotification("Autor", "Autor maior que 50 caracteres");

            if (Edicao <= 0)
                AddNotification("Edicao", "Edicao deve ser maior que zero");

            if (string.IsNullOrWhiteSpace(Isbn))
                AddNotification("Isbn", "O isbn é obrigatório");
            else if (Isbn.Length > 50)
                AddNotification("Isbn", "Isbn maior que 50 caracteres");

            if (string.IsNullOrWhiteSpace(Imagem))
                AddNotification("Imagem", "A imagem é obrigatória");

            return Valid;

        }
    }
}
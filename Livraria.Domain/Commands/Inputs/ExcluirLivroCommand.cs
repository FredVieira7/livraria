using System;
using System.Text.Json.Serialization;
using Flunt.Notifications;
using Livraria.Infra.Interfaces.Commands;

namespace Livraria.Domain.Commands.Inputs
{
    public class ExcluirLivroCommand : Notifiable, ICommandPadrao
    {
        [JsonIgnore]
        public string Id { get; set; }

        public bool ValidarCommand()
        {
            if (string.IsNullOrWhiteSpace(Id.ToString()))
                AddNotification("Id", "ID é um campo obrigatório");

            return Valid;

        }
    }
}
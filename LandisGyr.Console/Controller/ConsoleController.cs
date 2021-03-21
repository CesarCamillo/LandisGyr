using MediatR;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LandisGyr.ConsoleApp.Controller
{
    public class ConsoleController : IHostedService
    {
        private readonly IMediator _mediator;

        public ConsoleController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Iniciando a aplicação.");

            int choice = 0;

            while (choice >= 0)
            {
                Console.WriteLine("Selecione uma opção:");
                Console.WriteLine("1 - Inserir um novo Endpoint");
                Console.WriteLine("2 - Editar um Endpoint");
                Console.WriteLine("3 - Deletar um Endpoint");
                Console.WriteLine("4 - Listar todos os Endpoints");
                Console.WriteLine("5 - Encontrar um endpoint pelo Serial");
                Console.WriteLine("6 - Sair da Aplicação");

                try
                {
                    choice = Math.Abs(int.Parse(Console.ReadLine()));
                }
                catch (FormatException)
                {
                    choice = 0;
                }

                switch (choice)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        choice = -1;
                        Console.WriteLine("Encerrando a aplicação.");
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Por favor, insira um número de 1 a 6.");
                        break;
                }
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

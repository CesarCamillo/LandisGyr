using LandisGyr.ConsoleApp.Features;
using LandisGyr.ConsoleApp.Models;
using MediatR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task StartAsync(CancellationToken cancellationToken)
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
                        await CreateEndpoint(cancellationToken);
                        choice = 0;
                        break;
                    case 2:
                        await UpdateEndpoint(cancellationToken);
                        choice = 0;
                        break;
                    case 3:
                        await DeleteEndpoint(cancellationToken);
                        choice = 0;
                        break;
                    case 4:
                        await FindAllEndpoints(cancellationToken);
                        choice = 0;
                        break;
                    case 5:
                        await FindEndpoint(cancellationToken);
                        choice = 0;
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
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.Write("Operações encerradas.");
            return Task.CompletedTask;
        }

        private async Task FindEndpoint(CancellationToken cancellationToken)
        {
            FindEndpoint findCommand = new FindEndpoint();

            Console.Write("Insira o número serial do Endpoint: ");
            findCommand.SerialNumber = Console.ReadLine();

            var result = await _mediator.Send(findCommand, cancellationToken);

            if (result is null)
            {
                Console.WriteLine($"Não existe um Endpoint com número serial {findCommand.SerialNumber}");
            }
            else
            {
                Console.WriteLine(result);
            }
            Console.WriteLine("-------------------------------------");
        }

        private async Task DeleteEndpoint(CancellationToken cancellationToken)
        {
            FindEndpoint findCommand = new FindEndpoint();

            Console.Write("Insira o número serial do Endpoint: ");
            findCommand.SerialNumber = Console.ReadLine();

            var result = await _mediator.Send(findCommand, cancellationToken);

            if (result is null)
            {
                Console.WriteLine($"Não existe um Endpoint com número serial {findCommand.SerialNumber}");
            }
            else
            {
                Console.WriteLine($"Deseja mesmo deletar o endpoint {result.SerialNumber}?\n0 - Não\n1 - Sim");
                int aux = int.Parse(Console.ReadLine());

                if (aux == 1)
                {
                    try
                    {
                        DeleteEndpoint deleteCommand = new DeleteEndpoint
                        {
                            SerialNumber = findCommand.SerialNumber
                        };

                        await _mediator.Send(deleteCommand, cancellationToken);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Um erro inesperado ocorreu.");
                    }

                }
                else if (aux == 0)
                {
                    Console.WriteLine("Cancelando deleção.");
                }
                else
                {
                    Console.WriteLine("Caractere inválido, cancelando o processo.");
                }
            }
            Console.WriteLine("-------------------------------------");
        }

        private async Task UpdateEndpoint(CancellationToken cancellationToken)
        {
            FindEndpoint findCommand = new FindEndpoint();

            Console.Write("Insira o número serial do Endpoint: ");
            findCommand.SerialNumber = Console.ReadLine();

            var result = await _mediator.Send(findCommand, cancellationToken);
            
            if (result is null)
            {
                Console.WriteLine($"Não existe um Endpoint com número serial {findCommand.SerialNumber}");
            }
            else
            {
                UpdateEndpoint updateCommand = new UpdateEndpoint();

                Console.Write("Insira o estado do switch do Endpoint: ");
                updateCommand.SwitchState = (SwitchStates)int.Parse(Console.ReadLine());
                updateCommand.SerialNumber = findCommand.SerialNumber;

                UpdateEndpointValidator validator = new UpdateEndpointValidator();
                var validation = validator.Validate(updateCommand);

                if (!validation.IsValid)
                {
                    Console.WriteLine(validation.Errors.FirstOrDefault());
                }
                else
                {
                    try
                    {
                        await _mediator.Send(updateCommand, cancellationToken);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Um erro inesperado ocorreu");
                    }
                }
            }
            Console.WriteLine("-------------------------------------");
        }

        private async Task CreateEndpoint(CancellationToken cancellationToken)
        {
            CreateEndpoint command = new CreateEndpoint();

            Console.Write("Insira o número serial do Endpoint: ");
            command.SerialNumber = Console.ReadLine();
            Console.Write("Insira o id do modelo do medidor do Endpoint: ");
            command.MeterModel = (MeterModels)int.Parse(Console.ReadLine());
            Console.Write("Insira o número do medidor do Endpoint: ");
            command.MeterNumber = int.Parse(Console.ReadLine());
            Console.Write("Insira a versão do firmware do medidor do Endpoint: ");
            command.MeterFirmwareVersion = Console.ReadLine();
            Console.Write("Insira o estado do switch do Endpoint: ");
            command.SwitchState = (SwitchStates)int.Parse(Console.ReadLine());

            CreateEndpointValidator validator = new CreateEndpointValidator();
            var validation = validator.Validate(command);

            if (!validation.IsValid)
            {
                Console.WriteLine(validation.Errors.FirstOrDefault());
            }
            else
            {
                try
                {

                    await _mediator.Send(command, cancellationToken);
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine($"Já existe um Endpoint com o número serial {command.SerialNumber}.");
                }
                catch (Exception)
                {
                    Console.WriteLine("Um erro inesperado ocorreu");
                }
            }
            Console.WriteLine("-------------------------------------");
        }

        private async Task FindAllEndpoints(CancellationToken cancellationToken)
        {
            FindAllEndpoints command = new FindAllEndpoints();

            var result = await _mediator.Send(command, cancellationToken);

            List<Endpoint> endpoints = result.ToList();

            foreach (var endpoint in endpoints)
            {
                Console.WriteLine(endpoint);
                Console.WriteLine("-------------------------------------");
            }

        }


    }
}

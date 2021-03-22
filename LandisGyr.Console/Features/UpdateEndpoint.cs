using AutoMapper;
using FluentValidation;
using LandisGyr.ConsoleApp.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LandisGyr.ConsoleApp.Features
{
    public class UpdateEndpoint : IRequest
    {
        public string SerialNumber { get; set; }

        public SwitchStates SwitchState { get; set; }
    }


    public class UpdateEndpointValidator : AbstractValidator<UpdateEndpoint>
    {
        public UpdateEndpointValidator()
        {
            RuleFor(e => e.SerialNumber)
                .NotEmpty()
                .MaximumLength(255);

            RuleFor(e => e.SwitchState)
                .IsInEnum();
        }
    }

    public class UpdateEndpointHandler : IRequestHandler<UpdateEndpoint>
    {
        private readonly LandisGyrContext _context;
        private readonly IMapper _mapper;

        public UpdateEndpointHandler(LandisGyrContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<Unit> Handle(UpdateEndpoint request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var updatedModel = _mapper.Map<Endpoint>(request);


            return UpdateEndpointInternalAsync(updatedModel, cancellationToken);
        }

        private async Task<Unit> UpdateEndpointInternalAsync(Endpoint updatedModel, CancellationToken cancellationToken)
        {
            var existingEndpoint = await _context
                .Endpoints
                .WithSerialNumber(updatedModel.SerialNumber)
                .SingleOrDefaultAsync(cancellationToken);

            // TODO: Checar problema no profile <Endpoint, Endpoint> do AutoMapper
            //_mapper.Map(updatedModel, existingEndpoint);

            if (existingEndpoint is null)
            {
                throw new KeyNotFoundException($"Não existe Endpoint para o serial number {updatedModel.SerialNumber}.");
            }

            existingEndpoint.SwitchState = updatedModel.SwitchState;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}

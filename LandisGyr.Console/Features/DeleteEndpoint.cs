using LandisGyr.ConsoleApp.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LandisGyr.ConsoleApp.Features
{
    public class DeleteEndpoint : IRequest
    {
        public string SerialNumber { get; set; }
    }

    public class DeleteEndpointHandler : IRequestHandler<DeleteEndpoint>
    {
        private readonly LandisGyrContext _context;

        public DeleteEndpointHandler(LandisGyrContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<Unit> Handle(DeleteEndpoint request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException();
            }

            return DeleteEndpointInternalAsync(request.SerialNumber, cancellationToken);
        }

        private async Task<Unit> DeleteEndpointInternalAsync(string serialNumber, CancellationToken cancellationToken)
        {
            var existingEndpoint = await _context
                .Endpoints
                .WithSerialNumber(serialNumber).SingleOrDefaultAsync(cancellationToken);

            if (existingEndpoint is null)
            {
                throw new KeyNotFoundException($"Não foi encontrado um endpoint com serial {serialNumber}.");
            }

            _context.Endpoints.Remove(existingEndpoint);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

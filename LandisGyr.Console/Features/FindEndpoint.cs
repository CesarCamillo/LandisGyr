using AutoMapper;
using LandisGyr.ConsoleApp.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LandisGyr.ConsoleApp.Features
{
    public class FindEndpoint : IRequest<Endpoint>
    {
        public string SerialNumber { get; set; }
    }

    public class FindEndpointHandler : IRequestHandler<FindEndpoint, Endpoint>
    {
        private readonly LandisGyrContext _context;
        private readonly IMapper _mapper;

        public FindEndpointHandler(LandisGyrContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<Endpoint> Handle(FindEndpoint request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException();
            }

            return FindEndpointInternalAsync(request.SerialNumber, cancellationToken);
        }

        private async Task<Endpoint> FindEndpointInternalAsync(string serialNumber, CancellationToken cancellationToken)
        {
            var existingEndpoint = await _context
                .Endpoints
                .WithSerialNumber(serialNumber)
                .AsNoTracking()
                .SingleOrDefaultAsync(cancellationToken);

            if (existingEndpoint is null)
            {
                throw new KeyNotFoundException($"Não foi encontrado um endpoint com serial {serialNumber}.");
            }

            return existingEndpoint;
        }
    }
}

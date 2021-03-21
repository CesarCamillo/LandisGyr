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
    public class FindAllEndpoints : IRequest<IEnumerable<Endpoint>>
    {
    }

    public class FindAllEndpointsHandler : IRequestHandler<FindAllEndpoints, IEnumerable<Endpoint>>
    {
        private readonly LandisGyrContext _context;
        private readonly IMapper _mapper;

        public FindAllEndpointsHandler(LandisGyrContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<IEnumerable<Endpoint>> Handle(FindAllEndpoints request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return FindAllEndpointsInternalAsync(cancellationToken);
        }

        private async Task<IEnumerable<Endpoint>> FindAllEndpointsInternalAsync(CancellationToken cancellationToken)
        {
            var result = await _context
                .Endpoints
                .AllEndpoints()
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<Endpoint>>(result);
        }
    }
}

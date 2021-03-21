using AutoMapper;
using FluentValidation;
using LandisGyr.ConsoleApp.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LandisGyr.ConsoleApp.Features
{
    public class CreateEndpoint : IRequest<Endpoint>
    {
        /// <summary>
        /// Número serial do novo Endpoint
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Modelo do medidor do novo endpoint
        /// </summary>
        public MeterModels MeterModel { get; set; }

        /// <summary>
        /// Número do medidor do novo endpoint
        /// </summary>
        public int MeterNumber { get; set; }

        /// <summary>
        /// Versão do firmware do medidor do nov endpoint
        /// </summary>
        public string MeterFirmwareVersion { get; set; }

        /// <summary>
        /// Estadp do switch do novo endpoint
        /// </summary>
        public SwitchStates SwitchState { get; set; }
    }

    public class CreateEndpointValidator : AbstractValidator<CreateEndpoint>
    {
        public CreateEndpointValidator()
        {
            RuleFor(e => e.SerialNumber)
                .NotEmpty()
                .MaximumLength(255);

            RuleFor(e => e.MeterNumber)
                .NotEmpty();

            RuleFor(e => e.MeterFirmwareVersion)
                .NotEmpty()
                .MaximumLength(255);

            RuleFor(e => e.MeterModel)
                .NotEmpty()
                .IsInEnum();

            RuleFor(e => e.SwitchState)
                .IsInEnum();                
        }
    }

    public class CreateEndpointHandler : IRequestHandler<CreateEndpoint, Endpoint>
    {
        private readonly LandisGyrContext _context;
        private readonly IMapper _mapper;

        public CreateEndpointHandler(LandisGyrContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<Endpoint> Handle(CreateEndpoint request, CancellationToken cancelToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var endpoint = _mapper.Map<Endpoint>(request);

            return CreateInternalEndpointAsync(endpoint, cancelToken);
        }

        private async Task<Endpoint> CreateInternalEndpointAsync(Endpoint endpoint, CancellationToken cancelToken)
        {
            _context.Endpoints.Add(endpoint);

            await _context.SaveChangesAsync(cancelToken);

            return endpoint;
        }
    }
}

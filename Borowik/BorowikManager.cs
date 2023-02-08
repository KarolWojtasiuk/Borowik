using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Borowik;

[ServiceDescriptor(typeof(IBorowikManager), ServiceLifetime.Singleton)]
internal class BorowikManager : IBorowikManager
{
    private readonly IMediator _mediator;

    public BorowikManager(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<TResponse> SendRequestAsync<TResponse>(IBorowikRequest<TResponse> request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(request, cancellationToken);
    }
}
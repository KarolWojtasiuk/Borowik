using MediatR;

namespace Borowik;

public interface IBorowikRequest<TResponse> : IRequest<TResponse>
{
}
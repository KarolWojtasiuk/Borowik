using MediatR;

namespace Borowik.Queries;

public interface IQuery<TResult> : IRequest<TResult>
{

}
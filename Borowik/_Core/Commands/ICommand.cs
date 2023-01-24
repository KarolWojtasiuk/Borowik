using MediatR;

namespace Borowik.Commands;
public interface ICommand : IRequest<Unit>
{
}

public interface ICommand<TResult> : IRequest<TResult>
{
}
namespace Borowik.Gui.Wasm.Services;

public interface IExceptionHandler
{
    public Task HandleAsync(Func<Task> func);
    public Task<T> HandleAsync<T>(Func<Task<T>> func);
}
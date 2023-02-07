using Blazorise;

namespace Borowik.Gui.Wasm.Services;

internal class ExceptionHandler : IExceptionHandler
{
    private readonly INotificationService _notificationService;

    public ExceptionHandler(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }
    
    public async Task HandleAsync(Func<Task> func)
    {
        try
        {
            await func();
        }
        catch (Exception e)
        {
            await _notificationService.Error(e.Message, "Unhandled Error");
            throw;
        }
    }

    public async Task<T> HandleAsync<T>(Func<Task<T>> func)
    {
        try
        {
            return await func();
        }
        catch (Exception e)
        {
            await _notificationService.Error(e.Message, "Unhandled Error");
            throw;
        }
    }
}
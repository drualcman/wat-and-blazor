using Microsoft.JSInterop;

namespace ExecuteWasm.WasmModules;

public abstract class WasmLoaderServiceBase : IAsyncDisposable
{
    readonly Lazy<Task<IJSObjectReference>> GetWasModuleInstanceTask;

    public WasmLoaderServiceBase(IJSRuntime jsRuntime, string module)
    {
        GetWasModuleInstanceTask = new Lazy<Task<IJSObjectReference>>(
            async () =>
            {
                IJSObjectReference jsModule = await jsRuntime.InvokeAsync<IJSObjectReference>(
                    "import", "./js/wasmLoader.js");

                IJSObjectReference wasmModuleInstance = await jsModule.InvokeAsync<IJSObjectReference>(
                    "loadWasmModule", module);
                return wasmModuleInstance;
            });
    }

    public async ValueTask DisposeAsync()
    {
        if(GetWasModuleInstanceTask.IsValueCreated)
        {
            IJSObjectReference module = await GetWasModuleInstanceTask.Value;
            await module.DisposeAsync();
        }
    }

    protected async Task<T> InvokeAsync<T>(string methodName, params object[] parameters)
    {
        IJSObjectReference module = await GetWasModuleInstanceTask.Value;
        T result = default;
        string newMethodName = $"exports.{methodName}";
        try
        {
            if(parameters is not null) result = await module.InvokeAsync<T>(newMethodName, parameters);
            else result = await module.InvokeAsync<T>(newMethodName);
        }
        catch(Exception ex)
        {
            await Console.Out.WriteAsync(ex.Message);
        }
        return result;
    }

    protected async Task InvokeVoidAsync(string methodName, params object[] parameters)
    {
        IJSObjectReference module = await GetWasModuleInstanceTask.Value;
        string newMethodName = $"exports.{methodName}";
        try
        {
            if(parameters is not null) await module.InvokeVoidAsync(newMethodName, parameters);
            else await module.InvokeVoidAsync(newMethodName);
        }
        catch(Exception ex)
        {
            await Console.Out.WriteAsync(ex.Message);
        }
    }
}

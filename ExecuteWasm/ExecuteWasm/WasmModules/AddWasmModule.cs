using Microsoft.JSInterop;

namespace ExecuteWasm.WasmModules;

public class AddWasmModule : WasmLoaderServiceBase
{
    public AddWasmModule(IJSRuntime jsRuntime) : base(jsRuntime, "./wasm/add.wasm")
    {
    }

    public async Task<int> Add(int a, int b)
    {
        int result = await InvokeAsync<int>("add", a, b);
        return result;
    }
}

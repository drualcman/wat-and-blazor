export const loadWasmModule = async (wasModule) => {
    try {
        const response = await fetch(wasModule);
        const resultObject = await WebAssembly.instantiateStreaming(response);
        console.log(`module ${wasModule} loaded.`);
        return resultObject.instance;
    }
    catch (error) {
        console.error(`Could not load module ${wasModule}: ${error}`);
    }
}
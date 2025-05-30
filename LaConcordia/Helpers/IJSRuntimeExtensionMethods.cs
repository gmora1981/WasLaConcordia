﻿using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace LaConcordia.Helpers
{
    public static class IJSRuntimeExtensionMethods
    {
        public static async ValueTask InitializeInactivityTimer<T>(this IJSRuntime js,
           DotNetObjectReference<T> dotNetObjectReference) where T : class
        {
            await js.InvokeVoidAsync("initializeInactivityTimer", dotNetObjectReference);
        }
        public static ValueTask<object> SetInLocalStorage(this IJSRuntime js, string key, string content)
         => js.InvokeAsync<object>(
     "localStorage.setItem",
     key, content
     );

        public static ValueTask<string> GetFromLocalStorage(this IJSRuntime js, string key)
            => js.InvokeAsync<string>(
                "localStorage.getItem",
                key
                );

        public static ValueTask<object> RemoveItem(this IJSRuntime js, string key)
            => js.InvokeAsync<object>(
                "localStorage.removeItem",
                key);
    }
}

using Microsoft.JSInterop;

namespace LaConcordia.Helpers
{
    public class DisplayMessage : IDisplayMessage
    {
        private readonly IJSRuntime js;

        public DisplayMessage(IJSRuntime js)
        {
            this.js = js;
        }

        public async ValueTask DisplayErrorMessage(string message)
        {
            await DoDisplayMessage("Error", message, "error");
        }

        // ⚠️ Mensaje de advertencia
        public async ValueTask DisplayWarningMessage(string message)
        {
            await DoDisplayMessage("Advertencia", message, "warning");
        }

        // ℹ️ Mensaje informativo
        public async ValueTask DisplayInfoMessage(string message)
        {
            await DoDisplayMessage("Información", message, "info");
        }

        public async ValueTask DisplaySuccessMessage(string message)
        {
            await DoDisplayMessage("Success", message, "success");
        }

        private async ValueTask DoDisplayMessage(string title, string message, string messageType)
        {
            await js.InvokeVoidAsync("Swal.fire", title, message, messageType);
        }

    }
}

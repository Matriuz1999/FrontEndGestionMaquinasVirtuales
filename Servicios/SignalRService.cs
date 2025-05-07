using Microsoft.AspNetCore.SignalR.Client;

namespace FrontEndGestionMaquinasVirtuales.Servicios
{
    public class SignalRService
    {
        private HubConnection _hubConnection;

        public event Action<string>? OnNotificacion;

        public async Task ConectarAsync()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5296/maquinasVirtualesHub")
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.On<string>("MaquinaVirtualCreada", (mensaje) =>
            {
                Console.WriteLine($"Recibido: {mensaje}");
                OnNotificacion?.Invoke($"🖥️ Máquina creada: {mensaje}");
            });

            _hubConnection.On<string>("MaquinaVirtualActualizada", (mensaje) =>
            {
                Console.WriteLine($"Actualizado: {mensaje}");
                OnNotificacion?.Invoke($"🔄 Máquina actualizada: {mensaje}");
            });

            _hubConnection.On<string>("MaquinaVirtualEliminada", (mensaje) =>
            {
                Console.WriteLine($"Eliminado: {mensaje}");
                OnNotificacion?.Invoke($"❌ Máquina eliminada: {mensaje}");
            });

            try
            {
                await _hubConnection.StartAsync();
                Console.WriteLine("✅ Conectado a SignalR.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al conectar: {ex.Message}");
            }
        }


        public async Task DesconectarAsync()
        {
            if (_hubConnection != null)
                await _hubConnection.StopAsync();
        }
    }
}

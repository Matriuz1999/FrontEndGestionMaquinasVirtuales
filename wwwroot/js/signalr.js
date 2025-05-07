window.signalRHelper = {
    connection: null,
    connectToSignalR: function (url, dotnetHelper) {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(url)
            .configureLogging(signalR.LogLevel.Information)
            .build();

        this.connection.on("MaquinaVirtualCreada", function (data) {
            dotnetHelper.invokeMethodAsync('RecibirNotificacion', JSON.stringify(data));
        });

        this.connection.on("MaquinaVirtualActualizada", function (data) {
            dotnetHelper.invokeMethodAsync('RecibirNotificacion', JSON.stringify(data));
        });

        this.connection.on("MaquinaVirtualEliminada", function (data) {
            dotnetHelper.invokeMethodAsync('RecibirNotificacion', JSON.stringify(data));
        });

        this.connection.start().then(function () {
            console.log("✅ Conectado exitosamente a SignalR");

            // Mostrar mensaje en pantalla si existe un contenedor con ID 'estadoSignalR'
            const estado = document.getElementById("estadoSignalR");
            if (estado) {
                estado.textContent = "✅ Conectado a SignalR";
            }
        }).catch(function (err) {
            console.error("❌ Error al conectar a SignalR:", err);
            const estado = document.getElementById("estadoSignalR");
            if (estado) {
                estado.textContent = "❌ Error al conectar a SignalR";
            }
        });
    }
};


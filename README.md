# FrontEnd Gestión Máquinas Virtuales

Aplicación web desarrollada con Blazor Server para la gestión de máquinas virtuales.

## Características principales

- Autenticación de usuarios con roles
- Gestión completa de máquinas virtuales (CRUD)
- Notificaciones en tiempo real con SignalR
- Interfaz adaptable y responsive
- Permisos basados en roles (Administrador/Cliente)

## Requisitos previos

- .NET 7.0 o superior
- Un editor de código como Visual Studio 2022 o VS Code
- API backend configurada y funcionando

## Instalación

1. Clone el repositorio:
   ```
 https://github.com/Matriuz1999/FrontEndGestionMaquinasVirtuales.git
   ```

2. Navegue al directorio del proyecto:
   ```
   cd FrontEndGestionMaquinasVirtuales
   ```

3. Restaure las dependencias:
   ```
   dotnet restore
   ```

4. Configure la API:
   - Abra el archivo `appsettings.json` y configure la URL base de la API:
   ```json
   {
     "ApiSettings": {
       "BaseUrl": "http://localhost:5000/api",
       "UrlSignal": "http://localhost:5000/maquinasVirtualesHub"
     }
   }
   ```

5. Compile y ejecute el proyecto:
   ```
   dotnet run
   ```

6. Abra un navegador y vaya a `https://localhost:5001` o la URL mostrada en la consola.

## Estructura del proyecto

```
FrontEndGestionMaquinasVirtuales/
├── Dtos/                  # Data Transfer Objects
│   ├── LoginDto.cs        # DTOs para autenticación
│   └── MaquinaVirtualDto.cs # DTOs para máquinas virtuales
├── Pages/                 # Páginas Blazor
│   ├── Login.razor        # Página de inicio de sesión
│   └── MaquinasVirtuales.razor # Página de gestión de VMs
├── Servicios/             # Servicios
│   ├── ApiGestionMaquinas.cs # Cliente HTTP para la API
│   └── SignalRService.cs  # Servicio para SignalR
├── wwwroot/               # Archivos estáticos
│   ├── css/               # Hojas de estilo
│   └── js/                # JavaScript
│       └── signalr.js     # Cliente SignalR
└── Program.cs             # Punto de entrada
```

## Configuración

La aplicación requiere las siguientes configuraciones en `appsettings.json`:

```json
{
  "ApiSettings": {
    "BaseUrl": "URL_DE_TU_API",
    "UrlSignal": "URL_DE_TU_HUB_SIGNALR"
  }
}


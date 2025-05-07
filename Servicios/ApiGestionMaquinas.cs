using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;
using FrontEndGestionMaquinasVirtuales.Dtos;
using System.Text.Json.Serialization;

namespace FrontEndGestionMaquinasVirtuales.Servicios
{
    public class ApiGestionMaquinas
    {
        private readonly IConfiguration _config;
        private readonly string _baseUrl;
        private string TokenNube;

        public ApiGestionMaquinas(IConfiguration config)
        {
            _config = config;
            // Obtener la baseUrl desde la configuración
            _baseUrl = _config["ApiSettings:BaseUrl"] ?? throw new ArgumentNullException("ApiSettings:BaseUrl no está configurada.");
        }

        // Método para agregar el token a las cabeceras de las peticiones
        private void AgregarToken(HttpClient httpClient)
        {
            if (!string.IsNullOrEmpty(TokenNube))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenNube);
            }
        }

        // Consumo de API para loguearse
        public async Task<LoginResponseDto> LoginAsync(LoginDto login)
        {
            try
            {
                var UrlApi = $"{_baseUrl}/Auth/login";

                LoginResponseDto loginResponse = new LoginResponseDto();

                using (var httpClient = new HttpClient())
                {
                    var dto = new LoginDto
                    {
                        Email = login.Email,
                        Password = login.Password
                    };

                    StringContent content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClient.PostAsync(UrlApi, content);
                    string ApiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        loginResponse = JsonConvert.DeserializeObject<LoginResponseDto>(ApiResponse);
                        TokenNube = loginResponse?.Token;
                    }
                    else
                    {
                        throw new Exception($"Error al iniciar sesión: {ApiResponse}");
                    }
                }

                return loginResponse;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en LoginAsync: {ex.Message}", ex);
            }
        }

        // Consumo de API para obtener todas las maquinas virtuales
        public async Task<List<MaquinaVirtualDto>> ObtenerTodasAsync()
        {
            try
            {
                var UrlApi = $"{_baseUrl}/MaquinasVirtuales";
                List<MaquinaVirtualDto> maquinas = new List<MaquinaVirtualDto>();

                using (var httpClient = new HttpClient())
                {
                    AgregarToken(httpClient);
                    var result = await httpClient.GetAsync(UrlApi);
                    string apiResponse = await result.Content.ReadAsStringAsync();

                    if (result.IsSuccessStatusCode)
                    {
                        maquinas = JsonConvert.DeserializeObject<List<MaquinaVirtualDto>>(apiResponse);
                    }
                    else
                    {
                        throw new Exception($"Error al obtener máquinas virtuales: {apiResponse}");
                    }
                }

                return maquinas;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerTodasAsync: {ex.Message}", ex);
            }
        }

        // Consumo de API para obtener una maquina virtual por ID
        public async Task<MaquinaVirtualDto> ObtenerPorIdAsync(int id)
        {
            try
            {
                var UrlApi = $"{_baseUrl}/MaquinasVirtuales/{id}";
                MaquinaVirtualDto maquina = null;

                using (var httpClient = new HttpClient())
                {
                    AgregarToken(httpClient);
                    var result = await httpClient.GetAsync(UrlApi);
                    string apiResponse = await result.Content.ReadAsStringAsync();

                    if (result.IsSuccessStatusCode)
                    {
                        maquina = JsonConvert.DeserializeObject<MaquinaVirtualDto>(apiResponse);
                    }
                    else
                    {
                        throw new Exception($"Error al obtener la máquina virtual con ID {id}: {apiResponse}");
                    }
                }

                return maquina;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorIdAsync: {ex.Message}", ex);
            }
        }

        // Consumo de API para crear una nueva maquina virtual
        public async Task<MaquinaVirtualDto> CrearAsync(MaquinaVirtualCreateDto dto)
        {
            try
            {
                var UrlApi = $"{_baseUrl}/MaquinasVirtuales";
                MaquinaVirtualDto maquinaCreada = new MaquinaVirtualDto();

                using (var httpClient = new HttpClient())
                {
                    AgregarToken(httpClient);
                    StringContent content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync(UrlApi, content);
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        maquinaCreada = JsonConvert.DeserializeObject<MaquinaVirtualDto>(apiResponse);
                    }
                    else
                    {
                        throw new Exception($"Error al crear la máquina virtual: {apiResponse}");
                    }
                }

                return maquinaCreada;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en CrearAsync: {ex.Message}", ex);
            }
        }

        // Consumo de API para actualizar una maquina virtual
        public async Task<MaquinaVirtualDto> ActualizarAsync(int id, MaquinaVirtualUpdateDto dto)
        {
            try
            {
                var UrlApi = $"{_baseUrl}/MaquinasVirtuales/{id}";
                MaquinaVirtualDto maquinaActualizada = new MaquinaVirtualDto();

                using (var httpClient = new HttpClient())
                {
                    AgregarToken(httpClient);
                    StringContent content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                    var response = await httpClient.PutAsync(UrlApi, content);
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        maquinaActualizada = JsonConvert.DeserializeObject<MaquinaVirtualDto>(apiResponse);
                    }
                    else
                    {
                        throw new Exception($"Error al actualizar la máquina virtual con ID {id}: {apiResponse}");
                    }
                }

                return maquinaActualizada;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ActualizarAsync: {ex.Message}", ex);
            }
        }

        // Consumo de API para eliminar una maquina virtual
        public async Task<bool> EliminarAsync(int id)
        {
            try
            {
                var UrlApi = $"{_baseUrl}/MaquinasVirtuales/{id}";

                using (var httpClient = new HttpClient())
                {
                    AgregarToken(httpClient);
                    var response = await httpClient.DeleteAsync(UrlApi);
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception($"Error al eliminar la máquina virtual con ID {id}: {apiResponse}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en EliminarAsync: {ex.Message}", ex);
            }
        }
    }
}

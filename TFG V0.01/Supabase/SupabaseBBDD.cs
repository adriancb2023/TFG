using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class SupabaseBBDD
{
    #region Configuración

    private readonly string urlBase = "https://ddwyrkqxpmwlznjfjrwv.supabase.co/rest/v1/";
    private readonly string claveAPI = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImRkd3lya3F4cG13bHpuamZqcnd2Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDQzOTcwNjQsImV4cCI6MjA1OTk3MzA2NH0.G2LzHWbC09LC69bj9wONzhD_a6AfFI1ZYFuQ3KD7XhI";
    private readonly HttpClient clienteHTTP;

    public SupabaseBBDD()
    {
        clienteHTTP = new HttpClient();
        clienteHTTP.BaseAddress = new Uri(urlBase);
        clienteHTTP.DefaultRequestHeaders.Add("apikey", claveAPI);
        clienteHTTP.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", claveAPI);
    }

    #endregion

    #region Métodos HTTP Básicos
    private async Task<string> ObtenerAsync(string ruta)
    {
        try
        {
            var respuesta = await clienteHTTP.GetAsync(ruta);
            respuesta.EnsureSuccessStatusCode(); // Lanza una excepción si el código de estado no es exitoso
            return await respuesta.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            // Registra el error para depuración
            return $"Error: {ex.Message}";
        }
    }

    private async Task<string> CrearAsync(string ruta, object datos)
    {
        string json = JsonSerializer.Serialize(datos);
        var contenido = new StringContent(json, Encoding.UTF8, "application/json");
        var respuesta = await clienteHTTP.PostAsync(ruta, contenido);
        return await respuesta.Content.ReadAsStringAsync();
    }

    private async Task<string> ActualizarAsync(string ruta, object datos)
    {
        string json = JsonSerializer.Serialize(datos);
        var solicitud = new HttpRequestMessage(new HttpMethod("PATCH"), ruta)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
        var respuesta = await clienteHTTP.SendAsync(solicitud);
        return await respuesta.Content.ReadAsStringAsync();
    }

    private async Task<string> EliminarAsync(string ruta)
    {
        var respuesta = await clienteHTTP.DeleteAsync(ruta);
        return await respuesta.Content.ReadAsStringAsync();
    }
    #endregion



    #region CRUD Genérico

    #region Crear
    public async Task<string> InsertarEnTablaAsync(string tabla, object datos) =>
        await CrearAsync(tabla, datos);
    #endregion

    #region Leer
    public async Task<string> ObtenerTablaAsync(string tabla) =>
        await ObtenerAsync(tabla);

    public async Task<string> ObtenerPorIdAsync(string tabla, int id) =>
        await ObtenerAsync($"{tabla}?id=eq.{id}");
    #endregion

    #region Actualizar
    public async Task<string> ActualizarPorIdAsync(string tabla, int id, object datos) =>
        await ActualizarAsync($"{tabla}?id=eq.{id}", datos);
    #endregion

    #region Eliminar
    public async Task<string> EliminarPorIdAsync(string tabla, int id) =>
        await EliminarAsync($"{tabla}?id=eq.{id}");
    #endregion
    #endregion



    #region CRUD Específico - Clientes

    #region Crear
    public async Task<string> CrearClienteAsync(object cliente) =>
        await CrearAsync("Clientes", cliente);
    #endregion

    #region Leer
    public async Task<string> ObtenerClientesAsync() =>
        await ObtenerAsync("Clientes");
    #endregion

    #region Actualizar
    public async Task<string> ActualizarClienteAsync(int id, object cliente) =>
        await ActualizarAsync($"Clientes?id=eq.{id}", cliente);
    #endregion

    #region Eliminar
    public async Task<string> EliminarClienteAsync(int id) =>
        await EliminarAsync($"Clientes?id=eq.{id}");
    #endregion



    #region CRUD Específico - Casos

    #region Crear
    public async Task<string> CrearCasoAsync(object caso) =>
        await CrearAsync("Casos", caso);
    #endregion

    #region Leer
    public async Task<string> ObtenerCasosAsync() =>
        await ObtenerAsync("Casos");
    #endregion

    #region Actualizar
    public async Task<string> ActualizarCasoAsync(int id, object caso) =>
        await ActualizarAsync("Casos?id=eq." + id, caso);
    #endregion

    #region Eliminar
    public async Task<string> EliminarCasoAsync(int id) =>
        await EliminarAsync("Casos?id=eq." + id);
    #endregion
    #endregion



    #region CRUD Específico - Tareas

    #region Crear
    public async Task<string> CrearTareaAsync(object tarea) =>
        await CrearAsync("Tareas", tarea);
    #endregion

    #region Leer
    public async Task<string> ObtenerTareasAsync() =>
        await ObtenerAsync("Tareas");
    #endregion

    #region Actualizar
    public async Task<string> ActualizarTareaAsync(int id, object tarea) =>
        await ActualizarAsync("Tareas?id=eq." + id, tarea);
    #endregion

    #region Eliminar
    public async Task<string> EliminarTareaAsync(int id) =>
        await EliminarAsync("Tareas?id=eq." + id);
    #endregion
    #endregion



    #region CRUD Específico - Usuarios

    #region Crear
    public async Task<string> CrearUsuarioAsync(object usuario) =>
        await CrearAsync("Usuarios", usuario);
    #endregion

    #region Leer
    public async Task<string> ObtenerUsuariosAsync() =>
        await ObtenerAsync("Usuarios");
    #endregion

    #region Actualizar
    public async Task<string> ActualizarUsuarioAsync(int id, object usuario) =>
        await ActualizarAsync("Usuarios?id=eq." + id, usuario);
    #endregion

    #region Eliminar
    public async Task<string> EliminarUsuarioAsync(int id) =>
        await EliminarAsync("Usuarios?id=eq." + id);
    #endregion
    #endregion




    #region CRUD Específico - Abogados

    #region Crear
    public async Task<string> CrearAbogadoAsync(object abogado) =>
        await CrearAsync("Abogados", abogado);
    #endregion

    #region Leer
    public async Task<string> ObtenerAbogadosAsync() =>
        await ObtenerAsync("Abogados");
    #endregion

    #region Actualizar
    public async Task<string> ActualizarAbogadoAsync(int id, object abogado) =>
        await ActualizarAsync("Abogados?id=eq." + id, abogado);
    #endregion

    #region Eliminar
    public async Task<string> EliminarAbogadoAsync(int id) =>
        await EliminarAsync("Abogados?id=eq." + id);
    #endregion
    #endregion




    #region CRUD Específico - Documentos

    #region Crear
    public async Task<string> CrearDocumentoAsync(object documento) =>
        await CrearAsync("Documentos", documento);
    #endregion

    #region Leer
    public async Task<string> ObtenerDocumentosAsync() =>
        await ObtenerAsync("Documentos");
    #endregion

    #region Actualizar
    public async Task<string> ActualizarDocumentoAsync(int id, object documento) =>
        await ActualizarAsync("Documentos?id=eq." + id, documento);
    #endregion

    #region Eliminar
    public async Task<string> EliminarDocumentoAsync(int id) =>
        await EliminarAsync("Documentos?id=eq." + id);
    #endregion
    #endregion




    #region CRUD Específico - Comentarios

    #region Crear
    public async Task<string> CrearComentarioAsync(object comentario) =>
        await CrearAsync("Comentarios", comentario);
    #endregion

    #region Leer
    public async Task<string> ObtenerComentariosAsync() =>
        await ObtenerAsync("Comentarios");
    #endregion

    #region Actualizar
    public async Task<string> ActualizarComentarioAsync(int id, object comentario) =>
        await ActualizarAsync("Comentarios?id=eq." + id, comentario);
    #endregion

    #region Eliminar
    public async Task<string> EliminarComentarioAsync(int id) =>
        await EliminarAsync("Comentarios?id=eq." + id);
    #endregion
    #endregion

    #endregion



    #region Utilidades para la Interfaz

    public string CalcularDuracionDias(DateTime inicio, DateTime fin)
    {
        TimeSpan diferencia = fin - inicio;
        return $"{diferencia.TotalDays:N1} días";
    }

    public string CalcularDuracionHoras(DateTime inicio, DateTime fin)
    {
        TimeSpan diferencia = fin - inicio;
        return $"{diferencia.TotalHours:N1} horas";
    }

    public string CalcularDuracionCompleta(DateTime inicio, DateTime fin)
    {
        TimeSpan diferencia = fin - inicio;
        return $"{diferencia.Days} días, {diferencia.Hours}h, {diferencia.Minutes}m";
    }

    public string FormatearFecha(DateTime fecha)
    {
        return fecha.ToString("dd/MM/yyyy");
    }

    public string ObtenerColorPorFechaFin(DateTime fechaFin)
    {
        if (fechaFin < DateTime.Now)
            return "Red";
        else if ((fechaFin - DateTime.Now).TotalDays <= 2)
            return "Orange";
        else
            return "Green";
    }
    #endregion


    #region Utilidades Backend

    public bool ValidarCliente(string nombre, string correo)
    {
        return !string.IsNullOrWhiteSpace(nombre) && correo.Contains("@");
    }

    public string ConvertirAJSON(object objeto)
    {
        return JsonSerializer.Serialize(objeto, new JsonSerializerOptions { WriteIndented = true });
    }

    public T? ConvertirDesdeJSON<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json);
    }

    #endregion
}

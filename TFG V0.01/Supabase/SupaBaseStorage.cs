using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Supabase;
using Supabase.Storage;

internal class SupaBaseStorage
{
    #region supabase storage variables
    private readonly Supabase.Client _client;
    private readonly Supabase.Storage.Interfaces.IStorageClient<Supabase.Storage.Bucket, Supabase.Storage.FileObject> _storageClient;
    #endregion

    #region Constructor
    public SupaBaseStorage(string url, string apiKey)
    {
        _client = new Supabase.Client(url, apiKey);
        _storageClient = _client.Storage;
    }
    #endregion

    #region Métodos de Inicialización
    public async Task InicializarAsync()
    {
        await _client.InitializeAsync();
    }
    #endregion

    #region Métodos de Subida y Descarga

    public async Task SubirArchivoAsync(string nombreCubo, string rutaArchivo, string nombreArchivo)
    {
        var bucket = _storageClient.From(nombreCubo);
        await bucket.Upload(rutaArchivo, nombreArchivo);
    }

    public async Task SubirArchivoAutomaticoAsync(string rutaArchivo, string nombreArchivo)
    {
        string nombreCubo = ObtenerCuboPorTipoArchivo(nombreArchivo);
        await SubirArchivoAsync(nombreCubo, rutaArchivo, nombreArchivo);
    }

    public async Task<Stream> DescargarArchivoAsync(string nombreCubo, string nombreArchivo)
    {
        var bucket = _storageClient.From(nombreCubo);
        var response = await bucket.Download(nombreArchivo, null);
        return new MemoryStream(response);
    }

    #endregion

    #region Métodos de Eliminación y Listado

    public async Task EliminarArchivoAsync(string nombreCubo, string nombreArchivo)
    {
        var bucket = _storageClient.From(nombreCubo);
        await bucket.Remove(new List<string> { nombreArchivo });
    }

    public async Task<List<string>> ListarArchivosAsync(string nombreCubo)
    {
        var bucket = _storageClient.From(nombreCubo);
        var files = await bucket.List();
        var nombresArchivos = new List<string>();

        foreach (var file in files)
        {
            nombresArchivos.Add(file.Name);
        }

        return nombresArchivos;
    }

    #endregion

    #region Métodos de Búsqueda

    public async Task<string> BuscarArchivoAsync(string nombreArchivo)
    {
        var buckets = await _storageClient.ListBuckets();
        foreach (var bucket in buckets)
        {
            var archivos = await ListarArchivosAsync(bucket.Name);
            if (archivos.Contains(nombreArchivo))
            {
                return bucket.Name;
            }
        }
        return null;
    }

    public async Task<bool> BuscarPorCuboAsync(string nombreCubo, string nombreArchivo)
    {
        var archivos = await ListarArchivosAsync(nombreCubo);
        return archivos.Contains(nombreArchivo);
    }

    #endregion

    #region Métodos Auxiliares
    private string ObtenerCuboPorTipoArchivo(string nombreArchivo)
    {
        string extension = Path.GetExtension(nombreArchivo).ToLower();
        return extension switch
        {
            ".jpg" or ".jpeg" or ".png" => "images",
            ".mp4" or ".avi" => "videos",
            ".pdf" => "documents",
            _ => "others"
        };
    }

    #endregion

    #region Métodos Adicionales

    public async Task ActualizarArchivoAsync(string nombreCubo, string rutaArchivo, string nombreArchivo)
    {
        await EliminarArchivoAsync(nombreCubo, nombreArchivo);
        await SubirArchivoAsync(nombreCubo, rutaArchivo, nombreArchivo);
    }

    public async Task<Supabase.Storage.FileObject> ObtenerDetallesArchivoAsync(string nombreCubo, string nombreArchivo)
    {
        var bucket = _storageClient.From(nombreCubo);
        var archivos = await bucket.List();
        return archivos.Find(file => file.Name == nombreArchivo);
    }

    public async Task CrearCuboAsync(string nombreCubo)
    {
        await _storageClient.CreateBucket(nombreCubo);
    }

    public async Task EliminarCuboAsync(string nombreCubo)
    {
        await _storageClient.DeleteBucket(nombreCubo);
    }

    #endregion
}



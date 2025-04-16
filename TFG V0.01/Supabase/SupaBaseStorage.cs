using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Supabase;
using Supabase.Storage;
using Supabase.Storage.Exceptions;

internal class SupaBaseStorage
{
    private readonly Supabase.Client _client;
    private readonly Supabase.Storage.Interfaces.IStorageClient<Supabase.Storage.Bucket, Supabase.Storage.FileObject> _storageClient;

    public SupaBaseStorage(string url, string apiKey)
    {
        _client = new Supabase.Client(url, apiKey);
        _storageClient = _client.Storage;
    }

    public async Task InicializarAsync()
    {
        try
        {
            await _client.InitializeAsync();
        }
        catch (Exception ex)
        {
            throw new SupabaseStorageException("Error al inicializar el cliente Supabase.", ex);
        }
    }

    public async Task SubirArchivoAsync(string nombreCubo, string rutaArchivo, string nombreArchivo)
    {
        try
        {
            if (!File.Exists(rutaArchivo))
                throw new FileNotFoundException("El archivo no existe.", rutaArchivo);

            if (string.IsNullOrWhiteSpace(nombreCubo) || string.IsNullOrWhiteSpace(nombreArchivo))
                throw new ArgumentException("El nombre del cubo o archivo no puede estar vacío.");

            var bucket = _storageClient.From(nombreCubo);
            await bucket.Upload(rutaArchivo, nombreArchivo);
        }
        catch (Exception ex)
        {
            throw new SupabaseStorageException("Error al subir el archivo a Supabase.", ex);
        }
    }

    public async Task SubirArchivoAutomaticoAsync(string rutaArchivo, string nombreArchivo)
    {
        try
        {
            string nombreCubo = ObtenerCuboPorTipoArchivo(nombreArchivo);
            await SubirArchivoAsync(nombreCubo, rutaArchivo, nombreArchivo);
        }
        catch (Exception ex)
        {
            throw new SupabaseStorageException("Error al subir archivo automáticamente.", ex);
        }
    }

    public async Task<Stream> DescargarArchivoAsync(string nombreCubo, string nombreArchivo)
    {
        try
        {
            var bucket = _storageClient.From(nombreCubo);
            var response = await bucket.Download(nombreArchivo, (TransformOptions?)null);
            return new MemoryStream(response);
        }
        catch (Exception ex)
        {
            throw new SupabaseStorageException("Error al descargar el archivo desde Supabase.", ex);
        }
    }

    public async Task EliminarArchivoAsync(string nombreCubo, string nombreArchivo)
    {
        try
        {
            var bucket = _storageClient.From(nombreCubo);
            await bucket.Remove(new List<string> { nombreArchivo });
        }
        catch (Exception ex)
        {
            throw new SupabaseStorageException("Error al eliminar el archivo de Supabase.", ex);
        }
    }

    public async Task<List<string>> ListarArchivosAsync(string nombreCubo)
    {
        try
        {
            var bucket = _storageClient.From(nombreCubo);
            var files = await bucket.List();
            var nombresArchivos = new List<string>();

            foreach (var file in files)
                nombresArchivos.Add(file.Name);

            return nombresArchivos;
        }
        catch (Exception ex)
        {
            throw new SupabaseStorageException("Error al listar archivos del cubo.", ex);
        }
    }

    public async Task<string> BuscarArchivoAsync(string nombreArchivo)
    {
        try
        {
            var buckets = await _storageClient.ListBuckets();
            foreach (var bucket in buckets)
            {
                var archivos = await ListarArchivosAsync(bucket.Name);
                if (archivos.Contains(nombreArchivo))
                    return bucket.Name;
            }

            return null;
        }
        catch (Exception ex)
        {
            throw new SupabaseStorageException("Error al buscar el archivo en Supabase.", ex);
        }
    }

    public async Task<bool> BuscarPorCuboAsync(string nombreCubo, string nombreArchivo)
    {
        try
        {
            var archivos = await ListarArchivosAsync(nombreCubo);
            return archivos.Contains(nombreArchivo);
        }
        catch (Exception ex)
        {
            throw new SupabaseStorageException("Error al buscar el archivo en el cubo especificado.", ex);
        }
    }

    public string ObtenerCuboPorTipoArchivo(string nombreArchivo)
    {
        string extension = Path.GetExtension(nombreArchivo).ToLower();
        return extension switch
        {
            ".jpg" or ".jpeg" or ".png" => "images",
            ".mp4" or ".avi" => "videos",
            ".pdf" => "documents",
            ".mp3" or ".wav" or ".ogg" => "audios",
            _ => "others"
        };
    }

    public async Task ActualizarArchivoAsync(string nombreCubo, string rutaArchivo, string nombreArchivo)
    {
        try
        {
            await EliminarArchivoAsync(nombreCubo, nombreArchivo);
            await SubirArchivoAsync(nombreCubo, rutaArchivo, nombreArchivo);
        }
        catch (Exception ex)
        {
            throw new SupabaseStorageException("Error al actualizar el archivo en Supabase.", ex);
        }
    }

    public async Task<Supabase.Storage.FileObject> ObtenerDetallesArchivoAsync(string nombreCubo, string nombreArchivo)
    {
        try
        {
            var bucket = _storageClient.From(nombreCubo);
            var archivos = await bucket.List();
            return archivos.Find(file => file.Name == nombreArchivo);
        }
        catch (Exception ex)
        {
            throw new SupabaseStorageException("Error al obtener detalles del archivo.", ex);
        }
    }

    public async Task CrearCuboAsync(string nombreCubo)
    {
        try
        {
            await _storageClient.CreateBucket(nombreCubo);
        }
        catch (Exception ex)
        {
            throw new SupabaseStorageException("Error al crear el cubo en Supabase.", ex);
        }
    }

    public async Task EliminarCuboAsync(string nombreCubo)
    {
        try
        {
            await _storageClient.DeleteBucket(nombreCubo);
        }
        catch (Exception ex)
        {
            throw new SupabaseStorageException("Error al eliminar el cubo en Supabase.", ex);
        }
    }
}

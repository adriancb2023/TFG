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


}

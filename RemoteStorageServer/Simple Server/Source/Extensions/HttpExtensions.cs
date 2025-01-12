
using System.Net;
using System.Text;

public static class HttpExtensions
{
    public const string Get = "GET";
    public const string Post = "POST";
    public const string Put = "PUT";
    public const string Delete = "DELETE";

    public static bool IsGet(this HttpListenerRequest request)
    {
        return request.HttpMethod == Get;
    }
    
    public static bool IsPost(this HttpListenerRequest request)
    {
        return request.HttpMethod == Post;
    }
    
    public static bool IsPut(this HttpListenerRequest request)
    {
        return request.HttpMethod == Put;
    }
    
    public static bool IsDelete(this HttpListenerRequest request)
    {
        return request.HttpMethod == Delete;
    }

    public static bool IsAbsolutePath(this HttpListenerRequest request, string path)
    {
        string? uri = request.Url?.AbsolutePath;
        return uri == path;
    }

    public static string ReadText(this HttpListenerRequest request)
    {
        using StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding);
        return reader.ReadToEnd();
    }
    
    public static void WriteText(this HttpListenerResponse response, string value)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(value);
        response.ContentLength64 = buffer.Length;
        response.OutputStream.Write(buffer, 0, buffer.Length);
    }
}
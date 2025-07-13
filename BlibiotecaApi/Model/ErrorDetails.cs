using System.Text.Json;

namespace BlibiotecaApi.Model;

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string? Menssage { get; set; }
    public string? Trace { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
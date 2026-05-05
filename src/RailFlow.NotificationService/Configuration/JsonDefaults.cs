using System.Text.Json;

namespace RailFlow.NotificationService.Configuration;

public static class JsonDefaults
{
    public static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web);
}

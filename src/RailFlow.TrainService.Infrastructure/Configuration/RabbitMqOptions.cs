using System.ComponentModel.DataAnnotations;

namespace RailFlow.TrainService.Infrastructure.Configuration;

public class RabbitMqOptions
{
    [Required]
    public string Host { get; set; } = string.Empty;
    [Required]
    public int Port { get; set; }
    [Required]
    public string User { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}

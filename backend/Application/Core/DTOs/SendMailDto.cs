namespace Application.Core.DTOs;

public class SendMailDto
{
    public required string To { get; set; }
    public required string Name { get; set; }
    public string? Subject { get; set; }
    public required string TemplateName { get; set; }
    public object? ReplaceDto { get; set; }
}
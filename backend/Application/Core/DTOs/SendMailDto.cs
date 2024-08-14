namespace Application.Core.DTOs;

public class SendMailDto
{
    public string To { get; set; }
    public string Name { get; set; }
    public string Subject { get; set; }
    public string TemplateName { get; set; }
    public object ReplaceDto { get; set; }
}
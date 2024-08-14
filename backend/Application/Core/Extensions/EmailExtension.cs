using System.Reflection;

namespace Application.Core.Extensions;

public static class EmailExtension
{
    public static string ReplaceParams(this string template, object model)
    {
        if (string.IsNullOrEmpty(template) || model == null)
            return template;

        var properties = model.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var property in properties)
        {
            var placeholder = $"{{{{{property.Name}}}}}";
            var value = property.GetValue(model)?.ToString() ?? string.Empty;
            template = template.Replace(placeholder, value);
        }

        return template;
    }
}
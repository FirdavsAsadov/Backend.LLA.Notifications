namespace Notifications.Infrastructure.Infrastructure.Common.Settings;

public class TemplateRendiringSettings
{
    public string PlaceholderRegexPattern { get; set; } = default!;
    
    public string PlaceholderValueRegexPattern { get; set; } = default!;
    
    public int RegexMatchTimeoutInSeconds { get; set; } 
}
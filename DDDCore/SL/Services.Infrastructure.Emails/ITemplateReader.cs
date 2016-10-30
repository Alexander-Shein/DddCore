namespace Services.Infrastructure.Emails
{
    public interface ITemplateReader
    {
        Template Read(string name);
    }
}
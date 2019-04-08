namespace TaskForModule5.Localization
{
    public interface ICustomStringLocalizer
    {
        string this[string index] { get; }
        void UpdateCulture();
    }
}
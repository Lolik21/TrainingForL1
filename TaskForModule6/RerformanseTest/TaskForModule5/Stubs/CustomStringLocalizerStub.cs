using TaskForModule5.Localization;

namespace TaskForModule5.Application
{
    public class CustomStringLocalizerStub : ICustomStringLocalizer
    {
        public string this[string index] => string.Empty;

        public void UpdateCulture()
        {
            // stub
        }
    }
}
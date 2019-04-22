using System;
using System.Globalization;
using System.Threading;
using Microsoft.Extensions.Logging;
using TaskForModule5.Localization;

namespace TaskForModule5.LocaleSelectors
{
    public class ConsoleLocaleSelector : ILocaleSelector
    {
        private readonly ILogger _logger;
        private readonly ICustomStringLocalizer _stringLocalizer;

        public ConsoleLocaleSelector(ILogger logger, ICustomStringLocalizer stringLocalizer)
        {
            _logger = logger;
            _stringLocalizer = stringLocalizer;
        }

        public void SelectLocale()
        {
            int selectedNumber = 0;
            while (selectedNumber == 0)
            {
                _logger.Log(LogLevel.None, _stringLocalizer["Russian"]);
                _logger.Log(LogLevel.None, _stringLocalizer["English"]);
                int.TryParse(Console.ReadLine(), out selectedNumber);
                if (selectedNumber <= 0 || selectedNumber > 2)
                {
                    selectedNumber = 0;
                    _logger.Log(LogLevel.None, _stringLocalizer["WrongLanguageNumber"]);
                }
            }
            
            switch (selectedNumber)
            {
                case 1:
                    CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("ru-RU");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ru-RU");
                    break;
                case 2:
                    CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
                    break;
            }
            _stringLocalizer.UpdateCulture();
            _logger.Log(LogLevel.None, _stringLocalizer["LocaleSelected"]);
        }
    }
}
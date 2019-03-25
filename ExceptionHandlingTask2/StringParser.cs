using System;

namespace ExceptionHandlingTask2
{
    internal sealed class StringParser : IStringParser
    {
        public int ParseToInt(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullOrWhiteSpaceException($"{nameof(value)} is null of whitespace");
            }

            var resultValue = 0;
            foreach (var valueChar in value)
            {
                if (!char.IsDigit(valueChar))
                {
                    throw new FormatException($"Invalid format of {value}. Position: {valueChar}");
                }

                var numericValue = (int)char.GetNumericValue(valueChar);
                checked
                {
                    resultValue = resultValue * 10 + numericValue;
                }
            }

            return resultValue;
        }
    }
}
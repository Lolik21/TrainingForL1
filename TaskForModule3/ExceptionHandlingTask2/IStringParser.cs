namespace ExceptionHandlingTask2
{
    /// <summary>
    /// Performs basic string parsing routine
    /// </summary>
    public interface IStringParser
    {
        /// <summary>
        /// Parses string value to int
        /// </summary>
        /// <param name="value">Value to be parsed</param>
        /// <returns>Parsed value</returns>
        /// <exception cref="System.FormatException">
        /// Thrown when <param name="value"></param> has invalid format
        /// </exception>
        /// <exception cref="System.OverflowException">
        /// Thrown when <param name="value"></param> is too big for integer value
        /// </exception>
        /// <exception cref="ArgumentNullOrWhiteSpaceException">
        /// Thrown when <param name="value"></param> is null, whitespace or empty
        /// </exception>
        int ParseToInt(string value);
    }
}
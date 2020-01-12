namespace Fasetto.Word.Lib
{
    /// <summary>
    /// The level of details to output for a logger
    /// </summary>
    public enum LogOutputLevel
    {
        /// <summary>
        /// Logs everything
        /// </summary>
        Debug = 1,

        /// <summary>
        /// Logs all information except, debug information
        /// </summary>
        Verbose,

        /// <summary>
        /// Logs all information message, ignoring any debug and verbose messages
        /// </summary>
        Informative,

        /// <summary>
        /// Log only critical errors and warnings and success, but no general information
        /// </summary>
        Critical,

        /// <summary>
        /// The logger will never output anything
        /// </summary>
        Nothing = 7
    }
}

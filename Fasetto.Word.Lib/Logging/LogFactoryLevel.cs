namespace Fasetto.Word.Lib
{
    /// <summary>
    /// The severity of the log message
    /// </summary>
    public enum LogFactoryLevel
    {
        /// <summary>
        /// Logs everything
        /// </summary>
        Debug,

        /// <summary>
        /// Logs all information except, debug information
        /// </summary>
        Verbose,

        /// <summary>
        /// Logs all information message, ignoring any debug and verbose messages
        /// </summary>
        Informative,

        /// <summary>
        /// Logs only warnings, errors and standard messages
        /// </summary>
        Normal,

        /// <summary>
        /// Log only critical errors and warnings, no general information
        /// </summary>
        Critical,

        /// <summary>
        /// The logger will never output anything
        /// </summary>
        Nothing
    }
}

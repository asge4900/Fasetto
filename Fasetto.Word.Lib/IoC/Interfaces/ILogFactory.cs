﻿using System;

namespace Fasetto.Word.Lib
{
    /// <summary>
    /// Holds a bunch of loggers to log messages for the user
    /// </summary>
    public interface ILogFactory
    {
        #region Properties

        /// <summary>
        /// The level of logging to output
        /// </summary>
        LogFactoryLevel LogOutputLevel { get; set; }

        /// <summary>
        /// If true includes the origin of where the log message was logged from
        /// such as the class name, line number and file name
        /// </summary>
        bool IncludeLogOriginDetails { get; set; }

        #endregion

        #region Events

        /// <summary>
        /// Fires whenever a new log arrives
        /// </summary>
        event Action<(string Message, LogFactoryLevel Level)> NewLog;

        #endregion      

        #region Methods

        /// <summary>
        /// Adds the specific logger to this factory
        /// </summary>
        /// <param name="logger">The logger</param>
        void AddLogger(ILogger logger);

        /// <summary>
        /// Removes the specific logger from this factory
        /// </summary>
        /// <param name="logger">The logger</param>
        void RemoveLogger(ILogger logger);

        /// <summary>
        /// Logs the specific message to all loggers in this factory
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="level">The level of the message being logged</param>
        /// <param name="origin">The method/function this message was logged in</param>
        /// <param name="filePath">The code filename that this message was logged from</param>
        /// <param name="lineNumber">The line code in the filename this message was logged from</param>
        void Log(string message, LogFactoryLevel level = LogFactoryLevel.Informative, string origin = "", string filePath = "", int lineNumber = 0);

        #endregion
    }
}

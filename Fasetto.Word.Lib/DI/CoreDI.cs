using Dna;

namespace Fasetto.Word.Lib
{
    /// <summary>
    /// The IoC container for our application
    /// </summary>
    public static class CoreDI
    {
        #region Properties      

        /// <summary>
        /// A shortcut to access the <see cref="IFileManager"/>
        /// </summary>
        public static IFileManager FileManager => Framework.Service<IFileManager>();

        /// <summary>
        /// A shortcut to access the <see cref="ITaskManager"/>
        /// </summary>
        public static ITaskManager TaskManager => Framework.Service<ITaskManager>();       

        #endregion 
    }
}

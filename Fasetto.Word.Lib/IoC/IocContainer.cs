using Ninject;

namespace Fasetto.Word.Lib
{
    /// <summary>
    /// The IoC container for our application
    /// </summary>
    public static class IocContainer
    {
        /// <summary>
        /// The kernel for our IoC container
        /// </summary>
        public static IKernel Kernel { get; private set; } = new StandardKernel(); 
    }
}

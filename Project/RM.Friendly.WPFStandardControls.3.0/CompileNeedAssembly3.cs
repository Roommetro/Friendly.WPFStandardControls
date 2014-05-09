using System.Windows;
namespace RM.Friendly.WPFStandardControls
{
    /// <summary>
    /// Need Compile Assemblys.
    /// For TestAssistant plugin.
    /// </summary>
    public static class CompileNeedAssembly3
    {
        /// <summary>
        /// Get Assemblys.
        /// </summary>
        /// <returns>Assemblys.</returns>
        public static string[] GetAssemblies()
        {
            return new string[] { 
            typeof(AutoResizedEventArgs).Assembly.Location,
            typeof(Application).Assembly.Location,
            typeof(DependencyObject).Assembly.Location
            };
        }
    }
}

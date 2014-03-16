using Codeer.Friendly;
using Codeer.Friendly.Windows;

namespace RM.Friendly.WPFStandardControls
{
    /// <summary>
    /// 
    /// </summary>
    public class WPFControlsBase : IAppVarOwner
    {
        public WindowsAppFriend App
        {
            get;
            private set;
        }
        public AppVar AppVar
        {
            get;
            private set;
        }

        protected WPFControlsBase(WindowsAppFriend app, AppVar appVar)
        {
            App = app;
            AppVar = appVar;
            WindowsAppExpander.LoadAssembly(app, GetType().Assembly);
        }
    }
}

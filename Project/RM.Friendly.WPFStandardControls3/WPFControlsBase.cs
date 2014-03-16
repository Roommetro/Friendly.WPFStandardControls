using Codeer.Friendly;
using Codeer.Friendly.Windows;
using System.Diagnostics;

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

        protected T GetPropValue<T>() {
            var stackTrace = new StackTrace();
            var frame = stackTrace.GetFrame(1);
            var methodName = frame.GetMethod().Name;
            if (methodName.StartsWith("get_")) {
                methodName = methodName.Substring(4);
            }
            return this.GetPropValue<T>(methodName);
        }

        protected T GetPropValue<T>(string propName) {
            return (T)this.AppVar[propName]().Core;
        }
    }
}

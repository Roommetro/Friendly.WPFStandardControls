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
            return this.GetPropValue<T>(this.GetCallerName());
        }

        protected T GetPropValue<T>(string propName) {
            return (T)this.AppVar[propName]().Core;
        }

        protected void SetPropValue<T>(T value) {
            this.SetPropValue(this.GetCallerName(), value);
        }

        protected void SetPropValue<T>(string propName, T value) {
            this.AppVar[propName](value);
        }

        private string GetCallerName(int skipCount = 1) {
            var stackTrace = new StackTrace();
            var frame = stackTrace.GetFrame(skipCount + 1);
            var methodName = frame.GetMethod().Name;
            if (methodName.StartsWith("get_") || methodName.StartsWith("set_")) {
                methodName = methodName.Substring(4);
            }
            return methodName;
        }
    }
}

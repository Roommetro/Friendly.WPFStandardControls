using Codeer.Friendly;
using Codeer.Friendly.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

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

        protected void EmulateInTarget(params object[] args) {
            this.EmulateInTarget((Async)null, args);
        }

        // 対象メソッド第一引数がstringの場合、methodNameと誤解されるから、区別がつくために最後に「method」
        // をつけました
        protected void EmulateInTargetMethod(string methodName, params object[] args) {
            this.EmulateInTarget(methodName, (Async)null, args);
        }

        protected void EmulateInTarget(Async async, params object[] args) {
            this.EmulateInTargetMethod(this.GetCallerName() + "InTarget", async, args);
        }

        protected void EmulateInTargetMethod(string methodName, Async async, params object[] args) {
            this.EmulateInTargetInternal(methodName, this.GetCallerMethod().DeclaringType, async, args);
        }

        private void EmulateInTargetInternal(string methodName, Type targetType, Async async, params object[] args) {
            var arguments = new List<object>();
            arguments.Add(this.AppVar);
            arguments.AddRange(args);

            var op = async == null ? this.App[targetType, methodName] : this.App[targetType, methodName, async];
            op(arguments.ToArray());
        }

        private string GetCallerName(int skipCount = 1) {
            var methodName = GetCallerMethod(skipCount + 1).Name;
            if (methodName.StartsWith("get_") || methodName.StartsWith("set_")) {
                methodName = methodName.Substring(4);
            }
            return methodName;
        }

        private MethodBase GetCallerMethod(int skipCount = 1) {
            var stackTrace = new StackTrace();
            MethodBase method;
            do {
                ++skipCount;
                method = stackTrace.GetFrame(skipCount).GetMethod();
                // WPFControlsBaseのメソッドをスキップする
            } while (method.DeclaringType == typeof(WPFControlsBase) && 
                     skipCount + 1 < stackTrace.FrameCount);
            return method;
        }
    }
}

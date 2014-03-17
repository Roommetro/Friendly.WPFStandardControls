using System;
using Codeer.Friendly;
using Codeer.Friendly.Windows;
using RM.Friendly.WPFStandardControls.Inside;
using System.Reflection;
using System.Collections.Generic;
using System.Diagnostics;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// This is the base class of AppVar wrappers.
    /// </summary>
#else
    /// <summary>
    /// AppVarラップアイテムの基本クラスです。
    /// </summary>
#endif
    public class AppVarWrapper : IAppVarOwner
    {
        WindowsAppFriend _app;
        AppVar _appVar;

#if ENG
        /// <summary>
        /// Application manipulation object.
        /// </summary>
#else
        /// <summary>
        /// アプリケーション操作クラスです。
        /// </summary>
#endif
        public WindowsAppFriend App { get { return _app; } }

#if ENG
        /// <summary>
        /// Variable manipulation object within the target application.
        /// </summary>
#else
        /// <summary>
        /// アプリケーション変数操作クラスです。
        /// </summary>
#endif
        public AppVar AppVar { get { return _appVar; } }

#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="app">Application manipulation object.</param>
        /// <param name="appVar">Application variable object for the control.</param>
#else
        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="appVar">アプリケーション内変数。</param>
#endif
        protected AppVarWrapper(WindowsAppFriend app, AppVar appVar)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app");
            }
            if (appVar == null)
            {
                throw new ArgumentNullException("appVar");
            }
            _app = app;
            _appVar = appVar;
            Initializer3.Initialize(app);
        }

#if ENG
        /// <summary>
        /// Returns a delegate to invoke static operations in the test application.
        /// </summary>
        /// <param name="operation">The operation name.</param>
        /// <returns>Delegate to invoke static operations in the application.</returns>
#else
        /// <summary>
        /// テスト対象アプリケーション内の変数の操作を呼び出すdelegateを取得します。
        /// </summary>
        /// <param name="operation">操作。</param>
        /// <returns>操作実行delegate。</returns>
#endif
        public FriendlyOperation this[string operation]
        {
            get { return _appVar[operation]; }
        }

#if ENG
        /// <summary>
        /// Returns a delegate to invoke static operations in the test application.
        /// </summary>
        /// <param name="operation">The operation name.</param>
        /// <param name="async">Asynchronous execution.</param>
        /// <returns>Delegate to invoke static operations in the application.</returns>
#else
        /// <summary>
        /// テスト対象アプリケーション内の変数の操作を呼び出すdelegateを取得します。
        /// </summary>
        /// <param name="operation">操作。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
        /// <returns>操作実行delegate。</returns>
#endif
        public FriendlyOperation this[string operation, Async async]
        {
            get { return _appVar[operation, async]; }
        }

#if ENG
        /// <summary>
        /// Returns a delegate to invoke static operations in the test application.
        /// </summary>
        /// <param name="operation">The operation name.</param>
        /// <param name="operationTypeInfo">
        /// Operation type information. 
        /// Used when there is more than one overload for the operation or you want to call an operation by the same name in a parent class. 
        /// Often the operation can be resolved based on its parameters without specifying the operation type info.
        /// </param>
        /// <returns>Delegate to invoke static operations in the application.</returns>
#else
        /// <summary>
        /// テスト対象アプリケーション内の変数の操作を呼び出すdelegateを取得します。
        /// </summary>
        /// <param name="operation">操作。</param>
        /// <param name="operationTypeInfo">操作タイプ情報。</param>
        /// <returns>操作実行delegate。</returns>
#endif
        public FriendlyOperation this[string operation, OperationTypeInfo operationTypeInfo]
        {
            get { return _appVar[operation, operationTypeInfo]; }
        }

#if ENG
        /// <summary>
        /// Returns a delegate to invoke static operations in the test application.
        /// </summary>
        /// <param name="operation">The operation name.</param>
        /// <param name="operationTypeInfo">
        /// Operation type information. 
        /// Used when there is more than one overload for the operation or you want to call an operation by the same name in a parent class. 
        /// Often the operation can be resolved based on its parameters without specifying the operation type info.
        /// </param>
        /// <param name="async">Asynchronous execution.</param>
        /// <returns>Delegate to invoke static operations in the application.</returns>
#else
        /// <summary>
        /// テスト対象アプリケーション内の変数の操作を呼び出すdelegateを取得します。
        /// </summary>
        /// <param name="operation">操作。</param>
        /// <param name="operationTypeInfo">操作タイプ情報。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
        /// <returns>操作実行delegate。</returns>
#endif
        public FriendlyOperation this[string operation, OperationTypeInfo operationTypeInfo, Async async]
        {
            get { return _appVar[operation, operationTypeInfo, async]; }
        }

        protected T GetPropValue<T>()
        {
            return this.GetPropValue<T>(this.GetCallerName());
        }

        private T GetPropValue<T>(string propName)
        {
            return (T)this.AppVar[propName]().Core;
        }

        protected AppVar EmulateInTarget(params object[] args)
        {
            return this.EmulateInTarget((Async)null, args);
        }

        protected AppVar EmulateInTarget(Async async, params object[] args)
        {
            return this.InvokeStaticOperation(this.GetCallerName() + "InTarget", async, args);
        }

        private AppVar InvokeStaticOperation(string methodName, Async async, params object[] args)
        {
            return this.InvokeStaticOperation(methodName, GetType(), async, args);
        }

        private AppVar InvokeStaticOperation(string methodName, Type targetType, Async async, params object[] args)
        {
            var arguments = new List<object>();
            arguments.Add(this.AppVar);
            arguments.AddRange(args);
            var op = async == null ? this.App[targetType, methodName] : this.App[targetType, methodName, async];
            return op(arguments.ToArray());
        }

        private string GetCallerName(int skipCount = 1)
        {
            var methodName = GetCallerMethod(skipCount + 1).Name;
            if (methodName.StartsWith("get_") || methodName.StartsWith("set_"))
            {
                methodName = methodName.Substring(4);
            }
            return methodName;
        }

        private MethodBase GetCallerMethod(int skipCount)
        {
            var stackTrace = new StackTrace();
            MethodBase method = null;
            Type currentType = GetType();
            bool findMyType = false;
            for (int i = skipCount; i < stackTrace.FrameCount; i++)
            {
                MethodBase methodCheck = stackTrace.GetFrame(i).GetMethod();
                if (findMyType)
                {
                    if (methodCheck.DeclaringType != currentType)
                    {
                        break;
                    }
                }
                else
                {
                    if (methodCheck.DeclaringType == currentType)
                    {
                        method = methodCheck;
                    }
                }
            }
            return method;
        }
    }
}

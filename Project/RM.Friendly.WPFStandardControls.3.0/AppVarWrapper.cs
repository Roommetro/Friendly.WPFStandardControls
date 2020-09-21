using System;
using Codeer.Friendly;
using Codeer.Friendly.Windows;
using RM.Friendly.WPFStandardControls.Inside;
using System.Reflection;
using System.Collections.Generic;
using System.Diagnostics;
using Codeer.Friendly.DotNetExecutor;

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
    public partial class AppVarWrapper<CoreType> : IAppVarOwner
    {
        static Dictionary<Type, Type> _invokeTypes = new Dictionary<Type, Type>();

        WindowsAppFriend _app;
        AppVar _appVar;

        static Dictionary<Type, Type> _invokeTypeDic = new Dictionary<Type, Type>();
        static AppVar _typeFinder;

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
        /// <param name="appVar">Application variable object for the control.</param>
#else
        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="appVar">アプリケーション内変数。</param>
#endif
        protected AppVarWrapper(AppVar appVar)
        {
            if (appVar == null)
            {
                throw new ArgumentNullException("appVar");
            }
            _app = (WindowsAppFriend)appVar.App;
            _appVar = appVar;
            WPFStandardControls_3.Injection(_app);
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

        /// <summary>
        /// Getter.
        /// </summary>
        /// <typeparam name="T">Type of return value.</typeparam>
        /// <param name="name">name.</param>
        /// <returns>Return value.</returns>
        protected T Getter<T>(string name)
        {
            return (T)this.AppVar[name]().Core;
        }

        private AppVar InvokeStatic(string methodName, params object[] args)
        {
            return this.InvokeStatic(methodName, null, args);
        }

        private AppVar InvokeStatic(string methodName, Async async, params object[] args)
        {
            return this.InvokeStatic(methodName, GetInvokeType(), async, args);
        }

        private AppVar InvokeStatic(string methodName, Type targetType, Async async, params object[] args)
        {
            var arguments = new List<object>();
            arguments.Add(this.AppVar);
            arguments.AddRange(args);
            var op = async == null ? this.App[targetType, methodName] : this.App[targetType, methodName, async];
            return op(arguments.ToArray());
        }

#if ENG
        /// <summary>
        /// Type for invoke in target
        /// </summary>
        /// <returns>Type for invoke in target.</returns>
#else
        /// <summary>
        /// 対象プロセス内部で実行する型の取得
        /// </summary>
        /// <returns>対象プロセス内部で実行する型</returns>
#endif
        protected virtual Type GetInvokeType()
        {
            var type = GetType();
            Type invokeType = null;

            lock (_invokeTypeDic)
            {
                if (_invokeTypeDic.TryGetValue(type, out invokeType)) return invokeType;
            }

            if (_typeFinder == null)
            {
                _typeFinder = App.Dim(new NewInfo<TypeFinder>());
            }

            invokeType = type;
            while (invokeType != null)
            {
                if (!_typeFinder["GetType"](invokeType.FullName).IsNull) break;
                invokeType = invokeType.BaseType;
            }

            if (invokeType == null) throw new NotSupportedException();

            lock (_invokeTypeDic)
            {
                _invokeTypeDic[type] = invokeType;
            }

            return invokeType;
        }
    }
}

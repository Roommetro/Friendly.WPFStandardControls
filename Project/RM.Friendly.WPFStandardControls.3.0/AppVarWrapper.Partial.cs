using Codeer.Friendly;

namespace RM.Friendly.WPFStandardControls
{
    //Helper for InvokeStatic.
    partial class AppVarWrapper<CoreType>
    {
        /// <summary>
        /// Action.
        /// </summary>
        /// <typeparam name="T1">Type of argument 1.</typeparam>
        /// <param name="t1">argument 1.</param>
        protected delegate void MyAction<T1>(T1 t1);

        /// <summary>
        /// Action.
        /// </summary>
        /// <typeparam name="T1">Type of argument 1.</typeparam>
        /// <typeparam name="T2">Type of argument 2.</typeparam>
        /// <param name="t1">argument 1.</param>
        /// <param name="t2">argument 2.</param>
        protected delegate void MyAction<T1, T2>(T1 t1, T2 t2);

        /// <summary>
        /// Action.
        /// </summary>
        /// <typeparam name="T1">Type of argument 1.</typeparam>
        /// <typeparam name="T2">Type of argument 2.</typeparam>
        /// <typeparam name="T3">Type of argument 3.</typeparam>
        /// <param name="t1">argument 1.</param>
        /// <param name="t2">argument 2.</param>
        /// <param name="t3">argument 3.</param>
        protected delegate void MyAction<T1, T2, T3>(T1 t1, T2 t2, T3 t3);

        /// <summary>
        /// Action.
        /// </summary>
        /// <typeparam name="T1">Type of argument 1.</typeparam>
        /// <typeparam name="T2">Type of argument 2.</typeparam>
        /// <typeparam name="T3">Type of argument 3.</typeparam>
        /// <typeparam name="T4">Type of argument 4.</typeparam>
        /// <param name="t1">argument 1.</param>
        /// <param name="t2">argument 2.</param>
        /// <param name="t3">argument 3.</param>
        /// <param name="t4">argument 4.</param>
        protected delegate void MyAction<T1, T2, T3, T4>(T1 t1, T2 t2, T3 t3, T4 t4);

        /// <summary>
        /// Invoke static method.
        /// </summary>
        /// <param name="method">method.</param>
        protected void InvokeStatic(MyAction<CoreType> method)
        {
            InvokeStatic(method.Method.Name);
        }

        /// <summary>
        /// Invoke static method.
        /// </summary>
        /// <typeparam name="T1">Type of argument 1.</typeparam>
        /// <param name="method">method.</param>
        /// /// <param name="t1">argument 1.</param>
        protected void InvokeStatic<T1>(MyAction<CoreType, T1> method, T1 t1)
        {
            InvokeStatic(method.Method.Name, t1);
        }

        /// <summary>
        /// Invoke static method.
        /// </summary>
        /// <typeparam name="T1">Type of argument 1.</typeparam>
        /// <typeparam name="T2">Type of argument 2.</typeparam>
        /// <param name="method">method.</param>
        /// <param name="t1">argument 1.</param>
        /// <param name="t2">argument 2.</param>
        protected void InvokeStatic<T1, T2>(MyAction<CoreType, T1, T2> method, T1 t1, T2 t2)
        {
            InvokeStatic(method.Method.Name, t1, t2);
        }

        /// <summary>
        /// Invoke static method.
        /// </summary>
        /// <typeparam name="T1">Type of argument 1.</typeparam>
        /// <typeparam name="T2">Type of argument 2.</typeparam>
        /// <typeparam name="T3">Type of argument 3.</typeparam>
        /// <param name="method">method.</param>
        /// <param name="t1">argument 1.</param>
        /// <param name="t2">argument 2.</param>
        /// <param name="t3">argument 3.</param>
        protected void InvokeStatic<T1, T2, T3>(MyAction<CoreType, T1, T2, T3> method, T1 t1, T2 t2, T3 t3)
        {
            InvokeStatic(method.Method.Name, t1, t2, t3);
        }

        /// <summary>
        /// Invoke static method.
        /// </summary>
        /// <param name="method">method.</param>
        /// <param name="async">async.</param>
        protected void InvokeStatic(MyAction<CoreType> method, Async async)
        {
            InvokeStatic(method.Method.Name, async);
        }

        /// <summary>
        /// Invoke static method.
        /// </summary>
        /// <typeparam name="T1">Type of argument 1.</typeparam>
        /// <param name="method">method.</param>
        /// <param name="async">async.</param>
        /// <param name="t1"></param>
        protected void InvokeStatic<T1>(MyAction<CoreType, T1> method, Async async, T1 t1)
        {
            InvokeStatic(method.Method.Name, async, t1);
        }

        /// <summary>
        /// Invoke static method.
        /// </summary>
        /// <typeparam name="T1">Type of argument 1.</typeparam>
        /// <typeparam name="T2">Type of argument 2.</typeparam>
        /// <param name="method">method.</param>
        /// <param name="async">async.</param>
        /// <param name="t1">argument 1.</param>
        /// <param name="t2">argument 2.</param>
        protected void InvokeStatic<T1, T2>(MyAction<CoreType, T1, T2> method, Async async, T1 t1, T2 t2)
        {
            InvokeStatic(method.Method.Name, async, t1, t2);
        }

        /// <summary>
        /// Invoke static method.
        /// </summary>
        /// <typeparam name="T1">Type of argument 1.</typeparam>
        /// <typeparam name="T2">Type of argument 2.</typeparam>
        /// <typeparam name="T3">Type of argument 3.</typeparam>
        /// <param name="method">method.</param>
        /// <param name="async">async.</param>
        /// <param name="t1">argument 1.</param>
        /// <param name="t2">argument 2.</param>
        /// <param name="t3">argument 3.</param>
        protected void InvokeStatic<T1, T2, T3>(MyAction<CoreType, T1, T2, T3> method, Async async, T1 t1, T2 t2, T3 t3)
        {
            InvokeStatic(method.Method.Name, async, t1, t2, t3);
        }

        /// <summary>
        /// Func.
        /// </summary>
        /// <typeparam name="T1">Type of argument 1.</typeparam>
        /// <typeparam name="Ret">Type of return value.</typeparam>
        /// <param name="t1">argument 1.</param>
        /// <returns>return value.</returns>
        protected delegate Ret MyFunc<T1, out Ret>(T1 t1);

        /// <summary>
        /// Func.
        /// </summary>
        /// <typeparam name="T1">Type of argument 1.</typeparam>
        /// <typeparam name="T2">Type of argument 2.</typeparam>
        /// <typeparam name="Ret">Type of return value.</typeparam>
        /// <param name="t1">argument 1.</param>
        /// <param name="t2">argument 2.</param>
        /// <returns>return value.</returns>
        protected delegate Ret MyFunc<T1, T2, out Ret>(T1 t1, T2 t2);

        /// <summary>
        /// Func.
        /// </summary>
        /// <typeparam name="T1">Type of argument 1.</typeparam>
        /// <typeparam name="T2">Type of argument 2.</typeparam>
        /// <typeparam name="T3">Type of argument 3.</typeparam>
        /// <typeparam name="Ret">Type of return value.</typeparam>
        /// <param name="t1">argument 1.</param>
        /// <param name="t2">argument 2.</param>
        /// <param name="t3">argument 3.</param>
        /// <returns>return value.</returns>
        protected delegate Ret MyFunc<T1, T2, T3, out Ret>(T1 t1, T2 t2, T3 t3);

        /// <summary>
        /// Invoke static method.
        /// </summary>
        /// <typeparam name="Ret">Type of return value.</typeparam>
        /// <param name="method">method.</param>
        /// <param name="retType">The hint of return value type.</param>
        /// <returns>return value.</returns>
        protected Ret InvokeStatic<Ret>(MyFunc<CoreType, Ret> method, Type<Ret> retType)
        {
            return (Ret)InvokeStatic(method.Method.Name).Core;
        }

        /// <summary>
        /// Invoke static method.
        /// </summary>
        /// <typeparam name="T1">Type of argument 1.</typeparam>
        /// <typeparam name="Ret">Type of return value.</typeparam>
        /// <param name="method">method.</param>
        /// <param name="retType">The hint of return value type.</param>
        /// <param name="t1">argument 1.</param>
        /// <returns>return value.</returns>
        protected Ret InvokeStatic<T1, Ret>(MyFunc<CoreType, T1, Ret> method, Type<Ret> retType, T1 t1)
        {
            return (Ret)InvokeStatic(method.Method.Name, t1).Core;
        }

        /// <summary>
        /// Invoke static method.
        /// </summary>
        /// <typeparam name="T1">Type of argument 1.</typeparam>
        /// <typeparam name="T2">Type of argument 2.</typeparam>
        /// <typeparam name="Ret">Type of return value.</typeparam>
        /// <param name="method">method.</param>
        /// <param name="retType">The hint of return value type.</param>
        /// <param name="t1">argument 1.</param>
        /// <param name="t2">argument 2.</param>
        /// <returns>return value.</returns>
        protected Ret InvokeStatic<T1, T2, Ret>(MyFunc<CoreType, T1, T2, Ret> method, Type<Ret> retType, T1 t1, T2 t2)
        {
            return (Ret)InvokeStatic(method.Method.Name, t1, t2).Core;
        }

        /// <summary>
        /// Invoke static method.
        /// </summary>
        /// <typeparam name="T1">Type of argument 1.</typeparam>
        /// <typeparam name="T2">Type of argument 2.</typeparam>
        /// <typeparam name="T3">Type of argument 3.</typeparam>
        /// <typeparam name="Ret">Type of return value.</typeparam>
        /// <param name="method">method.</param>
        /// <param name="retType">The hint of return value type.</param>
        /// <param name="t1">argument 1.</param>
        /// <param name="t2">argument 2.</param>
        /// <param name="t3">argument 3.</param>
        /// <returns>return value.</returns>
        protected Ret InvokeStatic<T1, T2, T3, Ret>(MyFunc<CoreType, T1, T2, Ret> method, Type<Ret> retType, T1 t1, T2 t2, T3 t3)
        {
            return (Ret)InvokeStatic(method.Method.Name, t1, t2, t3).Core;
        }

        /// <summary>
        /// Invoke static method.
        /// </summary>
        /// <typeparam name="Ret">Type of return value.</typeparam>
        /// <param name="method">method.</param>
        /// <param name="retType">The hint of return value type.</param>
        /// <returns>return value.</returns>
        protected AppVar InvokeStaticRetAppVar<Ret>(MyFunc<CoreType, Ret> method, Type<Ret> retType)
        {
            return InvokeStatic(method.Method.Name);
        }

        /// <summary>
        /// Invoke static method.
        /// </summary>
        /// <typeparam name="T1">Type of argument 1.</typeparam>
        /// <typeparam name="Ret">Type of return value.</typeparam>
        /// <param name="method">method.</param>
        /// <param name="retType">The hint of return value type.</param>
        /// <param name="t1">argument 1.</param>
        /// <returns>return value.</returns>
        protected AppVar InvokeStaticRetAppVar<T1, Ret>(MyFunc<CoreType, T1, Ret> method, Type<Ret> retType, T1 t1)
        {
            return InvokeStatic(method.Method.Name, t1);
        }

        /// <summary>
        /// Invoke static method.
        /// </summary>
        /// <typeparam name="T1">Type of argument 1.</typeparam>
        /// <typeparam name="T2">Type of argument 2.</typeparam>
        /// <typeparam name="Ret">Type of return value.</typeparam>
        /// <param name="method">method.</param>
        /// <param name="retType">The hint of return value type.</param>
        /// <param name="t1">argument 1.</param>
        /// <param name="t2">argument 2.</param>
        /// <returns>return value.</returns>
        protected AppVar InvokeStaticRetAppVar<T1, T2, Ret>(MyFunc<CoreType, T1, T2, Ret> method, Type<Ret> retType, T1 t1, T2 t2)
        {
            return InvokeStatic(method.Method.Name, t1, t2);
        }

        /// <summary>
        /// Invoke static method.
        /// </summary>
        /// <typeparam name="T1">Type of argument 1.</typeparam>
        /// <typeparam name="T2">Type of argument 2.</typeparam>
        /// <typeparam name="T3">Type of argument 3.</typeparam>
        /// <typeparam name="Ret">Type of return value.</typeparam>
        /// <param name="method">method.</param>
        /// <param name="retType">The hint of return value type.</param>
        /// <param name="t1">argument 1.</param>
        /// <param name="t2">argument 2.</param>
        /// <param name="t3">argument 3.</param>
        /// <returns>return value.</returns>
        protected AppVar InvokeStaticRetAppVar<T1, T2, T3, Ret>(MyFunc<CoreType, T1, T2, Ret> method, Type<Ret> retType, T1 t1, T2 t2, T3 t3)
        {
            return InvokeStatic(method.Method.Name, t1, t2, t3);
        }

        /// <summary>
        /// The hint of type.
        /// </summary>
        /// <typeparam name="T">type.</typeparam>
        protected class Type<T> { }

        /// <summary>
        /// Get the hint of return value type.
        /// </summary>
        /// <typeparam name="T">type.</typeparam>
        /// <returns>The hint of return value type.</returns>
        protected Type<T> Ret<T>() { return null; }
    }
}

using Codeer.Friendly;

namespace RM.Friendly.WPFStandardControls
{
    //Helper for InvokeStatic.
    partial class AppVarWrapper<CoreType>
    {
        protected delegate void MyAction<T1>(T1 t);
        protected delegate void MyAction<T1, T2>(T1 t1, T2 t2);
        protected delegate void MyAction<T1, T2, T3>(T1 t1, T2 t2, T3 t3);
        protected delegate void MyAction<T1, T2, T3, T4>(T1 t1, T2 t2, T3 t3, T4 t4);
        
        protected void InvokeStatic(MyAction<CoreType> method)
        {
            InvokeStatic(method.Method.Name);
        }
        protected void InvokeStatic<T1>(MyAction<CoreType, T1> method, T1 t)
        {
            InvokeStatic(method.Method.Name, t);
        }

        protected void InvokeStatic<T1, T2>(MyAction<CoreType, T1, T2> method, T1 t1, T2 t2)
        {
            InvokeStatic(method.Method.Name, t1, t2);
        }

        protected void InvokeStatic<T1, T2, T3>(MyAction<CoreType, T1, T2, T3> method, T1 t1, T2 t2, T3 t3)
        {
            InvokeStatic(method.Method.Name, t1, t2, t3);
        }

        protected void InvokeStatic(MyAction<CoreType> method, Async async)
        {
            InvokeStatic(method.Method.Name, async);
        }

        protected void InvokeStatic<T1>(MyAction<CoreType, T1> method, Async async, T1 t)
        {
            InvokeStatic(method.Method.Name, async, t);
        }

        protected void InvokeStatic<T1, T2>(MyAction<CoreType, T1, T2> method, Async async, T1 t1, T2 t2)
        {
            InvokeStatic(method.Method.Name, async, t1, t2);
        }

        protected void InvokeStatic<T1, T2, T3>(MyAction<CoreType, T1, T2, T3> method, Async async, T1 t1, T2 t2, T3 t3)
        {
            InvokeStatic(method.Method.Name, async, t1, t2, t3);
        }

        //func doesn't need async.
        protected delegate Ret MyFunc<T1, out Ret>(T1 t);
        protected delegate Ret MyFunc<T1, T2, out Ret>(T1 t1, T2 t2);
        protected delegate Ret MyFunc<T1, T2, T3, out Ret>(T1 t1, T2 t2, T3 t3);

        protected Ret InvokeStatic<Ret>(MyFunc<CoreType, Ret> method, Type<Ret> retType)
        {
            return (Ret)InvokeStatic(method.Method.Name).Core;
        }

        protected Ret InvokeStatic<T1, Ret>(MyFunc<CoreType, T1, Ret> method, Type<Ret> retType, T1 t)
        {
            return (Ret)InvokeStatic(method.Method.Name, t).Core;
        }

        protected Ret InvokeStatic<T1, T2, Ret>(MyFunc<CoreType, T1, T2, Ret> method, Type<Ret> retType, T1 t1, T2 t2)
        {
            return (Ret)InvokeStatic(method.Method.Name, t1, t2).Core;
        }

        protected Ret InvokeStatic<T1, T2, T3, Ret>(MyFunc<CoreType, T1, T2, Ret> method, Type<Ret> retType, T1 t1, T2 t2, T3 t3)
        {
            return (Ret)InvokeStatic(method.Method.Name, t1, t2, t3).Core;
        }

        //helper for conjecture return value type.
        protected class Type<T> { }
        protected Type<T> Ret<T>() { return new Type<T>(); }
    }
}

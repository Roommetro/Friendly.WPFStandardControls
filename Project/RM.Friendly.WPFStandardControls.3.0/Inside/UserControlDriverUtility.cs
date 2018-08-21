using Codeer.Friendly;
using System;

namespace RM.Friendly.WPFStandardControls.Inside
{
    static class UserControlDriverUtility
    {
        internal static T AttachDriver<T>(IAppVarOwner appVar) where T : class
            => (T)Activator.CreateInstance(typeof(T), appVar.AppVar);
    }
}
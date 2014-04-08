using System;
using System.Reflection;
using Codeer.Friendly.Windows;

namespace RM.Friendly.WPFStandardControls.Inside
{
    static class Initializer4
    {
        internal static void Initialize(WindowsAppFriend app)
        {
            string key = typeof(Initializer4) + "[Initialize]";
            object isInit;
            if (!app.TryGetAppControlInfo(key, out isInit))
            {
                WindowsAppExpander.LoadAssembly(app, typeof(Initializer4).Assembly);
                ResourcesLocal.Initialize(app);
                app.AddAppControlInfo(key, true);
            }
        }
    }
}

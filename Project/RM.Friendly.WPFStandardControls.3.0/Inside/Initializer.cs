using System;
using System.Reflection;
using Codeer.Friendly.Windows;

namespace RM.Friendly.WPFStandardControls.Inside
{
    static class Initializer3
    {
        internal static void Initialize(WindowsAppFriend app)
        {
            string key = typeof(Initializer3) + "[Initialize]";
            object isInit;
            if (!app.TryGetAppControlInfo(key, out isInit))
            {
                WindowsAppExpander.LoadAssembly(app, typeof(Initializer3).Assembly);
                ResourcesLocal3.Initialize(app);
                app.AddAppControlInfo(key, true);
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace RM.Friendly.WPFStandardControls.Generator.CreateDriver
{
    internal static class ReflectionAccessor
    {
        private static readonly Dictionary<string, Type> FullNameAndType = new Dictionary<string, Type>();

        public static Type GetType(string typeFullName)
        {
            lock (FullNameAndType)
            {
                //キャッシュを見る
                if (FullNameAndType.TryGetValue(typeFullName, out Type type))
                {
                    return type;
                }

                //各アセンブリに問い合わせる			
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                foreach (var assembly in assemblies)
                {
                    type = assembly.GetType(typeFullName);
                    if (type != null)
                    {
                        FullNameAndType.Add(typeFullName, type);
                        break;
                    }
                }
                return type;
            }
        }
    }
}

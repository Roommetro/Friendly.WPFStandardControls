using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;

namespace RM.Friendly.WPFStandardControls.Generator.CreateDriver
{
    internal class WPFCustomIdentify
    {
        private delegate object GenerateIdentifyCode(DependencyObject target, IEnumerable<DependencyObject> tree);

        private class IdentifyGeneratorHolder
        {
            public int Priority { get; }
            public GenerateIdentifyCode GenerateIdentifyCode { get; }

            public IdentifyGeneratorHolder(int priority, GenerateIdentifyCode generateIdentifyCode)
            {
                Priority = priority;
                GenerateIdentifyCode = generateIdentifyCode;
            }
        }

        private readonly List<IdentifyGeneratorHolder> _identifyGenerators = new List<IdentifyGeneratorHolder>();

        public WPFCustomIdentify()
        {
            //WPFの要素特定クラス取得
            var identifyGeneratorType = ReflectionAccessor.GetType("Codeer.TestAssistant.GeneratorToolKit.IWPFControlIdentifyCodeGenerator");
            var identifyGenerateMethod = identifyGeneratorType.GetMethod("GenerateIdentifyCode");
            var identifyPriority = identifyGeneratorType.GetProperty("Priority");

            foreach (var type in EnumAllTypes())
            {
                try
                {
                    if (identifyGeneratorType.IsAssignableFrom(type) && type.IsClass)
                    {
                        var generator = Activator.CreateInstance(type);
                        _identifyGenerators.Add(new IdentifyGeneratorHolder(
                            (int)identifyPriority.GetValue(generator, new object[0]),
                            (target, tree) => identifyGenerateMethod.Invoke(generator, new object[] { target, tree })));
                    }
                }
                catch { }
            }

            //プライオリティが高い順にソート
            _identifyGenerators.Sort((l, r) => r.Priority - l.Priority);
        }

        private IEnumerable<Type> EnumAllTypes()
        {
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in GetTypesFromAssembly(asm))
                {
                    yield return type;
                }
            }
        }

        private IEnumerable<Type> GetTypesFromAssembly(Assembly asm)
        {
            try
            {
                return asm.GetTypes();
            }
            catch
            {
                return new Type[0];
            }
        }

        public string Generate(DependencyObject obj, ICollection<DependencyObject> tree, List<string> usings)
        {
            foreach (var i in _identifyGenerators)
            {
                var code = i.GenerateIdentifyCode(obj, tree);
                if (code != null)
                {
                    var text = (string)code.GetType().GetProperty("IdentifyCode").GetValue(code, new object[0]);
                    if (!string.IsNullOrEmpty(text) && (text[0] != '.')) text = "." + text;

                    foreach (var x in (string[])code.GetType().GetProperty("AddUsings").GetValue(code, new object[0]))
                    {
                        if (!usings.Contains(x)) usings.Add(x);
                    }
                    return text;
                }
            }
            return string.Empty;
        }
    }
}

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;

namespace RM.Friendly.WPFStandardControls.Generator.CreateDriver
{
    internal class DriverElementNameGeneratorAdaptor
    {
        private delegate string GenerateName(object target);

        private class NameGeneratorHolder
        {
            public int Priority { get; set; }
            public GenerateName GenerateName { get; set; }
        }

        private readonly List<NameGeneratorHolder> _nameGenerators = new List<NameGeneratorHolder>();
        private readonly CodeDomProvider _dom;

        public DriverElementNameGeneratorAdaptor(CodeDomProvider dom)
        {
            _dom = dom;

            Initialize();
        }

        private void Initialize()
        {
            //名前カスタムクラスを集める
            var nameGeneratorType = ReflectionAccessor.GetType("Codeer.TestAssistant.GeneratorToolKit.IDriverElementNameGenerator");
            var nameGenerateMethod = nameGeneratorType.GetMethod("GenerateName");
            var namePriority = nameGeneratorType.GetProperty("Priority");

            foreach (var type in EnumAllTypes())
            {
                try
                {
                    if (nameGeneratorType.IsAssignableFrom(type) && type.IsClass)
                    {
                        var generator = Activator.CreateInstance(type);
                        _nameGenerators.Add(new NameGeneratorHolder
                        {
                            Priority = (int)namePriority.GetValue(generator, new object[0]),
                            GenerateName = c => (string)nameGenerateMethod.Invoke(generator, new[] { c })
                        });
                    }
                }
                catch { }
            }

            //プライオリティが高い順にソート
            _nameGenerators.Sort((l, r) => r.Priority - l.Priority);
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

        public string MakeDriverPropName(object control, string secondCandidate, List<string> names)
        {
            var customName = MakeName(control);
            if (!string.IsNullOrEmpty(customName)) return ToUniqueName(customName, names);

            secondCandidate = secondCandidate.Replace(" ", "");
            foreach (var err in "(){}<>+-=*/%!\"#$&'^~\\|@;:,.?")
            {
                secondCandidate = secondCandidate.Replace(err.ToString(), string.Empty);
            }

            var name = (!string.IsNullOrEmpty(secondCandidate) && _dom.IsValidIdentifier(secondCandidate)) ?
                        secondCandidate :
                        control.GetType().Name;
            return ToUniqueName(name, names);
        }

        private static string ToUniqueName(string nameSrc, List<string> names)
        {
            var name = nameSrc;
            for (int i = 0;; i++)
            {
                if (!names.Contains(name))
                {
                    names.Add(name);
                    return name;
                }
                name = nameSrc + i;
            }
        }

        private string MakeName(object obj)
        {
            foreach (var e in _nameGenerators)
            {
                var name = e.GenerateName(obj);
                if (!string.IsNullOrEmpty(name)) return name;
            }
            return string.Empty;
        }
    }
}

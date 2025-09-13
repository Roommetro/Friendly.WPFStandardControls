using Codeer.Friendly.Windows;
using MessagePack;
using MessagePack.Formatters;
using MessagePack.Resolvers;
using System;
using System.Reflection;

namespace Test
{
    public class IntPtrFormatter : IMessagePackFormatter<IntPtr>
    {
        public void Serialize(ref MessagePackWriter writer, IntPtr value, MessagePackSerializerOptions options)
            => writer.Write(value.ToInt64());

        public IntPtr Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
            => new IntPtr(reader.ReadInt64());
    }

    public class CustomSerializer : ICustomSerializer
    {
        MessagePackSerializerOptions customOptions = MessagePackSerializerOptions
            .Standard
            .WithResolver(
                CompositeResolver.Create(
                    new IMessagePackFormatter[] { new IntPtrFormatter() },
                    new IFormatterResolver[] { TypelessContractlessStandardResolver.Instance, DynamicObjectResolverAllowPrivate.Instance, DynamicContractlessObjectResolverAllowPrivate.Instance }
                )
            );

        public object Deserialize(byte[] bin)
            => MessagePackSerializer.Typeless.Deserialize(bin, customOptions);

        public Assembly[] GetRequiredAssemblies() => [GetType().Assembly, typeof(MessagePackSerializer).Assembly];

        public byte[] Serialize(object obj)
            => MessagePackSerializer.Typeless.Serialize(obj, customOptions);
    }

    [SetUpFixture]
    public class Initializer
    {
        [OneTimeSetUp]
        public void OneTimeSetUp() => WindowsAppFriend.SetCustomSerializer<CustomSerializer>();
    }
}
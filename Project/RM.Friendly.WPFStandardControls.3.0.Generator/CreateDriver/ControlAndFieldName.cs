namespace RM.Friendly.WPFStandardControls.Generator.CreateDriver
{
    internal class ControlAndFieldName<T>
    {
        public T Control { get; }
        public string Name { get; }

        public ControlAndFieldName(T control, string name)
        {
            Control = control;
            Name = name;
        }
    }
}

using System;

namespace ContainerLibrary.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ExportAttribute: Attribute
    {
        public ExportAttribute()
        {

        }

        public Type TypeToResolve { get; private set; }

        public ExportAttribute(Type typeToResolve)
        {
            TypeToResolve = typeToResolve;
        }
    }
}

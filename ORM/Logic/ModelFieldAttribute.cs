using System;

namespace ORM
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class FieldAttribute : Attribute
    {
        public string FieldName { get; set; }

        public FieldAttribute(string fieldName)
        {
            FieldName = fieldName;
        }
    }
}

using System;
using System.Collections.Generic;

namespace Cask.Attributes
{
    public class RequiredComponentsAttribute : Attribute
    {
        public List<Type> Types { get; private set; }
        
        public RequiredComponentsAttribute(params Type[] requiredComponentTypes)
        {
            Types = new List<Type>(requiredComponentTypes);
        }
        
        public RequiredComponentsAttribute(ICollection<Type> requiredComponentTypes)
        {
            Types = new List<Type>(requiredComponentTypes);
        }
    }
}
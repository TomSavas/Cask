using System;
using System.Collections.Generic;

namespace Core.Components
{
    public class ComponentDependencyResolver : IComponentDependencyResolver
    {
        //private IComponentFactory _componentFactory;
        
        //public ComponentDependencyResolver(IComponentFactory componentFactory)
        //{
        //    _componentFactory = componentFactory;
        //}
        
        public void ResolveDependencies(IComponent component, IDictionary<Type, IComponent> existingComponents)
        {
            foreach (var requiredComponent in component.RequiredComponents)
            {
                if (existingComponents.ContainsKey(requiredComponent))
                    component.Dependencies.Add(requiredComponent, existingComponents[requiredComponent]);
                else
                    //Temporary solution, should use a factory. Most likely...
                    component.Dependencies.Add(requiredComponent, (IComponent)Activator.CreateInstance(requiredComponent));
            }
        }

        public void RemoveDependencies(IComponent component, IDictionary<Type, IComponent> existingComponents)
        {
            //handle
        }
    }
}
using System;
using System.Collections.Generic;
using Cask.Components;
using Cask.Managers;

namespace Cask.Extensions
{
    /// <summary>
    /// This is essentially made for more friendly syntax. Instead of writting gameObj.Components.Add(typeof(Transform), new Transform());
    /// it makes syntax like this possible: gameObj.Components.Add<Transform>(new Transform());
    /// </summary>
    public static class IDictionaryExtensions
    {
        public static void Add<T>(this IDictionary<Type, IComponent> dictionary, T value) where T : class, IComponent
        {
            dictionary.Add(typeof(T), value);
        }
        
        public static void Add(this IDictionary<Type, IComponent> dictionary, Type type, IComponent component)
        {
            dictionary.Add(type, component);
        } 
        
        public static T Get<T>(this IDictionary<Type, IComponent> dictionary) where T : class, IComponent 
        {
            return dictionary[typeof(T)] as T;
        }
        
        public static T Get<T>(this IReadOnlyDictionary<Type, IComponent> dictionary) where T : class, IComponent 
        {
            return dictionary[typeof(T)] as T;
        }
        
        public static void Add<T>(this IDictionary<Type, IManager> dictionary, T value) where T : class, IManager
        {
            dictionary.Add(typeof(T), value);
        }
        
        public static void Add(this IDictionary<Type, IManager> dictionary, Type type, IManager manager)
        {
            dictionary.Add(type, manager);
        } 
        
        public static T Get<T>(this IDictionary<Type, IManager> dictionary) where T : class, IManager 
        {
            return dictionary[typeof(T)] as T;
        }
    }
}
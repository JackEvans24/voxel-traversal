using UnityEngine;

namespace TraversalDemo.Models
{
    public static class GameObjectExtensions
    {
        public static T AddChildComponent<T>(this GameObject gameObject) where T : Component
        {
            var childGameObject = new GameObject(typeof(T).Name);
            childGameObject.transform.parent = gameObject.transform;

            var component = childGameObject.AddComponent<T>();
            return component;
        }
    }
}
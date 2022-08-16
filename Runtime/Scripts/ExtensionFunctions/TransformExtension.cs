//Resharper disable all

using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace SUP.GameUtils.ExtensionFunctions {
    public static class TransformExtension {
        #region Immediate children functions

        /// <summary>
        /// Finds immediate children & returns as array
        /// </summary>
        /// <param name="transform">Transform of parent gameobject</param>
        /// <returns>Array of transforms</returns>
        public static Transform[] GetChildren(this Transform transform) {
            if (transform.childCount == 0) return new Transform[0];

            var transformChildren = new List<Transform>();
            for (int i = 0; i < transform.childCount; i++) {
                transformChildren.Add(transform.GetChild(i));
            }

            return transformChildren.ToArray();
        }

        /// <summary>
        /// Finds immediate children by name & returns as array
        /// </summary>
        /// <param name="transform">Tranform of parent gameobject</param>
        /// <param name="name">Name to find</param>
        /// <returns>Array of transforms</returns>
        public static Transform[] GetChildrenByName(this Transform transform, string name) {
            var transformChildren = GetChildren(transform).ToList();
            transformChildren.RemoveAll(childTransform => !childTransform.name.Equals(name));
            return transformChildren.ToArray();
        }

        /// <summary>
        /// Finds immediate children by layer name & returns as array
        /// </summary>
        /// <param name="transform">Tranform of parent gameobject</param>
        /// <param name="layerName">Layer name</param>
        /// <returns>Array of transforms</returns>
        public static Transform[] GetChildrenByLayer(this Transform transform, string layerName) {
            return transform.GetChildrenByLayer(LayerMask.NameToLayer(layerName));
        }

        /// <summary>
        /// Finds immediate children by layer index & returns as array
        /// </summary>
        /// <param name="transform">Tranform of parent gameobject</param>
        /// <param name="layerIndex">Layer index</param>
        /// <returns>Array of transforms</returns>
        public static Transform[] GetChildrenByLayer(this Transform transform, int layerIndex) {
            var transformChildren = GetChildren(transform).ToList();
            transformChildren.RemoveAll(childTransform => childTransform.gameObject.layer != layerIndex);
            return transformChildren.ToArray();
        }

        /// <summary>
        /// Finds immediate children by tag name & returns as array
        /// </summary>
        /// <param name="transform">Tranform of parent gameobject</param>
        /// <param name="tagName">Tag name</param>
        /// <returns>Array of transforms</returns>
        public static Transform[] GetChildrenByTag(this Transform transform, string tagName) {
            var transformChildren = GetChildren(transform).ToList();
            transformChildren.RemoveAll(childTransform => !childTransform.gameObject.CompareTag(tagName));
            return transformChildren.ToArray();
        }

        /// <summary>
        /// Finds immediate children component & returns as array
        /// </summary>
        /// <param name="transform">Parent transform</param>
        /// <typeparam name="T">Component</typeparam>
        /// <returns></returns>
        public static T[] GetChildrenComponent<T>(this Transform transform) where T : Component =>
            FilterChildrenWithComponent<T>(transform.GetChildren());

        /// <summary>
        /// Finds immediate children component by name & returns as array
        /// </summary>
        /// <param name="transform">Parent transform</param>
        /// <param name="name">Name to find</param>
        /// <typeparam name="T">Component</typeparam>
        /// <returns></returns>
        public static T[] GetChildrenComponentByName<T>(this Transform transform, string name) where T : Component =>
            FilterChildrenWithComponent<T>(transform.GetChildrenByName(name));

        /// <summary>
        /// Finds immediate children component by layer name & returns as array
        /// </summary>
        /// <param name="transform">Parent transform</param>
        /// <param name="layerName">Layer name</param>
        /// <typeparam name="T">Component</typeparam>
        /// <returns></returns>
        public static T[] GetChildrenComponentByLayer<T>(this Transform transform, string layerName) where T : Component =>
            FilterChildrenWithComponent<T>(transform.GetChildrenByLayer(layerName));

        /// <summary>
        /// Finds immediate children component by layer index & returns as array
        /// </summary>
        /// <param name="transform">Parent transform</param>
        /// <param name="layerIndex">Layer index</param>
        /// <typeparam name="T">Component</typeparam>
        /// <returns></returns>
        public static T[] GetChildrenComponentByLayer<T>(this Transform transform, int layerIndex) where T : Component =>
            FilterChildrenWithComponent<T>(transform.GetChildrenByLayer(layerIndex));

        /// <summary>
        /// Finds immediate children component by tag name & returns as array
        /// </summary>
        /// <param name="transform">Parent transform</param>
        /// <param name="tagName">Tag name</param>
        /// <typeparam name="T">Component</typeparam>
        /// <returns></returns>
        public static T[] GetChildrenComponentByTag<T>(this Transform transform, string tagName) where T : Component =>
            FilterChildrenWithComponent<T>(transform.GetChildrenByTag(tagName));

        #endregion

        #region Deep children functions

        /// <summary>
        /// Finds deep children & returns as array
        /// </summary>
        /// <param name="transform">Tranform of parent gameobject</param>
        /// <returns>Array of transforms</returns>
        public static Transform[] GetAllDeepChildren(this Transform transform) {
            if (transform.childCount == 0) new List<Transform>(); //Return empty list 

            var transformDeepChildren = FindAllDeepChildren(transform).ToList();
            transformDeepChildren.Remove(transform); //Remove source transform from list
            return transformDeepChildren.ToArray();
        }

        /// <summary>
        /// Finds deep children by name & returns as array
        /// </summary>
        /// <param name="transform">Tranform of parent gameobject</param>
        /// <param name="name">Name of find</param>
        /// <returns>Array of transforms</returns>
        public static Transform[] GetAllDeepChildrenByName(this Transform transform, string name) {
            var transformDeepChildren = FindAllDeepChildren(transform).ToList();
            transformDeepChildren.RemoveAll(childTransform => !childTransform.name.Equals(name));
            return transformDeepChildren.ToArray();
        }

        /// <summary>
        /// Finds deep children by layer name & returns as array
        /// </summary>
        /// <param name="transform">Tranform of parent gameobject</param>
        /// <param name="layerName">Layer name</param>
        /// <returns>Array of transforms</returns>
        public static Transform[] GetAllDeepChildrenByLayer(this Transform transform, string layerName) {
            return transform.GetAllDeepChildrenByLayer(LayerMask.NameToLayer(layerName));
        }

        /// <summary>
        /// Finds deep children by layer index & returns as array
        /// </summary>
        /// <param name="transform">Tranform of parent gameobject</param>
        /// <param name="layerIndex">Layer index</param>
        /// <returns>Array of transforms</returns>
        public static Transform[] GetAllDeepChildrenByLayer(this Transform transform, int layerIndex) {
            var transformDeepChildren = FindAllDeepChildren(transform).ToList();
            transformDeepChildren.RemoveAll(childTransform => childTransform.gameObject.layer != layerIndex);
            return transformDeepChildren.ToArray();
        }

        /// <summary>
        /// Finds deep children by tag name & returns as array
        /// </summary>
        /// <param name="transform">Tranform of parent gameobject</param>
        /// <param name="tagName">Tag name</param>
        /// <returns>Array of transforms</returns>
        public static Transform[] GetAllDeepChildrenByTag(this Transform transform, string tagName) {
            var transformDeepChildren = FindAllDeepChildren(transform).ToList();
            transformDeepChildren.RemoveAll(childTransform => !childTransform.gameObject.CompareTag(tagName));
            return transformDeepChildren.ToArray();
        }

        /// <summary>
        /// Finds all deep children component & returns as array
        /// </summary>
        /// <param name="transform">Parent transform</param>
        /// <typeparam name="T">Component</typeparam>
        /// <returns></returns>
        public static T[] GetAllDeepChildrenComponent<T>(this Transform transform) where T : Component =>
            FilterChildrenWithComponent<T>(transform.GetAllDeepChildren());

        /// <summary>
        /// Finds all deep children component by name & returns as array
        /// </summary>
        /// <param name="transform">Parent transform</param>
        /// <param name="name">Name to find</param>
        /// <typeparam name="T">Component</typeparam>
        /// <returns></returns>
        public static T[] GetAllDeepChildrenComponentByName<T>(this Transform transform, string name) where T : Component =>
            FilterChildrenWithComponent<T>(transform.GetAllDeepChildrenByName(name));

        /// <summary>
        /// Finds all deep children component by layer name & returns as array
        /// </summary>
        /// <param name="transform">Parent transform</param>
        /// <param name="layerName">Layer name</param>
        /// <typeparam name="T">Component</typeparam>
        /// <returns></returns>
        public static T[] GetAllDeepChildrenComponentByLayer<T>(this Transform transform, string layerName) where T : Component =>
            FilterChildrenWithComponent<T>(transform.GetAllDeepChildrenByLayer(layerName));

        /// <summary>
        /// Finds all deep children component by layer index & returns as array
        /// </summary>
        /// <param name="transform">Parent transform</param>
        /// <param name="layerIndex">Layer index</param>
        /// <typeparam name="T">Component</typeparam>
        /// <returns></returns>
        public static T[] GetAllDeepChildrenComponentByLayer<T>(this Transform transform, int layerIndex) where T : Component =>
            FilterChildrenWithComponent<T>(transform.GetAllDeepChildrenByLayer(layerIndex));

        /// <summary>
        /// Finds all deep children component by tag name & returns as array
        /// </summary>
        /// <param name="transform">Parent transform</param>
        /// <param name="tagName">Tag name</param>
        /// <typeparam name="T">Component</typeparam>
        /// <returns></returns>
        public static T[] GetAllDeepChildrenComponentByTag<T>(this Transform transform, string tagName) where T : Component =>
            FilterChildrenWithComponent<T>(transform.GetAllDeepChildrenByTag(tagName));

        #endregion

        #region Helper functions

        // Private helper function to find children with T components
        private static T[] FilterChildrenWithComponent<T>(Transform[] transforms) where T : Component {
            var arrayOfComponents = new List<T>();
            foreach (var child in transforms) {
                if (child.TryGetComponent(typeof(T), out Component component)) arrayOfComponents.Add((T) component);
            }

            return arrayOfComponents.ToArray();
        }

        private static Transform[] FindAllDeepChildren(Transform transform) {
            var childTransforms = new List<Transform>() {transform};
            if (transform.childCount == 0) return childTransforms.ToArray();

            transform.GetChildren().ToList().ForEach(child => childTransforms.AddRange(FindAllDeepChildren(child)));
            return childTransforms.ToArray();
        }

        #endregion
    }
}
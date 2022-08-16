//Resharper disable all

using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using JetBrains.Annotations;
using Random = System.Random;

namespace SUP.GameUtils.ExtensionFunctions {
    public static class CollectionsExtension {
        #region GameObject functions

        public static void EnableAll(this IEnumerable<GameObject> list) => ChangeGameObjectsState(list, true);
        public static void DisableAll(this IEnumerable<GameObject> list) => ChangeGameObjectsState(list, false);
        public static List<T> Clone<T>(this IEnumerable<T> sourceList) => sourceList.ToList();
        public static void RemoveAllNullGameObjects(this List<GameObject> list) => list.RemoveAll(gameObj => gameObj == null);

        #endregion

        #region Collider Operations

        public static void EnableAllColliders(this IEnumerable<GameObject> enumerable) => ChangeCollidersState(enumerable.ToList(), true);
        public static void DisableAllColliders(this IEnumerable<GameObject> enumerable) => ChangeCollidersState(enumerable.ToList(), false);

        #endregion

        #region Shuffle Operations

        public static void Shuffle<T>(this IList<T> list) {
            var n = list.Count;
            while (n > 1) {
                var k = (UnityEngine.Random.Range(0, n) % n);
                n--;
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        [UsedImplicitly]
        public static Dictionary<TKey, TValue> Shuffle<TKey, TValue>(this Dictionary<TKey, TValue> source) {
            var r = new Random();
            return source.OrderBy(_ => r.Next()).ToDictionary(item => item.Key, item => item.Value);
        }

        #endregion

        #region Random Element

        public static T GetRandomElement<T>(this IEnumerable<T> iEnumerable) {
            var randomIndex = UnityEngine.Random.Range(0, iEnumerable.Count());
            return iEnumerable.ElementAt(randomIndex);
        }

        #endregion

        #region Private helper functions

        private static void ChangeGameObjectsState(IEnumerable<GameObject> enumerable, bool value) {
            enumerable.ToList().ForEach(gameObj => gameObj.SetActive(value));
        }

        private static void ChangeCollidersState(IEnumerable<GameObject> enumerable, bool value) {
            foreach (var gameObj in enumerable) {
                try {
                    gameObj.GetComponent<Collider2D>().enabled = value;
                } catch (Exception e) {
                    Debug.LogError($"CustomExtensions: GameObject = {gameObj.name}\n{e}");
                }
            }
        }

        #endregion
    }
}
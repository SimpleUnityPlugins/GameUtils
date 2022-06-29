//Resharper disable all
using UnityEngine;

public class PersistentSingletonMonoBehaviour<T> : MonoBehaviour where T : PersistentSingletonMonoBehaviour<T> {
    private static T _instance;

    public static T Instance {
        get {
            if (_instance != null) return _instance;
            Debug.Log($"Creating singleton instance of {typeof(T)}");
            _instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
            DontDestroyOnLoad(_instance.gameObject);
            return _instance;
        }
    }

    protected virtual void Awake() {
        if (_instance == null) {
            _instance = (T) this;
            DontDestroyOnLoad(_instance.gameObject);
        } else {
            Destroy(gameObject);
        }
    }
}
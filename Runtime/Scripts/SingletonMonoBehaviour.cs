using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T> {
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public static T Instance { get; private set; }

    protected virtual void Awake() {
        Instance = (T) this;
    }

    protected virtual void OnDestroy() {
        Instance = null;
    }
}
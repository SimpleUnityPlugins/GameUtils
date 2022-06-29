# Game Utils
Collection of game utility classes and functions for Unity.

## Installation
Refer this link for installation guide:ls
https://docs.unity3d.com/Manual/upm-ui-giturl.html

## Features
* PersistentSingletonMonoBehaviour
* SingletonMonoBehaviour

### PersistentSingletonMonoBehaviour
* Using persistent monobehaviour creates a single instance of monobehaviour throughout the game.
* Creates a new gameobject automatically if it doesn't exists.
* Deletes any new gameobject instance, if created.
```csharp
public class PlayerController : PersistentSingletonMonoBehaviour<PlayerController> {
    protected override void Awake() {
        base.Awake(); 
        
        // YOUR CODE HERE
    }
}
```

### SingletonMonoBehaviour
* Creates static instance variable of inheriting monobehaviour.
* Does not delete any new instance of gameobject, if created.
```csharp
public class PlayerController : SingletonMonoBehaviour<PlayerController> {
    protected override void Awake() {
        base.Awake();
        
        // YOUR CODE HERE
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        
        // YOUR CODE HERE
    }
}
```



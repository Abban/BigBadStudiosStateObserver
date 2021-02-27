# Game Farts Unity State Broker

This is a small library for transactional state in Unity.

## How it works
Each state value has an associated event broker and an action that an be subscribed to. When a value is changed it sets itself as dirty. Then when a state transaction is finished the state broker notifies all dirty subscribers that state has changed. Subscribers will only be notified once each, even if they are listening to multiple changed values.

## Installation
You can install this in Unity as a Package by going to `Window > Package Manager` And then hitting the `+` and selecting `Add Package from git URL`. 

## Usage
1. Create a class to hold your state variables. Each variable should implement the `IObservableStateProperty` interface.
2. In your initialisation instantiate a `StateBroker`. 
3. Create your state variables with a default value and pass them into the broker.
4. Instantiate your state class and inject your variables.

    ```c#
    StateBroker = new StateBroker();
    
    var stars = new ObservableStateProperty<int>(10);
    var coins = new ObservableStateProperty<int>(10);
   
    StateBroker.AddProperty(stars);
    StateBroker.AddProperty(coins);
        
    GameState = new GameState(stars, coins);
    ```

5. You can subscribe and unsubscribe to the variable like this:
    ```c#
    GameState.Stars.Subscribe(ObserverCallback);
    GameState.Stars.Unsubscribe(ObserverCallback);
    ```
6. To start a state transaction you tell the `StateBroker`.
   ```c#
   StateBroker.StartTransaction();
   ```
7. To fire a notification after values have changed you can call the `Commit()` method on the `StateBroker`.
    ```c#
    StateBroker.Commit();
    ```
8. If you want to change a state property outside a transaction you can manually call `Fire()` on them.
   
**Tip:** Use a `ScriptableObject` as your GameState and you can pass it around easier.
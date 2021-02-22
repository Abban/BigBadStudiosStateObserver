# Game Farts Unity State Broker

This is a small library for observing state in Unity.

## How it works
Each state value has an associated event broker and an action that an be subscribed to. When a value is changed it tells the state broker. Then when a state transaction is finished the state broker notifies all subscribers that state has changed. Subscribers will only be notified once each, even if they are listening to multiple changed values.

## Installation
You can install this in Unity as a Package by going to `Window > Package Manager` And then hitting the `+` and selecting `Add Package from git URL`. 

## Usage
1. Create an class to hold your state variables. Each variable should implement the `IObservableStateProperty` interface.
2. In your initialisation instantiate an `StateBroker`. 
3. Create your state variables and inject the broker ito each. Second parameter is a default value so you can load this from save data or something if you need to.
4. Instantiate your state class and inject your variables.

    ```c#
    StateBroker = new StateBroker();
    
    var stars = new ObservableStateProperty<int>(StateBroker, 10);
    var coins = new ObservableStateProperty<int>(StateBroker, 10);
        
    GameState = new GameState(stars, coins);
    ```

5. You can then subscribe to the variable's actions where you need.
    ```c#
    GameState.Stars.Action += ObserverCallback;
    ```
6. To start a state transaction you tell the `StateBroker`.
   ```c#
   StateBroker.StartTransaction();
   ```
7. To fire a notification after values have changed you can call the `Commit()` method on the `StateBroker`.
    ```c#
    StateBroker.Commit();
    ```
8. If you change a state value without starting a transaction it will trigger an immediate commit.
   
**Tip:** Use a `ScriptableObject` as your GameState and you can pass it around easier.
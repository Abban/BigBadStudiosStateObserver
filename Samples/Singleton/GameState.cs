using GF.Library.StateBroker;

namespace StateBrokerSingleton
{
    public class GameState
    {
        public IObservableStateProperty<int> Stars { get; }
        public IObservableStateProperty<int> Coins { get; }

        public GameState(
            IObservableStateProperty<int> stars,
            IObservableStateProperty<int> coins)
        {
            Stars = stars;
            Coins = coins;
        }
    }
}
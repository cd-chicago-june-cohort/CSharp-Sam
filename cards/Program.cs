using System;

namespace cards

{
    class Program
    {
        static void Main(string[] args)
        {
            Deck newDeck = new Deck();
            newDeck.showList();
            newDeck.Shuffle();
            newDeck.showList();
            newDeck.Unique();
            Player Sam = new Player("Sam");
            Sam.Draw(newDeck);
            Sam.Draw(newDeck);
            Sam.Draw(newDeck);
            Sam.showHand();
            Sam.Discard(2);
            Sam.showHand();
            newDeck.showList();
        }
    }
}

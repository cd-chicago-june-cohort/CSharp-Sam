using System;
using System.Collections.Generic;

namespace cards {
    public class Player{

        public string name;
        List<Card> hand = new List<Card>();

        public Player(string n = ""){
            name = n;
        }

        public Card Draw(Deck deck) {
            Card newCard = new Card();
            newCard = deck.Deal();
            hand.Add(newCard);
            return newCard;
        }

        public Card Discard(int index){
            if (hand[index] != null){
                Card temp = new Card();
                temp = hand[index];
                hand.RemoveAt(index);
                return temp;
            } else {
                return null;
            }
        }

        public void showHand(){
            foreach(var card in hand){
                Console.WriteLine($"This card in {name}'s hand is the {card.stringVal} of {card.suit}");
            }
        }

    }
}
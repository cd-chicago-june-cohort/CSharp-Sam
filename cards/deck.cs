using System;
using System.Collections.Generic;

namespace cards {
    public class Deck{
        public List<Card> myCards = new List<Card>();

        public Deck() {
            string[] suits = {"spades", "hearts", "clubs", "diamonds"};
            for (var s = 0; s < 4; s += 1) {
                for (int v = 1; v <= 13; v += 1){
                    Card newCard = new Card();
                    newCard.suit = suits[s];
                    newCard.value = v;
                    if (v > 1 && v < 11) {
                        newCard.stringVal = v.ToString();
                    } else if (v == 1) {
                        newCard.stringVal = "Ace";
                    } else if (v == 11) {
                        newCard.stringVal = "Jack";
                    } else if (v == 12) {
                        newCard.stringVal = "Queen";
                    } else {
                        newCard.stringVal = "King";
                    }
                    myCards.Add(newCard);    
                }
            }
        }

        public void showList(){
            foreach(var card in myCards) {
                Console.WriteLine($"This card is the {card.stringVal} of {card.suit}");
            }
            Console.WriteLine(myCards.Count);
        }

        public Card Deal(){
            Card tempCard = myCards[0];
            myCards.RemoveAt(0);
            Console.WriteLine($"The dealt card is the {tempCard.stringVal} of {tempCard.suit}");
            return tempCard;
        }

        public void Reset(){
            myCards.Clear();
            string[] suits = {"spades", "hearts", "clubs", "diamonds"};
            for (var s = 0; s < 4; s += 1) {
                for (int v = 1; v <= 13; v += 1){
                    Card newCard = new Card();
                    newCard.suit = suits[s];
                    newCard.value = v;
                    if (v > 1 && v < 11) {
                        newCard.stringVal = v.ToString();
                    } else if (v == 1) {
                        newCard.stringVal = "Ace";
                    } else if (v == 11) {
                        newCard.stringVal = "Jack";
                    } else if (v == 12) {
                        newCard.stringVal = "Queen";
                    } else {
                        newCard.stringVal = "King";
                    }
                    myCards.Add(newCard);    
                }
            }
        }

        public void Shuffle(){
            List<int> order = new List<int>();
            List<Card> tempList = new List<Card>();
            for (int i = 0; i < myCards.Count; i +=1){
                order.Add(i);
            }
            Random rand = new Random();
            for (int j = 0; j < myCards.Count; j +=1){
                int place = rand.Next(0,order.Count);
                // Console.WriteLine(order.Count);
                // Console.WriteLine(place);
                tempList.Add(myCards[order[place]]);
                order.RemoveAt(place);
            }
            myCards.Clear();
            myCards = tempList;
        }

        public void Unique(){
            bool unique = true;
            for (int j = 0; j < myCards.Count; j +=1) {
                for (int i = 0; i < myCards.Count; i += 1){
                    if (myCards[j].value == myCards[i].value && myCards[j].suit == myCards[i].suit && i != j){
                        unique = false;
                    }
                }
            }
            Console.WriteLine(unique);
        }

    }

}
using System; 

namespace cards {

    public class Card{

        public string stringVal;
        public string suit;
        public int value;
        public Card(string str = "", string suitstr="", int intval=0) {
            stringVal = str;
            suit = suitstr;
            value = intval;
        }
        
    }

}
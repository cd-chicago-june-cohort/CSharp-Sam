using System;
using System.Collections.Generic;

namespace AreWeHuman
{
    public class Ninja : Human{
        public Ninja(string name) : base(name){
            dexterity = 175;
        }

        public void Steal(object sucker) {
            Attack(sucker);
            health += 10;
        }

        public void GetAway(){
            health -= 15;
        }

        
    }
}
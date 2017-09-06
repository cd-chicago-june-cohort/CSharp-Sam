using System;
using System.Collections.Generic;

namespace AreWeHuman
{
    public class Human{
        public string name;
        public int strength;
        public int intelligence;
        public int dexterity;
        public int health;

        public Human(string newName = ""){
            name = newName;
            strength = 3;
            intelligence = 3;
            dexterity = 3;
            health = 100;
        }

        public Human(string newName, int s, int i, int d, int h){
            name = newName;
            strength = s;
            intelligence = i;
            dexterity = d;
            health = h;
        }

        public void Attack(object opponent){
            if(opponent is Human) {
                Human temp = opponent as Human;
                int damage = 5 * strength;
                temp.health -= damage;
                opponent = temp;
            }
        }

    }
}
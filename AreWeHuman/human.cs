using System;
using System.Collections.Generic;

namespace AreWeHuman
{
    public class Human{
        public string name {get; set;}
        public int strength {get; set;}
        public int intelligence {get; set;}
        public int dexterity {get; set;}
        public int health {get; set;}

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
            } else {
                Console.WriteLine("Failed Attack!");
            }
        }

        public void Show(){
            Console.WriteLine($"Name: {name} Strength: {strength} Intelligence: {intelligence} Dexterity: {dexterity} Health: {health}");
        }

    }
}
using System;
using System.Collections.Generic;

namespace AreWeHuman
{
    public class Wizard : Human {
        public Wizard(string name) : base(name){
            health = 50;
            intelligence = 25;
        }

        public void Heal(){
            int healingPowers = intelligence * 10;
            health += healingPowers;
        }

        Random fireStrength = new Random();
        public void Fireball(object obj){
            if(obj is Human) {
                Human opponent = obj as Human;
                int firePower = fireStrength.Next(20,50);
                opponent.health -= firePower;
                obj = opponent;
            } else {
                Console.WriteLine("Failed Attack!");
            }
        }
    }
}
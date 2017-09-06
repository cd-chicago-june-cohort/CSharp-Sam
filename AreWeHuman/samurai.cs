using System;
using System.Collections.Generic;

namespace AreWeHuman
{
    public class Samurai : Human{

        private static List<WeakReference> instances = new List<WeakReference>();
        public Samurai(string name) : base(name){
            health = 200;
            instances.Add(new WeakReference(this));
        }

        public void DeathBlow(object obj){
            if(obj is Human) {
                Human opponent = obj as Human;
                if (opponent.health < 50) {
                    opponent.health = 0;
                }
                obj = opponent;
            } else {
                Console.WriteLine("Failed Attack!");
            }
        }

        public void Meditate(){
            health = 200;
        }

        public void HowMany(){
            Console.WriteLine("Samurais: " + instances.Count);
        }



    }
}
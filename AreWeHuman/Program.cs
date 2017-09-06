using System;
using System.Collections.Generic;

namespace AreWeHuman
{
    class Program
    {
        static void Main(string[] args)
        {
            Human Harry = new Human("Harry");
            Harry.Show();
            Wizard Sam = new Wizard("Sam");
            Harry.Attack(Sam);
            Sam.Show();
            Sam.Heal();
            Sam.Show();
            Ninja Ric = new Ninja("Erick");
            Ric.Show();
            Console.WriteLine("Stole from Sam");
            Ric.Steal(Sam);
            Sam.Fireball(Ric);
            Sam.Fireball(Ric);
            Ric.Show();
            Sam.Show();
            Console.WriteLine("Ran away from Sam");
            Ric.GetAway();
            Ric.Show();
            Samurai Diana = new Samurai("Diana");
            Samurai Maria = new Samurai("Maria");
            Maria.Show();
            Diana.HowMany();
            Diana.DeathBlow(Ric);
            Console.WriteLine("Testing Death Blow");
            Ric.Show();
            Sam.Fireball(Maria);
            Console.WriteLine("Testing Meditate");
            Maria.Show();
            Maria.Meditate();
            Maria.Show();
            Samurai Gracie = new Samurai("Gracie");
            Gracie.HowMany();

        }
    }
}

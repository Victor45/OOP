using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4_POO
{
    public class Evenimente
    {
        private readonly Ecosistem _ecosistem;

        public Evenimente(Ecosistem ecosistem)
        {
            _ecosistem = ecosistem;
        }

        public void Furtuna()
        {
            //Console.WriteLine("Eveniment aleatoriu: Furtuna!");
            bool afected = false;
            foreach (var entity in _ecosistem.Entitati.ToList())
            {
                Random random = new Random();
                int chance = random.Next(0, 100);
                if (chance <= 30)
                {
                    afected = true;
                    if (entity.Nume == "Iarba")
                    {
                        entity.Energie -= 20;
                        Console.WriteLine($"Entitatea {entity.Nume}[{entity.ID}] a fost afectata de furtuna. Energie: {entity.Energie}");
                    }
                    else
                    {
                        entity.Energie -= 5;
                        Console.WriteLine($"Entitatea {entity.Nume}[{entity.ID}] a fost afectata de furtuna. Energie: {entity.Energie}");
                    }
                    if(entity.Energie <= 0)
                    {
                        Console.WriteLine($"Entitatea {entity.Nume}[{entity.ID}] a murit in urma furtunii.");
                        _ecosistem.RemoveEntitate(entity);
                    }
                }
            }
            if (!afected)
            {
                Console.WriteLine("Nici o entitate nu a fost afectata de furtuna");
            }
        }

        public void Seceta()
        {
            //Console.WriteLine("Eveniment aleatoriu: Seceta!");
            foreach (var entity in _ecosistem.Entitati)
            {
                if (entity.Nume == "Iarba")
                {
                    entity.Energie -= 70;
                }
                else
                {
                    entity.Energie -= 50;
                }
            }
            Console.WriteLine("Seceta! Toate plantele au pierdut 70% din energie in urma secetei.");
            Console.WriteLine("Seceta! Toate animalele au pierdut 50% din energie in urma secetei.");
            foreach (var entity in _ecosistem.Entitati.ToList())
            {
                if (entity.Energie <= 0)
                {
                    Console.WriteLine($"Entitatea {entity.Nume}[{entity.ID}] a murit in urma secetei. Energie: {entity.Energie}");
                    _ecosistem.RemoveEntitate(entity);
                }
            }
        }
    }
}

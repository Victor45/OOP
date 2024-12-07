using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4_POO
{
    public class Ecosistem
    {
        public int eID = 0;
        public int MapDimensionX;
        public int MapDimensionY;
        public List<EntitateEcosistem> Entitati;
        public Evenimente Event;

        public Ecosistem(int mapDimensionX, int mapDimensionY)
        {
            MapDimensionX = mapDimensionX;
            MapDimensionY = mapDimensionY;
            Entitati = new List<EntitateEcosistem>();
            Event = new Evenimente(this);
        }
        public void AddEntitate(EntitateEcosistem entitate)
        {
            Entitati.Add(entitate);
        }
        public void RemoveEntitate(EntitateEcosistem entitate)
        {
            Entitati.Remove(entitate);
        }

        public void TimePassing(int nr_cicluri)
        {
            Console.WriteLine($"\nCiclul 1: Niciun eveniment aleatoriu.");
            foreach (var entitate in Entitati.ToList())
            {
                entitate.Energie -= 10;
                entitate.Actioneaza();
            }
            FinalRaport();
            for (int i = 1; i < nr_cicluri; i++)
            {
                if(Entitati.Count == 0)
                {
                    Console.WriteLine("Toate entitatile din ecosistem au murit.");
                    break;
                }
                Random random = new Random();
                int evenimente = random.Next(0, 3);
                if (evenimente == 0)
                {
                    Console.WriteLine($"\nCiclul {i + 1}: Niciun eveniment aleatoriu.");
                    foreach (var entitate in Entitati.ToList())
                    {
                        entitate.Energie -= 10;
                        entitate.Actioneaza();
                    }
                }
                else if (evenimente == 1)
                {
                    Console.WriteLine($"\nCiclul {i + 1}: Furtuna!");
                    Event.Furtuna();
                    foreach (var entitate in Entitati.ToList())
                    {
                        entitate.Energie -= 10;
                        entitate.Actioneaza();
                    }
                }
                else if (evenimente == 2)
                {
                    Console.WriteLine($"\nCiclul {i + 1}: Seceta!");
                    Event.Seceta();
                    foreach (var entitate in Entitati.ToList())
                    {
                        entitate.Energie -= 10;
                        entitate.Actioneaza();
                    }
                }
                FinalRaport();
            }
        }

        public void FinalRaport()
        {
            Console.WriteLine("\n   Raport Ecosistemului dupa simulare");
            Console.WriteLine($"Numărul total de entitati ramase: {Entitati.Count}");
            foreach (var entitate in Entitati)
            {
                Console.WriteLine($"Nume: {entitate.Nume}[{entitate.ID}], pozitia ({entitate.Pozitie.X}, {entitate.Pozitie.Y}), energie: {entitate.Energie}.");
            }
        }
    }
}

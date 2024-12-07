using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4_POO
{
    public abstract class EntitateEcosistem : Interactiune
    {
        protected readonly Ecosistem _ecosistem;
        public string Nume { get; set; }
        public int Energie { get; set; }
        public (int X, int Y) Pozitie { get; set; }
        public double RataSupravietuire { get; set; }
        public int ID { get; set; }

        public EntitateEcosistem(string nume, int energie, (int X, int Y) pozitie, double rataSupravietuire, Ecosistem ecosistem)
        {
            Nume = nume;
            Energie = energie;
            Pozitie = pozitie;
            RataSupravietuire = rataSupravietuire;
            _ecosistem = ecosistem;
            ID = _ecosistem.eID++;
        }
        public abstract void Ataca(Animal prada);
        public abstract void Reproduce();
        public abstract void Actioneaza();
    }

    public class Planta : EntitateEcosistem
    {
        public Planta(string nume, int energie, (int X, int Y) pozitie,Ecosistem ecosistem) : base(nume, energie, pozitie, 0.8, ecosistem) { }
        public override void Actioneaza()
        {
            Creste();
            Reproduce();
        }
        public void Creste()
        {
            Energie += 20;
            Console.WriteLine($"{Nume}[{ID}] a crescut. Energie: {Energie}");
        }
        public override void Reproduce()
        {
            if (this.Energie >= 75)
            {
                this.Energie  = this.Energie / 2;
                Console.WriteLine($"{Nume} s-a reprodus si s-a generat o noua planta. Energie ramasa: {this.Energie}");
                _ecosistem.AddEntitate(new Planta("Iarba", 50, (2, 2), _ecosistem));
            }
        }
        public override void Ataca(Animal prada)
        {
            return;
        }
    }

    public abstract class Animal : EntitateEcosistem
    {
        public int Viteza { get; set; }
        public string TipHrana { get; set; }
        public Animal(
            string nume, 
            int energie, 
            (int X, int Y) pozitie, 
            double rataSupravietuire, 
            int viteza, 
            string tipHrana,
            Ecosistem ecosistem
        )
            : base(nume, energie, pozitie, rataSupravietuire, ecosistem)
        {
            Viteza = viteza;
            TipHrana = tipHrana;
        }

        public abstract void Mananca(EntitateEcosistem hrana);
        public void Deplaseaza(EntitateEcosistem entitate = null)
        {
            if (entitate != null)
            {
                Pozitie = (entitate.Pozitie);
            }
            else Pozitie = (Pozitie.X + Viteza, Pozitie.Y + Viteza);
            this.Energie -= 5;
            if (this.Energie <= 0)
            {
                Console.WriteLine($"{Nume}[{ID}] a ramas fara energie si a murit");
                _ecosistem.RemoveEntitate(this);
            }
            else Console.WriteLine($"{Nume}[{ID}] s-a deplasat la ({Pozitie.X}, {Pozitie.Y}). Energie: {this.Energie}");
        }
        public abstract override void Actioneaza();
        public abstract override void Ataca(Animal prada);

        public override void Reproduce()
        {
            int ent_count = 0;
            foreach( var entitate in _ecosistem.Entitati)
            {
                if (entitate.Nume == this.Nume)
                {
                    ent_count++;
                }
            }
            Console.WriteLine($"Entitatile \"{Nume}\" încearcă să se reproducă.");
            if (Energie > 50 && ent_count >= 2)
            {
                Energie -= 20;
                Console.WriteLine($"Entitatile \"{Nume}\" s-au reprodus cu succes!");
                var type = this.GetType();
                if (type.Name == "Erbivor")
                    _ecosistem.AddEntitate(new Erbivor($"{Nume}", 50, (8, 5), 2, _ecosistem));
                else if (type.Name == "Carnivor")
                    _ecosistem.AddEntitate(new Carnivor($"{Nume}", 50, (8, 5), 2, _ecosistem));
                else _ecosistem.AddEntitate(new Omnivor($"{Nume}", 50, (8, 5), 2, _ecosistem));
            }
            else
            {
                Console.WriteLine($"Reproducerea a esuat.");
            }
        }
    }
    public class Erbivor : Animal
    {
        public Erbivor(string nume, int energie, (int X, int Y) pozitie, int viteza, Ecosistem ecosistem)
            : base(nume, energie, pozitie, 0.7, viteza, "plante", ecosistem)
        { }

        public override void Mananca(EntitateEcosistem hrana)
        {
            if (hrana is Planta planta)
            {
                Energie += planta.Energie;
                planta.Energie = 0;
                Console.WriteLine($"{Nume}[{ID}] a mâncat {planta.Nume}[{planta.ID}]. Energie: {Energie}");
                _ecosistem.RemoveEntitate(hrana);
            }
        }
        public override void Ataca(Animal prada)
        {
            return;
        }
        public override void Actioneaza()
        {
            Random random = new Random();
            int nr = random.Next(1, 4);
            if (nr == 1)
            {
                Deplaseaza();
            }
            else if (nr == 2)
            {
                var planta = _ecosistem.Entitati.FirstOrDefault(entity => entity.Nume == "Iarba");
                Deplaseaza(planta);
                Mananca(planta);
            }
            else if (nr == 3)
            {
                Reproduce();
            }
        }
    }

    public class Carnivor : Animal
    {
        public Carnivor(string nume, int energie, (int X, int Y) pozitie, int viteza, Ecosistem ecosistem)
            : base(nume, energie, pozitie, 0.6, viteza, "animale", ecosistem)
        { }

        public override void Mananca(EntitateEcosistem hrana)
        {
            if (hrana is Animal animal)
            {
                Energie += animal.Energie;
                animal.Energie = 0;
                Console.WriteLine($"{Nume}[{ID}] a vanat si a mancat {animal.Nume}[{animal.ID}]. Energie: {Energie}");
                _ecosistem.RemoveEntitate(hrana);
            }
        }
        public override void Ataca(Animal prada)
        {
            if (prada != null)
            {
                Console.WriteLine($"{Nume}[{ID}] ataca {prada.Nume}[{prada.ID}]");
                Random srandom = new Random();
                int chance = srandom.Next(0, 100);
                if (chance <= 70)
                {
                    Mananca(prada);
                }
                else
                {
                    prada.Energie -= 10;
                    Console.WriteLine($"{prada.Nume}[{prada.ID}] a reusit sa scape. Energie: {prada.Energie}");
                    prada.Deplaseaza();
                }
            }
            else return;
        }

        public override void Actioneaza()
        {
            Random srandom = new Random();
            int nr = srandom.Next(1, 4);
            if (nr == 1)
            {
                Deplaseaza();
            }
            else if (nr == 2)
            {
                var animal = _ecosistem.Entitati.FirstOrDefault(entity => entity.Nume == "Iepure" || entity.Nume == "Caprioara");
                Deplaseaza(animal);
                Ataca((Animal)animal);
                _ecosistem.RemoveEntitate(animal);
            }
            else if (nr == 3)
            {
                Reproduce();
            }
        }
    }

    public class Omnivor : Animal
    {
        public Omnivor(string nume, int energie, (int X, int Y) pozitie, int viteza, Ecosistem ecosistem)
            : base(nume, energie, pozitie, 0.9, viteza, "plante si animale", ecosistem)
        { }

        public override void Mananca(EntitateEcosistem hrana)
        {
            if (hrana is Planta planta)
            {
                Energie += planta.Energie;
                planta.Energie = 0;
                Console.WriteLine($"{Nume}[{ID}] a mancat planta {planta.Nume}[{planta.ID}]. Energie: {Energie}");

            }
            else if (hrana is Animal animal)
            {
                Energie += animal.Energie;
                animal.Energie = 0;
                Console.WriteLine($"{Nume}[{ID}] a mancat animalul {animal.Nume}[{animal.ID}]. Energie: {Energie}");
            }
            _ecosistem.RemoveEntitate(hrana);
        }
        public override void Ataca(Animal prada)
        {
            if (prada != null)
            {
                Console.WriteLine($"{Nume}[{ID}] ataca {prada.Nume}[{prada.ID}]");
                Random random = new Random();
                int chance = random.Next(0, 100);
                if (chance <= 70)
                {
                    Mananca(prada);
                }
                else
                {
                    prada.Energie -= 15;
                    Console.WriteLine($"{prada.Nume}[{prada.ID}] a reusit sa scape. Energie: {prada.Energie}");
                    prada.Deplaseaza();
                }
            }
            else return;
        }
        public override void Actioneaza()
        {
            Random random = new Random();
            int nr = random.Next(1, 5);
            if (nr == 1)
            {
                Deplaseaza();
            }
            else if (nr == 2)
            {
                var animal = _ecosistem.Entitati.FirstOrDefault(entity => entity.Nume == "Iepure" || entity.Nume == "Caprioara");
                Deplaseaza(animal);
                Ataca((Animal)animal);
                _ecosistem.RemoveEntitate(animal);
            }
            else if (nr == 3)
            {
                var planta = _ecosistem.Entitati.FirstOrDefault(entity => entity.Nume == "Iarba");
                Deplaseaza(planta);
                Mananca(planta);
                _ecosistem.RemoveEntitate(planta);
            }
            else if (nr == 4)
            {
                Reproduce();
            }
        }
    }
}

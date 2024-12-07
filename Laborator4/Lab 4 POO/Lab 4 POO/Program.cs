using Lab_4_POO;

Ecosistem Ecosistem = new Ecosistem(10, 10);
Ecosistem.AddEntitate(new Planta("Iarba", 50, (2, 2), Ecosistem));
Ecosistem.AddEntitate(new Planta("Iarba", 50, (2, 3), Ecosistem));
Ecosistem.AddEntitate(new Planta("Iarba", 50, (3, 2), Ecosistem));
Ecosistem.AddEntitate(new Erbivor("Iepure", 100, (6, 7), 3, Ecosistem));
Ecosistem.AddEntitate(new Erbivor("Iepure", 100, (7, 6), 3, Ecosistem));
Ecosistem.AddEntitate(new Erbivor("Caprioara", 100, (1, 3), 4, Ecosistem));
Ecosistem.AddEntitate(new Carnivor("Lup", 100, (3, 1), 5, Ecosistem));
Ecosistem.AddEntitate(new Omnivor("Urs", 100, (4, 4), 1, Ecosistem));

Console.WriteLine("Starea initiala a ecosistemului");
foreach(var entitate in Ecosistem.Entitati)
{
    Console.WriteLine($"Nume: {entitate.Nume}[{entitate.ID}], Energie: {entitate.Energie}, Pozitie: {entitate.Pozitie}");
}

Ecosistem.TimePassing(3);
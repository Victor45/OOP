# Documentația proiectului.
## **Descrierea proiectului**

Ecosistemul simulează:
1. Creșterea și reproducerea plantelor.
2. Deplasarea și hrănirea animalelor (erbivore, carnivore, omnivore).
3. Interacțiunile dintre entitățile ecosistemului (hrănire, reproducere, eliminare la moarte).
4. Evoluția entităților în timp și evenimentele aleatorii din ecosistem.

## Descrierea claselor și ierarhiilor.
### 1. **Clasele principale**
- **`EntitateEcosistem` (clasă abstractă):**
  - Este baza pentru toate entitățile din ecosistem (plante și animale).
  - Atribute comune: `nume`, `energie`, `pozitie (x, y)`, `rataSupravietuire`, `ID`.
  - Metode abstracte: `Actioneaza()`, `Ataca(Animal prada)` si `Reproduce()`.
  - Cunoaște despre obiectul `Ecosistem` prin injectarea acestuia.

- **Clase derivate:**
  - `Planta`: crește și se reproduce pe baza resurselor din ecosistem.
  - `Animal` (abstractă): include atribute specifice (`viteza`, `tipHrana`) și metode precum `mananca()`, `deplaseaza()`.
  - Subclase ale `Animal`: 
    - `Erbivor` (ex. iepure) - mănâncă plante.
    - `Carnivor` (ex. lup) - vânează alte animale.
    - `Omnivor` (ex. urs) - mănâncă atât plante, cât și animale.

### 2. **Clasa `Ecosistem`**
- Include atribute specifice ecosistemului precum:
  - `MapDimensionX`, `MapDimensionY` - dimensiunea hărții.
  - `List<EntitateEcosistem> Entitati` - lista cu toate entitățile din ecosistem.
  - `Evenimente Event` - obiect ce se ocupă de generarea evenimentelor.
  - `eID` - variabilă ce reprezintă ID-ul fiecărei entități. 
- Gestionează toate entitățile ecosistemului.
- Oferă funcționalități pentru:
  - Adăugarea și eliminarea entităților.
  - Simularea interacțiunilor între entități pe o hartă de dimensiuni fixe.
  - Afișarea stării ecosistemului după fiecare pas al simulării.

### 3. **Clasa `Evenimente`**
- Gestionează evenimentele ecosistemului:
  - Furtună.
  - Secetă.
- Cunoaște despre obiectul `Ecosistem` prin injectarea acestuia.

### 4. **Interfața `Interactiune`**
- Asigură un comportament comun pentru:
  - `ataca(Animal prada)` - pentru carnivore.
  - `reproduce()` - pentru toate entitățile.

## Explicația fiecărei metode.
1.	`ataca(Animal prada)` – Metodă responsabilă pentru comportamentul animalului în timpul unui atac. Primește ca parametru un `Animal`, care semnifică prada acestuia. Pentru a ataca prada, prădătorul mai întâi se va deplasa la poziția acesteia. În cadrul acestei metode există o șansă de 70% ca prada să fie mâncată, respectiv sunt șanse de 30% ca prada să scape în urma atacului, deplasându-se la o altă poziție pe hartă. În cazul în care prada reușește să scape, energia sa scade cu 20 de unități.
2.	`reproduce()` – Metodă responsabilă pentru reproducerea entităților din ecosistem. Pentru reproducerea plantelor, acestea au nevoie de minim 75 energie pentru a se putea reproduce. Pentru animale, este necesar ca energia acestora să fie mai mare de 50 și să fie minim 2 entități de aceeași specie în ecosistem. În momentul reproducerii energia entităților se reduce și o nouă entitate apare în ecosistem.
3.	`Creste()` – Metodă responsabilă pentru creșterea plantelor. În cadrul acesteia energia plantelor crește cu 20 de unități.
4.	`Deplaseaza(EntitateEcosistem entitate = null)` – Metodă responsabilă pentru deplasarea animalelor pe hartă. Poate primi ca parametru o entitate spre care se realizează deplasarea, iar în cazul în care este apelată fără parametri se deplasează la o poziție aleatorie. În momentul deplasării energia entității scade cu 5 unități.
5.	`Mananca(EntitateEcosistem hrana)` – Metodă în care are loc procesul de hrănire a animalelor. Parametrul primit reprezintă entitatea care urmează să fie mâncată. Pentru a mânca o entitate, consumatorul mai întâi se va deplasa la poziția acesteia. În momentul în care o entitate este mâncată, energia acesteia se transferă la energia consumatorului, iar entitatea mâncată este eliminată din ecosistem.
6.	`Actioneaza()` – În această metodă programul alege aleatoriu o acțiune dintre cele prezentate mai sus. Respectiv la fiecare apel al metodei, entitățile vor efectua o acțiune diferită. Astfel comportamentul în ecosistem va fi diversificat.
7.	`AddEntitate(EntitateEcosistem entitate)` – Metodă prin care are loc adăugarea unei entități în ecosistem, parametrul primit fiind entitatea care urmează să fie adăugată în ecosistem.
8.	`RemoveEntitate(EntitateEcosistem entitate)` - Metodă prin care are loc eliminarea unei entități din ecosistem, parametrul primit fiind entitatea care urmează să fie eliminată din ecosistem.
9.	`TimePassing(int nr_cicluri)` – Reprezintă metoda care simulează trecerea timpului și evoluția entităților. Trecerea timpului se realizează printr-un număr de cicluri, introduse de utilizator, în care entitățile realizează anumite acțiuni determinate aleatoriu (crestere, reproducere, deplasare, mancare, atac). Începând cu al 2-lea ciclu, energia fiecărei entități scade cu 10 unități din cauza trecerii timpului. Totodată, primul ciclu se realizează fară evenimente aleatorii, iar în următoarele cicluri apare posibilitatea de generare aleatorie a evenimentelor precum furtună sau secetă, fiind posibil doar un eveniment per ciclu.
10.	`Furtuna()` – Metodă responsabilă pentru generarea unei furtuni. În timpul furtunii există o șansă de 30% pentru fiecare entitate ca aceasta să fie afectată de furtună. În cazul în care este afectată o plantă, aceasta pierde 20 de unități din energie, în cazul unui animal, acesta pierde doar 5 unități din energie.
11.	`Seceta()` – Metodă responsabilă pentru generarea unei secete. În timpul secetei toate entitățile ecosistemului sunt afectate. Plantele pierd 70 de unități din energie, iar animalele pierd 50 de unități din energie. În cazul când energia unei entități scade sub 0 (inclusiv), aceasta este eliminată din ecosistem.
12.	`FinalRaport()` – În această metodă are loc afișarea stării finale a ecosistemului în urma simulării unui ciclu. În consolă vor fi afișate numele, poziția și energia fiecărei entități rămase.

## Diagrama UML a programului.
![image](https://github.com/user-attachments/assets/823afbc7-8abe-4b23-8ae5-5912e681cfc0)

## Scenarii de utilizare.
  Simularea ecosistemului a fost realizata în 3 cicluri de trecere a timpului, fiecare ciclu fiind un scenariu diferit. In primul ciclu a fost simulat comportamentul entităților fără prezența unui eveniment aleatoriu. În al doilea ciclu a fost simulată evoluția entităților în prezența unei furtuni, iar în al 3-lea ciclu evenimentul prezent este seceta. Interacțiunile între entități sunt afișate în timpul ciclului, la fiecare acțiune manifestată. La finalul unui ciclu este afișat un raport cu starea ecosistemului în urma simulării.

#### 1. Starea inițială a ecosistemului.
![image](https://github.com/user-attachments/assets/fa7ca4d9-7f55-49c9-8de8-2631eb16951d)

#### 2. Primul ciclu din ecosistem - Fără evenimente aleatorii.
![image](https://github.com/user-attachments/assets/df330634-afbf-4dfb-aca6-f5dd0409957f)

#### 3. Al doilea ciclu din ecosistem - Furtună.
![image](https://github.com/user-attachments/assets/3dcaad8e-68dc-4745-8ae6-c2850268f204)

#### 4. Al treilea ciclu din ecosistem - Secetă
![image](https://github.com/user-attachments/assets/1f319d2b-7147-4c5f-9179-e62944b7da57)


## Dificultățile întâlnite și soluțiile adoptate.
- A fost adăugată o variailă `ID` pentru fiecare entitate pentru a le putea diferenția în ecosistem.
- Pentru a avea acces la obiectul `Ecosistem` creat în programul principal a fost nevoită injectarea acestuia în clasele `EntitateEcosistem` și `Evenimente`.

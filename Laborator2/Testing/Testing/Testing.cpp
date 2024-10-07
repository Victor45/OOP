
#include <iostream>
using namespace std;

class Student
{
	friend void myname(Student& object);
	friend void getmoney(Student& object);

private:
	string name;
	int money = 2500;

public:
	Student(string n)
	{
		name = n;
	}

	friend class Student1;

	void showdet()
	{
		cout << "My name: " << name << endl;
		cout << "I have " << money << " dollars" << endl;
	}
};

void myname(Student& object)
{
	cout << "Numele meu este " << object.name << endl;
}

void getmoney(Student& object)
{
	cout << "Banii unui student: " << object.money << endl;
}

class Student2;

class Student1
{
private:
	int grade1;

public:
	Student1()
	{
		grade1 = 9;
	}
	
	friend class Student2;

	void showgrade2(Student2& s);

	void changenam(Student& object)
	{
		object.name = "Mike";
		object.money = 3 * object.money;
	}
};

class Student2
{
private:
	int grade2;

public:
	Student2()
	{
		grade2 = 5;
	}
	friend class Student1;

	void showgrade1(Student1& s)
	{
		cout << "Primul student, nota: " << s.grade1 << endl;
	}
};

void Student1::showgrade2(Student2& s)
{
	cout << "Al doilea student, nota: " << s.grade2 << endl;
}


int main()
{
	Student Me("Victor");
	myname(Me);
	getmoney(Me);

	Student1 elev1;
	elev1.changenam(Me);
	Me.showdet();

	Student2 elev2;

	elev2.showgrade1(elev1);
	elev1.showgrade2(elev2);

	return 0;
}

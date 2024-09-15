#include <iostream>
using namespace std;

class Student
{
	friend void myname(Student& object);

private:
	string name;

public:
	Student(string n)
	{
		name = n;
	}
};

void myname(Student& object)
{
	cout << "Numele meu este " << object.name << endl;
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

	Student1 elev1;
	Student2 elev2;

	elev2.showgrade1(elev1);
	elev1.showgrade2(elev2);

	return 0;
}
#include <iostream>

using namespace std;

class Person
{
public:
	virtual string getRole() = 0;
};

class Student : public Person
{
public:
	string getRole() override
	{
		return "Student";
	}
};

class Teacher : public Person
{
public:
	string getRole() override
	{
		return "Teacher";
	}
};

class Administrator : public Person
{
public:
	string getRole() override
	{
		return "Administrator";
	}
};

void printRole( Person* person)
{
	if (person)
	{
		cout << "Role: " << person->getRole() << endl; 
	}
	else
	{
		cout << "Null pointer provided!" << endl; 
	}
}

int main()
{
	Student student;
	Teacher teacher;
	Administrator administrator;

	cout << "Class Student Role: " << student.getRole() << endl;
	cout << "Class Teacher Role: " << teacher.getRole() << endl;
	cout << "Class Administrator Role: " << administrator.getRole() << endl;

	Person* person1 = new Student();
	Person* person2 = new Teacher();
	Person* person3 = new Administrator();

	printRole(person1);
	printRole(person2);
	printRole(person3);

	return 0;
}
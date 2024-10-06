#include <iostream>

using namespace std;

class Person
{
public:
	string name;
	int age;

	Person(string n, int a)
	{
		name = n;
		age = a;
	}
};

class Student :public Person
{
public:
	int studentID;
	string faculty;

	Student(string n, int a, int id, string f) : Person(n, a)
	{
		studentID = id;
		faculty = f;
	}
};

class Employee
{
public:
	string job;
	int salary;

	Employee(string j, int s)
	{
		job = j;
		salary = s;
	}
};

class StudentEmployee : public Student, public Employee
{
public:
	string experience;

	StudentEmployee(string n, int a, int id, string f, string j, int s, string exp) : Student(n, a, id, f), Employee(j, s)
	{
		experience = exp;
	}
};

int main()
{
	Student Me("Victor", 19, 3231, "FCIM");
	cout << "Name: " << Me.name << endl;
	cout << "Age: " << Me.age << endl;
	cout << "ID: " << Me.studentID << endl;
	cout << "Faculty: " << Me.faculty << endl;

	StudentEmployee st("Cristian", 20, 34525, "FCIM", "Developer", 1500, "Senior");
	cout << endl;
	cout << "Name: " << st.name << endl;
	cout << "Age: " << st.age << endl;
	cout << "ID: " << st.studentID << endl;
	cout << "Faculty: " << st.faculty << endl;
	cout << "Job: " << st.job << endl;
	cout << "Salary: " << st.salary << endl;
	cout << "Experience: " << st.experience;
	return 0;
}

#include <iostream>
#include <vector>
using namespace std;

class Student
{
public:
	string name;
	int age;

	Student(string n, int a) : name(n), age(a) {};

	void showinfo()
	{
		cout << "Name: " << name << ", age: " << age << endl;
	}
};

class Faculty
{
public:
	string fname;
	vector <Student*> students;
	
	Faculty(string name) : fname(name) {};

	void addStudent(Student* student)
	{
		students.push_back(student);
	}

	void showStudents()
	{
		for (Student* i : students)
		{
			cout << "Name: " << i->name << ", age: " << i->age << endl;
		}
	}

	~Faculty()
	{
		cout << "Facultatea a fost distrusa." << endl;
	}
};

class Book
{
public:
	string title;
	int pages;

	Book(string t, int p) : title(t), pages(p) {};
};

class Library
{
public:
	vector <Book> books;

	void addBook(const Book& book)
	{
		books.push_back(book);
	}

	void showBooks()
	{
		for (Book& i : books)
		{
			cout << "Title: " << i.title << ", pages: " << i.pages << endl;
		}
	}

	~Library() {};
};

int main()
{
	Faculty faculty("FCIM");

	Student student1("Victor", 19);
	Student student2("Ion", 20);
	Student student3("Cristi", 20);

	faculty.addStudent(&student1);
	faculty.addStudent(&student2);
	faculty.addStudent(&student3);

	faculty.showStudents();

	cout << endl;

	Library library;
	library.addBook(Book("OOP", 80));
	library.addBook(Book("APA", 120));
	library.addBook(Book("ASDN", 60));

	library.showBooks();

	return 0;
}
#include <iostream>
#include <string>
using namespace std;

class Parinte
{

public:
	string eyes = "Caprui";
	string hair;

	void details()
	{
		cout << "Culoarea ochilor: " << eyes << endl;
	}

};

class copil : public Parinte
{
public:

	string hobby = "Fotbal";
	int age = 20;

	void showinfo()
	{
		cout << "Hobby: " << hobby << endl;
		cout << "Varsta: " << age << endl;
		cout << "Culoarea parului: " << hair << endl;
	}
};


int main()
{
	copil copil1;
	copil1.hair = "Blond";
	copil1.showinfo();
	copil1.details();

	return 0;
}
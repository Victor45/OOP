#include <iostream>
using namespace std;

class Student {
public:
    string name;
    int credits; 

    Student(string n, int c) : name(n), credits(c) {}

    Student operator++()
    {
        credits++;
        return *this;
    }

    Student operator++(int)
    {
        Student temp = *this;
        credits++;
        return temp;
    }

    Student operator+(const Student& other)
    {
        return Student(name, credits + other.credits);
    }
    
    void showinfo()
    {
        cout << "Student: " << name << ", credits: " << credits << endl;
    }
};

int main() {
    Student student1("Victor", 30);
    Student student2("Ion", 25);

    Student student3 = student1 + student2;
    student3.showinfo();

    ++student3;
    student3.showinfo();

    student3++;
    student3.showinfo();

    return 0;
}

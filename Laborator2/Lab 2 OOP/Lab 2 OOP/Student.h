#pragma once
#include <iostream>
#include <string>

using namespace std;

class Student {
public:
    string firstName;
    string lastName;
    string email;
    string enrollmentDate;
    string birthDate;

    Student(string fn, string ln, string e, string ed, string bd);

    void showInfo();
};


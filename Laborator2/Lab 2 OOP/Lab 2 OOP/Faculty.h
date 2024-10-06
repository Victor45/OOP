#pragma once
#include <iostream>
#include <string>
#include <list>
#include "Student.h"

using namespace std;

enum StudyField
{
    MECHANICAL_ENGINEERING,
    SOFTWARE_ENGINEERING,
    FOOD_TECHNOLOGY,
    URBANISM_ARHITECTURE,
    VETERINARY_MEDICINE
};

string SFToString(StudyField sf);

class Faculty {
public:
    string name;
    string abreviation;
    list<Student> students;
    list<Student> Graduated;
    StudyField studyfield;

    Faculty(string n, string a, StudyField sf);

    void assignStudent(Student& student);
    void enrolled();
    void graduated();
    void belongsornot(string email);
};


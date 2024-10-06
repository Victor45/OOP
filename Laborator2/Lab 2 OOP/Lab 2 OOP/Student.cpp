#include "Student.h"

Student::Student(string fn, string ln, string e, string ed, string bd) {
    firstName = fn;
    lastName = ln;
    email = e;
    enrollmentDate = ed;
    birthDate = bd;
}


void Student::showInfo() {
    cout << firstName << " " << lastName << ", " << email << ", Enrollment Date: " << enrollmentDate << ", Birth Date: " << birthDate << endl;
}
#include "Faculty.h"

string SFToString(StudyField sf)
{
    switch (sf)
    {
    case MECHANICAL_ENGINEERING: return "MECHANICAL_ENGINEERING";
    case SOFTWARE_ENGINEERING: return "SOFTWARE_ENGINEERING";
    case FOOD_TECHNOLOGY: return "FOOD_TECHNOLOGY";
    case URBANISM_ARHITECTURE: return "URBANISM_ARHITECTURE";
    case VETERINARY_MEDICINE: return "VETERINARY_MEDICINE";
    }
}

Faculty::Faculty(string n, string a, StudyField sf) {
    name = n;
    abreviation = a;
    studyfield = sf;
}

void Faculty::assignStudent(Student& student) {
    students.push_back(student);
}

void Faculty::enrolled() {
    if (students.empty()) {
        cout << "There are no students enrolled in this faculty." << endl;
    }
    else {
        cout << abreviation << "'s enrolled students (" << students.size() <<  "): " << endl;
        for (Student& i : students) {
            cout << i.firstName << " " << i.lastName << ", " << i.email << ", Enrollment Date: " << i.enrollmentDate << endl;
        }
    }
}

void Faculty::graduated() {
    if (Graduated.empty()) {
        cout << "There are no graduated students of this faculty." << endl;
    }
    else {
        cout << abreviation << "'s graduated students: " << endl;
        for (Student& i : Graduated) {
            cout << i.firstName << " " << i.lastName << ", " << i.email << endl;
        }
    }
}

void Faculty::belongsornot(string email) {
    int belongs = 0;
    for (Student& i : students) {
        if (i.email == email) {
            belongs = 1;
            cout << "This student belongs to this faculty." << endl;
            return;
        }
    }
    if (belongs == 0) {
        cout << "This student does not belong to this faculty." << endl;
    }
}
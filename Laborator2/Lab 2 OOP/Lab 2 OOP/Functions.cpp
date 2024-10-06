#include "Functions.h"
#include "Faculty.h"
#include "Logger.h"
#include "GlobalVariables.h"

using namespace std;

string newFaculty()
{
    int size = faculties.size();

    cin.ignore();
    string name, abreviation;
    StudyField studyfield;
    int input;
    cout << "Name: ";
    getline(cin, name);
    cout << "Abreviation: ";
    getline(cin, abreviation);
    cout << "StudyField (ME (0), SE (1), FT (2), UA (3), VM (4)): ";
    cin >> input;
    studyfield = static_cast<StudyField>(input);

    Faculty faculty1(name, abreviation, studyfield);
    faculties.push_back(faculty1);

    if (faculty1.name == name && size + 1 == faculties.size()) {
        return faculty1.name;
    }
    else return "";
}

void belongsbyemail()
{
    string email;
    cout << "Enter the student email: ";
    cin >> email;
    int belongs = 0;
    for (Faculty& i : faculties)
    {
        for (Student& j : i.students)
        {
            if (j.email == email)
            {
                belongs = 1;
                cout << "This student belongs to " << i.name << endl;
            }
        }
    }
    if (belongs == 0)
    {
        cout << "This student does not belong to any faculty." << endl;
    }
}

void displayFaculties()
{
    cout << "University faculties:" << endl;
    for (Faculty& i : faculties)
    {
        cout << "Name: " << i.name << ", Abreviation: " << i.abreviation << ", StudyField: " << SFToString(i.studyfield) << endl;
    }
}

void byField()
{
    StudyField sf;
    int input;
    cout << "Choose a field ME(0), SE(1), FT(2), UA(3), VM(4): ";
    cin >> input;
    sf = static_cast<StudyField>(input);
    int field = 0;

    for (Faculty& i : faculties)
    {
        if (i.studyfield == sf)
        {
            field = 1;
            cout << "Name: " << i.name << ", Abreviation: " << i.abreviation << ", StudyField: " << SFToString(i.studyfield) << endl;
        }
    }
    if (field == 0)
    {
        cout << "There are no faculties belonging to this field." << endl;
    }
}

void studentinfo()
{
    string email;
    cout << "Enter the student email: ";
    cin >> email;
    int found = 0;
    for (Faculty& i : faculties)
    {
        for (Student& j : i.students)
        {
            if (j.email == email)
            {
                found = 1;
                j.showInfo();
                cout << "Status: Enrolled" << endl;
            }
        }
        for (Student& j : i.Graduated)
        {
            if (j.email == email)
            {
                found = 1;
                j.showInfo();
                cout << "Status: Graduated" << endl;
            }
        }
    }
    if (found == 0)
    {
        cout << "This student does not exist." << endl;
    }
}

Student createStudent()
{
    string fname, lastname, email, enroll, birth;
    int id;
    cin.ignore();
    cout << "First Name: ";
    getline(cin, fname);
    cout << "Last Name: ";
    getline(cin, lastname);
    cout << "Email: ";
    getline(cin, email);
    cout << "Enrollment Date: ";
    getline(cin, enroll);
    cout << "Birth Date: ";
    getline(cin, birth);
    Student newStudent(fname, lastname, email, enroll, birth);
    return newStudent;
}

void createandassign()
{
    Student newStudent = createStudent();
    if (newStudent.firstName != "" && newStudent.lastName != "") {
        logger.log(INFO, "Student " + newStudent.firstName + " was created");
    }
    else logger.log(ERROR, "Student was not created");
    string faculty;
    int f = 0;
    do {
        cout << "Faculty to assign (abreviation): ";
        cin >> faculty;
        for (Faculty& i : faculties)
        {
            if (i.abreviation == faculty)
            {
                f = 1;
                i.assignStudent(newStudent);
                logger.log(INFO, "Student " + newStudent.firstName + " was assigned to the faculty " + faculty);
            }
        }
        if (f == 0) {
            logger.log(ERROR, "Faculty " + faculty + " doesn't exist");
            logger.log(ERROR, "Student " + newStudent.firstName + " was not assigned to " + faculty);
        }
    } while (f == 0);
}

void graduate()
{
    string email;
    cout << "Enter the student email: ";
    cin >> email;
    int graduate = 0;
    for (Faculty& i : faculties)
    {
        for (auto it = i.students.begin(); it != i.students.end();)
        {
            if (it->email == email)
            {
                graduate = 1;
                i.Graduated.push_back(*it);
                it = i.students.erase(it);
            }
            else {
                it++;
            }
        }
    }
    if (graduate == 1)
    {
        logger.log(INFO, "Student " + email + " was graduated");
    }
    else {
        logger.log(ERROR, "The student " + email + " was not found.");
    }
}

void displayenrolled()
{
    string faculty;
    cout << "Students of which faculty (abreviation)?: ";
    cin >> faculty;
    int f = 0;
    for (Faculty& i : faculties)
    {
        if (i.abreviation == faculty)
        {
            f = 1;
            i.enrolled();
        }
    }
    if (f == 0) logger.log(ERROR, "Faculty " + faculty + " doesn't exist");
}

void displaygraduated()
{
    string faculty;
    cout << "Gratuated students of which faculty (abreviation)?: ";
    cin >> faculty;
    int f = 0;
    for (Faculty& i : faculties)
    {
        if (i.abreviation == faculty)
        {
            f = 1;
            i.graduated();
        }
    }
    if (f == 0) logger.log(ERROR, "Faculty " + faculty + " doesn't exist");
}

void tellbelongsornot()
{
    string email, faculty;
    cout << "Enter the student email: ";
    cin >> email;
    cout << "Enter the faculty abreviation: ";
    cin >> faculty;
    int f = 0;
    for (Faculty& i : faculties)
    {
        if (i.abreviation == faculty)
        {
            f = 1;
            i.belongsornot(email);
        }
    }
    if (f == 0) logger.log(ERROR, "Faculty " + faculty + " doesn't exist");
}




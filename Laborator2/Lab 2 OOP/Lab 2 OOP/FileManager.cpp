#include "FileManager.h"
#include <string>
#include <fstream>
#include <sstream>
#include "Logger.h"
#include "GlobalVariables.h"
#include "Functions.h"

using namespace std;

void FileManager::saveData(const list<Faculty>& faculties)
{
    ofstream outFile("university_data.txt");

    if (!outFile) {
        cerr << "Error: Could not open file for writing." << endl;
        return;
    }

    outFile << faculties.size() << endl;
    for (const auto& faculty : faculties) {
        outFile << faculty.name << "," << faculty.abreviation << "," << faculty.studyfield << endl;
        outFile << faculty.students.size() << endl;
        for (const auto& student : faculty.students) {
            outFile << student.firstName << "," << student.lastName << "," << student.email << "," << student.enrollmentDate << "," << student.birthDate << endl;
        }

        outFile << faculty.Graduated.size() << endl;
        for (const auto& graduate : faculty.Graduated) {
            outFile << graduate.firstName << "," << graduate.lastName << "," << graduate.email << "," << graduate.enrollmentDate << "," << graduate.birthDate << endl;
        }
    }

    outFile.close();
    logger.log(INFO, "Data saved successfully.");
}

void FileManager::loadData(list<Faculty>& faculties)
{
    ifstream inFile("university_data.txt");

    if (!inFile) {
        cerr << "Error: Could not open file for reading." << endl;
        return;
    }

    faculties.clear();
    int numFaculties, numStudents, numGraduated;
    string line;

    getline(inFile, line);
    numFaculties = stoi(line);

    for (int i = 0; i < numFaculties; i++) {
        string name, abv;
        int sf;

        getline(inFile, line);
        stringstream ss(line);
        getline(ss, name, ',');
        getline(ss, abv, ',');
        ss >> sf;

        Faculty faculty(name, abv, static_cast<StudyField>(sf));

        getline(inFile, line);
        numStudents = stoi(line);

        for (int j = 0; j < numStudents; ++j) {
            string fname, lname, email, enrollDate, birthDate;

            getline(inFile, line);
            stringstream ss2(line);
            getline(ss2, fname, ',');
            getline(ss2, lname, ',');
            getline(ss2, email, ',');
            getline(ss2, enrollDate, ',');
            getline(ss2, birthDate);

            Student student(fname, lname, email, enrollDate, birthDate);
            faculty.students.push_back(student);
        }

        getline(inFile, line);
        numGraduated = stoi(line);

        for (int j = 0; j < numGraduated; ++j) {
            string fname, lname, email, enrollDate, birthDate;

            getline(inFile, line);
            stringstream ss2(line);
            getline(ss2, fname, ',');
            getline(ss2, lname, ',');
            getline(ss2, email, ',');
            getline(ss2, enrollDate, ',');
            getline(ss2, birthDate);

            Student graduate(fname, lname, email, enrollDate, birthDate);
            faculty.Graduated.push_back(graduate);
        }
        faculties.push_back(faculty);
    }
    inFile.close();
    logger.log(INFO, "Data loaded successfully.");
}

void FileManager::BatchEnroll(Faculty& faculty)
{
    ifstream inFile("batch_enroll.txt");

    if (!inFile) {
        cerr << "Error: Could not open file for reading." << endl;
        return;
    }

    int students;
    string line;

    getline(inFile, line);
    students = stoi(line);

    for (int i = 0; i < students; i++)
    {
        string fname, lname, email, enrollDate, birthDate;

        getline(inFile, line);
        stringstream ss(line);
        getline(ss, fname, ',');
        getline(ss, lname, ',');
        getline(ss, email, ',');
        getline(ss, enrollDate, ',');
        getline(ss, birthDate);

        Student student(fname, lname, email, enrollDate, birthDate);
        faculty.students.push_back(student);
    }
    inFile.close();
    logger.log(INFO, "Students enrolled successfully");
}

void FileManager::BatchGraduate(Faculty& faculty)
{
    ifstream inFile("university_data.txt");

    if (!inFile) {
        cerr << "Error: Could not open file for reading." << endl;
        return;
    }

    string line, name, abv, fname, lname, email;
    int graduated = 0;
    while (!graduated) {
        getline(inFile, line);
        if (line == "") {
            logger.log(ERROR, "There are no students to graduate");
            return;
        }
        stringstream temp(line);
        string name, abv;
        getline(temp, name, ',');
        getline(temp, abv, ',');
        if (abv == faculty.abreviation)
        {
            getline(inFile, line);
            if (line == "0") {
                logger.log(ERROR, "There are no students to graduate");
                return;
            }
            int numStudents = stoi(line);
            for (int i = 0; i < numStudents; i++)
            {
                getline(inFile, line);
                stringstream temp2(line);
                getline(temp2, fname, ',');
                getline(temp2, lname, ',');
                getline(temp2, email, ',');
                graduate(email);
            }
            graduated = 1;
        }
    }
    if (graduated == 1) logger.log(INFO, "Students graduated successfully");
}
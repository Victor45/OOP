#include <iostream>
#include <list>
#include <stdbool.h>
#include <cstdlib>
#include <string>
#include <fstream>
#include <sstream>
#include "Faculty.h"
#include "FileManager.h"
#include "Logger.h"
#include "Functions.h"
#include "GlobalVariables.h"

using namespace std;

int main()
{
    int choice = 0;
    int choice1 = 0;
    int choice2 = 0;
    int savedData = 0;
    while (true)
    {
        cout << endl;
        cout << "1. General operations." << endl;
        cout << "2. Faculty operations." << endl;
        cout << "3. Save session." << endl;
        cout << "4. Load previous session." << endl;
        cout << "0. Exit." << endl;
        cout << "Choose an option: ";
        cin >> choice;
        if (choice < 0 || choice > 5)
        {
            logger.log(WARNING, "invalid option");
        }
        switch (choice)
        {
        case 1:
        {
            choice1 = 0;
            while (choice1 != 6)
            {
                cout << endl;
                cout << "What do you want to do?" << endl;
                cout << "1. Create a new faculty." << endl;
                cout << "2. Search what faculty a student belongs to by a unique identifier (email)." << endl;
                cout << "3. Display University faculties." << endl;
                cout << "4. Display all faculties belonging to a field." << endl;
                cout << "5. Display the information about a student." << endl;
                cout << "6. Back." << endl;
                cout << "Choose an option: ";
                cin >> choice1;
                switch (choice1)
                {
                case 1:
                {
                    newFaculty();
                    break;
                }
                case 2:
                {
                    belongsbyemail();
                    break;
                }
                case 3:
                {
                    displayFaculties();
                    break;
                }
                case 4:
                {
                    byField();
                    break;
                }
                case 5:
                    studentinfo();
                    break;
                }
            }
            break;
        }
        case 2:
        {
            choice2 = 0;
            while (choice2 != 8)
            {
                cout << endl;
                cout << "What do you want to do?" << endl;
                cout << "1. Create and assign a student to a faculty." << endl;
                cout << "2. Graduate a student from a faculty." << endl;
                cout << "3. Display current enrolled students." << endl;
                cout << "4. Display graduates." << endl;
                cout << "5. Tell or not if a student belongs to this faculty." << endl;
                cout << "6. Batch enrollment." << endl;
                cout << "7. Batch graduation." << endl;
                cout << "8. Back." << endl;
                cout << "Choose an option: ";
                cin >> choice2;
                switch (choice2)
                {
                case 1:
                {
                    if (!faculties.empty()) {
                        createandassign();
                    }
                    else logger.log(ERROR, "You need to create a faculty first!");
                    break;
                }
                case 2:
                {
                    graduate();
                    break;
                }

                case 3:
                {
                    displayenrolled();
                    break;
                }
                case 4:
                {
                    displaygraduated();
                    break;
                }
                case 5:
                {
                    tellbelongsornot();
                    break;
                }
                case 6:
                {
                    batchenrollment();
                    break;
                }
                case 7:
                {
                    if (savedData == 1) {
                        batchgraduation();
                    }
                    else logger.log(ERROR, "You need to save the session first!");
                    break;
                }
                }
            }
            break;
        }
        case 3:
        {
            FileManager::saveData(faculties);
            savedData = 1;
            break;
        }
        case 4:
        {
            FileManager::loadData(faculties);
            break;
        }
        case 0:
        {
            return 0;
        }
        }
    }
    return 0;
}


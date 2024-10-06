#pragma once
#include <fstream>
#include <iostream>
#include <sstream>
#include <string>

using namespace std;

enum LogLevel { DEBUG, INFO, WARNING, ERROR, CRITICAL };

class Logger {
public:

    Logger(const string& filename);

    ~Logger();

    void log(LogLevel level, const string& message);

private:
    ofstream logFile;

    string levelToString(LogLevel level);
};

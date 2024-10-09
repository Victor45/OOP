#pragma once
#include <fstream>
#include <iostream>
#include <sstream>
#include <string>

using namespace std;

enum LogLevel { DEBUG, INFO, WARNING, ERROR, CRITICAL };

class OperationLogger {
public:
    virtual void log(LogLevel level, const string& message) = 0;

    string levelToString(LogLevel level)
    {
        switch (level) {
        case DEBUG:
            return "DEBUG";
        case INFO:
            return "INFO";
        case WARNING:
            return "WARNING";
        case ERROR:
            return "ERROR";
        case CRITICAL:
            return "CRITICAL";
        default:
            return "UNKNOWN";
        }
    }
};

class FileLogger : public OperationLogger {
public:
    FileLogger(const string& filename)
    {
        logFile.open(filename, ios::app);
        if (!logFile.is_open()) {
            cerr << "Error opening log file." << endl;
        }
    }

    ~FileLogger() { logFile.close(); };

    void log(LogLevel level, const string& message) override {
        time_t now = time(0);
        tm timeinfo;

        errno_t err = localtime_s(&timeinfo, &now);

        if (err != 0) {
            cerr << "Error converting time to local time." << endl;
            return;
        }

        char timestamp[20];

        strftime(timestamp, sizeof(timestamp), "%Y-%m-%d %H:%M:%S", &timeinfo);

        ostringstream logEntry;
        logEntry << "[" << timestamp << "] " << levelToString(level) << ": " << message << endl;

        if (logFile.is_open()) {
            logFile << logEntry.str();
            logFile.flush();
        }
    }

private:
    ofstream logFile;
};

class ConsoleLogger : public OperationLogger 
{
public: 

    void log(LogLevel level, const string& message) override
    {
        time_t now = time(0);
        tm timeinfo;

        errno_t err = localtime_s(&timeinfo, &now);

        if (err != 0) {
            cerr << "Error converting time to local time." << endl;
            return;
        }

        char timestamp[20];

        strftime(timestamp, sizeof(timestamp), "%Y-%m-%d %H:%M:%S", &timeinfo);

        ostringstream logEntry;
        logEntry << "[" << timestamp << "] " << levelToString(level) << ": " << message << endl;

        cout << logEntry.str();
    }
};

int main()
{
    OperationLogger* filelog = new FileLogger("logfile.txt");
    OperationLogger* consolelog = new ConsoleLogger();
    
    filelog->log(INFO, "Successfully using polimorphism");
    consolelog->log(INFO, "Successfully using polimorphism");

    return 0;
}
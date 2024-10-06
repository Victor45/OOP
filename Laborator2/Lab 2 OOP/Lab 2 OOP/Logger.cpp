#include "Logger.h"
#include <ctime>

Logger::Logger(const string& filename)
{
    logFile.open(filename, ios::app);
    if (!logFile.is_open()) {
        cerr << "Error opening log file." << endl;
    }

}

Logger::~Logger() { logFile.close(); }

void Logger::log(LogLevel level, const string& message)
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

    if (logFile.is_open()) {
        logFile << logEntry.str();
        logFile.flush();
    }
}

string Logger::levelToString(LogLevel level)
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

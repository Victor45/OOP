#pragma once
#include "Faculty.h"
class FileManager
{
public:
	static void saveData(const list<Faculty>& faculties);
	static void loadData(list<Faculty>& faculties);
	static void BatchEnroll(Faculty& faculty);
	static void BatchGraduate(Faculty& faculty);
};


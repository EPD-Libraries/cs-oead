#pragma once
#include <stdexcept>

#if _WIN32
#define EXP extern "C" __declspec(dllexport)
#else
#define EXP extern "C"
#endif
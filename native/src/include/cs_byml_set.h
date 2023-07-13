#pragma once

#include <oead/byml.h>

#include "export.h"

using namespace oead;

EXP Byml* BymlSetHash(Byml::Hash* value);
EXP Byml* BymlSetArray(Byml::Array* value);
EXP Byml* BymlSetString(char* value);
EXP Byml* BymlSetBinary(u8* value, int value_len);
EXP Byml* BymlSetInt(S32 value);
EXP Byml* BymlSetUInt(U32 value);
EXP Byml* BymlSetFloat(F32 value);
EXP Byml* BymlSetInt64(S64 value);
EXP Byml* BymlSetUInt64(U64 value);
EXP Byml* BymlSetDouble(F64 value);
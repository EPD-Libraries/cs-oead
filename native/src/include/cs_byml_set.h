#pragma once

#include <oead/byml.h>

#include "export.h"

using namespace oead;

EXP Byml* BymlSetHash(Byml::Hash* value);
EXP Byml* BymlSetArray(Byml::Array* value);
EXP Byml* BymlSetString(char* value);
EXP Byml* BymlSetBinary(u8* value, int value_len);
EXP Byml* BymlSetInt(s32 value);
EXP Byml* BymlSetUInt(u32 value);
EXP Byml* BymlSetFloat(f32 value);
EXP Byml* BymlSetInt64(s64 value);
EXP Byml* BymlSetUInt64(u64 value);
EXP Byml* BymlSetDouble(f64 value);
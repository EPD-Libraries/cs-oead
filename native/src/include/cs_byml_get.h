#pragma once

#include <oead/byml.h>

#include "export.h"

using namespace oead;

EXP Byml::Hash* BymlGetHash(Byml* byml);
EXP Byml::Array* BymlGetArray(Byml* byml);
EXP std::string* BymlGetString(Byml* byml);
EXP std::vector<u8>* BymlGetBinary(Byml* byml);
EXP bool BymlGetBool(Byml* byml);
EXP s32 BymlGetInt(Byml* byml);
EXP u32 BymlGetUInt(Byml* byml);
EXP f32 BymlGetFloat(Byml* byml);
EXP s64 BymlGetInt64(Byml* byml);
EXP u64 BymlGetUInt64(Byml* byml);
EXP f64 BymlGetDouble(Byml* byml);
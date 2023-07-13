#pragma once

#include <oead/byml.h>

#include "export.h"

using namespace oead;

EXP Byml* BymlArrayGet(Byml::Array* array, int index);
EXP void BymlArraySet(Byml::Array* array, int index, Byml* value);
EXP void BymlArrayAdd(Byml::Array* array, Byml* value);
EXP void BymlArrayRemove(Byml::Array* array, Byml* value);
EXP void BymlArrayRemoveAt(Byml::Array* array, int index);
EXP void BymlArrayClear(Byml::Array* array);
EXP int BymlArrayLength(Byml::Array* array);

EXP Byml::Array* BymlArrayNew();

EXP bool BymlArrayFree(Byml::Array* array);
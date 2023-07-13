#pragma once

#include <oead/byml.h>

#include "export.h"

using namespace oead;

EXP void* BymlFromBinary(const u8* src, u32 src_len, Byml** output);
EXP void* BymlFromText(const char* src, Byml** output);
EXP void* BymlToBinary(Byml* byml, bool big_endian, int version, std::vector<u8>** output);
EXP void* BymlToText(Byml* byml, std::string** output);

EXP Byml::Type BymlGetType(Byml* byml);

EXP bool BymlFree(Byml* byml);
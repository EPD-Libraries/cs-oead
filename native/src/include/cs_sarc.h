#pragma once

#include <oead/sarc.h>

#include "export.h"

using namespace oead;

EXP void* SarcFromBinary(u8* src, size_t src_len, Sarc** output);
EXP void* SarcToBinary(SarcWriter* writer, u32* alignment, std::vector<u8>** output);

EXP int SarcGetNumFiles(Sarc* sarc);
EXP util::Endianness SarcGetEndianness(Sarc* sarc);
EXP bool SarcGetFile(Sarc* sarc, const char* key, const u8** dst, u32* dst_len);

EXP bool SarcFree(Sarc* sarc, SarcWriter* writer);
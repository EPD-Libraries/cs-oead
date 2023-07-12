#pragma once

#include <oead/sarc.h>

#include "export.h"

using namespace oead;

EXP void* SarcFromBinary(u8* src, size_t src_len, Sarc** output);
EXP void* SarcToBinary(SarcWriter* writer, u32* alignment, std::vector<u8>** output);
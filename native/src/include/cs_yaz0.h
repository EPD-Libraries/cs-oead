#pragma once

#include <oead/yaz0.h>

#include "export.h"

EXP void* Compress(u8* src, u32 src_len, u32 alignment, int level, std::vector<u8>** output);
EXP void* Decompress(u8* src, u32 src_len, u8* dst, u32 dst_len);
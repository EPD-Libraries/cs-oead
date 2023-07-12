#include "export.h"
#include "oead/yaz0.h"

EXP std::vector<u8>* Compress(u8* src, u32 src_len, u32 alignment, int level);
EXP void Decompress(u8* src, u32 src_len, u8* dst, u32 dst_len);
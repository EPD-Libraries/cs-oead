#include "cs_yaz0.h"

std::vector<u8>* Compress(u8* src, u32 src_len, u32 alignment, u32 level) {
  return new auto(oead::yaz0::Compress({src, src_len}, alignment, level));
}

void Decompress(u8* src, u32 src_len, u8* dst, u32 dst_len) {
  oead::yaz0::Decompress({src, src_len}, {dst, dst_len});
}
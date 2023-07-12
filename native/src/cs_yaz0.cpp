#include "cs_yaz0.h"

void* Compress(u8* src, u32 src_len, u32 alignment, int level, std::vector<u8>** output) {
  try {
    *output = new auto(oead::yaz0::Compress({src, src_len}, alignment, level));
  } catch (std::runtime_error ex) {
    return new auto(ex);
  }

  return nullptr;
}

void* Decompress(u8* src, u32 src_len, u8* dst, u32 dst_len) {
  try {
    oead::yaz0::Decompress({src, src_len}, {dst, dst_len});
  } catch (std::runtime_error ex) {
    return new auto(ex);
  }

  return nullptr;
}
#include "cs_sarc.h"

void* SarcFromBinary(u8* src, size_t src_len, Sarc** output) {
  try {
    *output = new Sarc({src, src_len});
  } catch (std::runtime_error ex) {
    return new auto(ex);
  }

  return nullptr;
}

void* SarcToBinary(SarcWriter* writer, u32* alignment, std::vector<u8>** output) {
  try {
    auto result = writer->Write();
    *alignment = result.first;
    *output = new auto(result.second);
  } catch (std::runtime_error ex) {
    return new auto(ex);
  }

  return nullptr;
}
#include "cs_sarc.h"

bool SarcFromBinary(u8* src, int src_len, Sarc** output) {
  try {
    *output = new Sarc({src, src_len});
    return true;
  } catch (std::exception ex) {
    std::cout << ex.what() << std::endl;
    return false;
  }
}

bool SarcToBinary(SarcWriter* writer, std::vector<u8>** output) {}
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

int SarcGetNumFiles(Sarc* sarc) {
  return sarc->GetNumFiles();
}

util::Endianness SarcGetEndianness(Sarc* sarc) {
  return sarc->GetEndianness();
}

bool SarcGetFile(Sarc* sarc, const char* key, const u8** dst, u32* dst_len) {
  auto result = sarc->GetFile(key);
  if (result) {
    *dst = result->data.data();
    *dst_len = result->data.size();
    return true;
  }

  return false;
}

bool SarcFree(Sarc* sarc, SarcWriter* writer) {
  if (reinterpret_cast<std::uintptr_t>(sarc) > -1) {
    delete sarc;
  }

  if (reinterpret_cast<std::uintptr_t>(writer) > -1) {
    delete writer;
  }

  return true;
}
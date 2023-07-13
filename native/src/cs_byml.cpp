#include "include/cs_byml.h"

void* BymlFromBinary(const u8* src, u32 src_len, Byml** output) {
  try {
    *output = new auto(Byml::FromBinary({src, src_len}));
  } catch (std::runtime_error ex) {
    return new auto(ex);
  }

  return nullptr;
}

void* BymlFromText(const char* src, Byml** output) {
  try {
    *output = new auto(Byml::FromText(src));
  } catch (std::runtime_error ex) {
    return new auto(ex);
  }

  return nullptr;
}

void* BymlToBinary(Byml* byml, bool big_endian, int version, std::vector<u8>** output) {
  try {
    *output = new auto(byml->ToBinary(big_endian, version));

  } catch (std::runtime_error ex) {
    return new auto(ex);
  }

  return nullptr;
}

void* BymlToText(Byml* byml, std::string** output) {
  try {
    *output = new auto(byml->ToText());
  } catch (std::runtime_error ex) {
    return new auto(ex);
  }

  return nullptr;
}

Byml::Type BymlGetType(Byml* byml) {
  return byml->GetType();
}

bool BymlFree(Byml* byml) {
  delete byml;
  return true;
}
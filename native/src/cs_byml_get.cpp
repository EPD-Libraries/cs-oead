#include "include/cs_byml_get.h"

Byml::Hash* BymlGetHash(Byml* byml) {
  return &byml->GetHash();
}

Byml::Array* BymlGetArray(Byml* byml) {
  return &byml->GetArray();
}

std::string* BymlGetString(Byml* byml) {
  return new auto(byml->GetString());
}

std::vector<u8>* BymlGetBinary(Byml* byml) {
  return new auto(byml->GetBinary());
}

bool BymlGetBool(Byml* byml) {
  return byml->GetBool();
}

s32 BymlGetInt(Byml* byml) {
  return byml->GetInt();
}

u32 BymlGetUInt(Byml* byml) {
  return byml->GetUInt();
}

f32 BymlGetFloat(Byml* byml) {
  return byml->GetFloat();
}

s64 BymlGetInt64(Byml* byml) {
  return byml->GetInt64();
}

u64 BymlGetUInt64(Byml* byml) {
  return byml->GetUInt64();
}

f64 BymlGetDouble(Byml* byml) {
  return byml->GetDouble();
}
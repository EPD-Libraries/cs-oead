#include "include/cs_byml_set.h"

Byml* BymlSetHash(Byml::Hash* value) {
  return new auto(Byml(*value));
}

Byml* BymlSetArray(Byml::Array* value) {
  return new auto(Byml(*value));
}

Byml* BymlSetString(char* value) {
  return new auto(Byml(std::string(value)));
}

Byml* BymlSetBinary(u8* value, int value_len) {
  return new auto(Byml(std::vector<u8>(value_len, *value)));
}

Byml* BymlSetInt(S32 value) {
  return new auto(Byml(value));
}

Byml* BymlSetUInt(U32 value) {
  return new auto(Byml(value));
}

Byml* BymlSetFloat(F32 value) {
  return new auto(Byml(value));
}

Byml* BymlSetInt64(S64 value) {
  return new auto(Byml(value));
}

Byml* BymlSetUInt64(U64 value) {
  return new auto(Byml(value));
}

Byml* BymlSetDouble(F64 value) {
  return new auto(Byml(value));
}
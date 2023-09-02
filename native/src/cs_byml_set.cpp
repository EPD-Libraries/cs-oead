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

Byml* BymlSetInt(s32 value) {
  return new auto(Byml(Number{value}));
}

Byml* BymlSetUInt(u32 value) {
  return new auto(Byml(Number{value}));
}

Byml* BymlSetFloat(f32 value) {
  return new auto(Byml(Number{value}));
}

Byml* BymlSetInt64(s64 value) {
  return new auto(Byml(Number{value}));
}

Byml* BymlSetUInt64(u64 value) {
  return new auto(Byml(Number{value}));
}

Byml* BymlSetDouble(f64 value) {
  return new auto(Byml(Number{value}));
}
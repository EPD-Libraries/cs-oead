#include "include/cs_byml_array.h"

Byml* BymlArrayGet(Byml::Array* array, int index) {
  return &array->at(index);
}

void BymlArraySet(Byml::Array* array, int index, Byml* value) {
  array->at(index) = *value;
}

void BymlArrayAdd(Byml::Array* array, Byml* value) {
  array->push_back(*value);
}

void BymlArrayRemove(Byml::Array* array, Byml* byml) {
  array->erase(std::remove(array->begin(), array->end(), *byml), array->end());
}

void BymlArrayRemoveAt(Byml::Array* array, int index) {
  array->erase(array->begin() + index);
}

void BymlArrayClear(Byml::Array* array) {
  array->clear();
}

int BymlArrayLength(Byml::Array* array) {
  return array->size();
}

Byml::Array* BymlArrayNew() {
  return new Byml::Array{};
}

bool BymlArrayFree(Byml::Array* array) {
  delete array;
  return true;
}
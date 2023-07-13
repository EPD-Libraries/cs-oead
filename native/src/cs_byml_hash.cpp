#include "include/cs_byml_hash.h"

bool BymlHashGet(Byml::Hash* hash, const char* key, Byml** output) {
  if (hash->contains(key)) {
    *output = &hash->at(key);
    return true;
  }

  return false;
}

void BymlHashAdd(Byml::Hash* hash, const char* key, Byml* value) {
  hash->insert_or_assign(key, *value);
}

void BymlHashRemove(Byml::Hash* hash, const char* key) {
  hash->erase(key);
}

bool BymlHashContainsKey(Byml::Hash* hash, const char* key) {
  return hash->contains(key);
}

void BymlHashClear(Byml::Hash* hash) {
  hash->clear();
}

int BymlHashCount(Byml::Hash* hash) {
  return hash->size();
}

void BymlHashIteratorCurrent(HashIterator* iterator, const char** key, Byml** value) {
  auto it = *iterator;
  *key = it->first.c_str();
  *value = &it->second;
}

bool BymlHashIteratorAdvance(Byml::Hash* hash, HashIterator* iterator, HashIterator** next) {
  if (iterator == NULL) {
    *next = new auto{hash->begin()};
    return true;
  }

  if (++(*iterator) != hash->end()) {
    *next = iterator;
    return true;
  }

  return false;
}

Byml::Hash* BymlHashNew() {
  return new Byml::Hash();
}

bool BymlHashFree(Byml::Hash* hash) {
  delete hash;
  return true;
}

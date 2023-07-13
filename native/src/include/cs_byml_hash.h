#pragma once

#include <oead/byml.h>

#include "export.h"

using namespace oead;

using HashIterator = absl::btree_map<std::string, Byml>::iterator;

EXP bool BymlHashGet(Byml::Hash* hash, const char* key, Byml** output);
EXP void BymlHashAdd(Byml::Hash* hash, const char* key, Byml* value);
EXP void BymlHashRemove(Byml::Hash* hash, const char* key);
EXP bool BymlHashContainsKey(Byml::Hash* hash, const char* key);
EXP void BymlHashClear(Byml::Hash* hash);
EXP int BymlHashCount(Byml::Hash* hash);

EXP void BymlHashIteratorCurrent(HashIterator* iterator, const char** key, Byml** value);
EXP bool BymlHashIteratorAdvance(Byml::Hash* hash, HashIterator* iterator, HashIterator** next);

EXP Byml::Hash* BymlHashNew();
EXP bool BymlHashFree(Byml::Hash* hash);
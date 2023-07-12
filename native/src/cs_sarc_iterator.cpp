#include "cs_sarc_iterator.h"

void SarcCurrent(SarcWriter::FileMap::iterator* iterator, const char** key, std::vector<u8>** output) {
  auto it = *iterator;
  *key = it->first.c_str();
  *output = new auto(it->second);
}

bool SarcAdvance(SarcWriter* writer, SarcWriter::FileMap::iterator* iterator, SarcWriter::FileMap::iterator** next) {
  if (writer->m_files.size() <= 0) {
    return false;
  }

  if (iterator == NULL) {
    *next = new auto{writer->m_files.begin()};
    return true;
  }

  if (++(*iterator) != writer->m_files.end()) {
    *next = iterator;
    return true;
  }

  return false;
}
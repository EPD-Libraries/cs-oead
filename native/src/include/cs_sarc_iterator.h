#pragma once

#include <oead/sarc.h>

#include "export.h"

using namespace oead;

EXP void SarcCurrent(SarcWriter::FileMap::iterator* iterator, const char** key, std::vector<u8>* output);
EXP bool SarcAdvance(SarcWriter* writer, SarcWriter::FileMap::iterator* iterator, SarcWriter::FileMap::iterator** next);
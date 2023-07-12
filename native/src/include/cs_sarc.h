#include <oead/sarc.h>

#include "export.h"

using namespace oead;

EXP bool SarcFromBinary(u8* src, int src_len, Sarc** output);
EXP bool SarcToBinary(SarcWriter* writer, std::vector<u8>** output);
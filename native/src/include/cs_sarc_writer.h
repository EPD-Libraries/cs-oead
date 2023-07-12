#pragma once

#include <oead/sarc.h>

#include "export.h"

using namespace oead;

EXP SarcWriter* SarcWriterNew();
EXP SarcWriter* SarcWriterFromSarc(Sarc* sarc);

EXP int SarcWriterGetNumFiles(SarcWriter* writer);
EXP bool SarcWriterGetFile(SarcWriter* writer, const char* key, const u8** dst, u32* dst_len);
EXP void SarcWriterSetEndianness(SarcWriter* writer, util::Endianness endianness);
EXP void SarcWriterSetMode(SarcWriter* writer, SarcWriter::Mode mode);

EXP void SarcWriterContainsKey(SarcWriter* writer, const char* key);
EXP void SarcWriterAddFile(SarcWriter* writer, const char* key, u8* src, u32 src_len);
EXP void SarcWriterRemoveFile(SarcWriter* writer, const char* key);
EXP void SarcWriterClearFiles(SarcWriter* writer);
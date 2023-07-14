#include "cs_sarc_writer.h"

SarcWriter* SarcWriterNew() {
  return new SarcWriter();
}

SarcWriter* SarcWriterFromSarc(Sarc* sarc) {
  return new auto(SarcWriter::FromSarc(*sarc));
}

int SarcWriterGetNumFiles(SarcWriter* writer) {
  return writer->m_files.size();
}

bool SarcWriterGetFile(SarcWriter* writer, const char* key, const u8** dst, u32* dst_len) {
  if (writer->m_files.contains(key)) {
    auto vec = writer->m_files.at(key);
    *dst = vec.data();
    *dst_len = vec.size();
    return true;
  }

  return false;
}

void SarcWriterSetEndianness(SarcWriter* writer, util::Endianness endianness) {
  writer->SetEndianness(endianness);
}

void SarcWriterSetMode(SarcWriter* writer, SarcWriter::Mode mode) {
  writer->SetMode(mode);
}

bool SarcWriterContainsKey(SarcWriter* writer, const char* key) {
  return writer->m_files.contains(key);
}

void SarcWriterAddFile(SarcWriter* writer, const char* key, u8* src, u32 src_len) {
  writer->m_files.insert_or_assign(key, std::vector<u8>(src, src + src_len));
}

void SarcWriterRemoveFile(SarcWriter* writer, const char* key) {
  writer->m_files.erase(key);
}

void SarcWriterClearFiles(SarcWriter* writer) {
  writer->m_files.clear();
}

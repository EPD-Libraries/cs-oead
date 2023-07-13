cmake_minimum_required(VERSION 3.10)
project(cs_oead VERSION 0.1.0)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_EXTENSIONS OFF)
set(CMAKE_POSITION_INDEPENDENT_CODE ON)
set(CMAKE_SHARED_LIBRARY_PREFIX "")

if("${CMAKE_CXX_COMPILER_ID}" STREQUAL "GNU")
    add_compile_options(-fdiagnostics-color=always)

    if(WIN32)
        set(CMAKE_CXX_STANDARD_LIBRARIES "-static -static-libgcc -static-libstdc++")
    endif()
elseif("${CMAKE_CXX_COMPILER_ID}" STREQUAL "Clang")
    add_compile_options(-fcolor-diagnostics)
endif()

include(CTest)
enable_testing()

add_library(cs_oead SHARED
    ./src/include/cs_byml_array.h
    ./src/cs_byml_array.cpp
    ./src/include/cs_byml_get.h
    ./src/cs_byml_get.cpp
    ./src/include/cs_byml_hash.h
    ./src/cs_byml_hash.cpp
    ./src/include/cs_byml_set.h
    ./src/cs_byml_set.cpp
    ./src/include/cs_byml.h
    ./src/cs_byml.cpp
    ./src/include/cs_sarc_iterator.h
    ./src/include/cs_sarc_writer.h
    ./src/include/cs_sarc.h
    ./src/cs_sarc_iterator.cpp
    ./src/cs_sarc_writer.cpp
    ./src/cs_sarc.cpp
    ./src/include/cs_yaz0.h
    ./src/cs_yaz0.cpp
    ./src/include/export.h
)

if(MSVC)
    target_compile_options(cs_oead PRIVATE /W4 /wd4244 /wd4127 /Zc:__cplusplus)
else()
    target_compile_options(cs_oead PRIVATE -Wall -Wextra -fno-plt)
endif()

add_subdirectory(${PROJECT_SOURCE_DIR}/lib/oead)
target_link_libraries(cs_oead PRIVATE oead)
target_include_directories(cs_oead PRIVATE ./src/include)

set(CPACK_PROJECT_NAME ${PROJECT_NAME})
set(CPACK_PROJECT_VERSION ${PROJECT_VERSION})
include(CPack)

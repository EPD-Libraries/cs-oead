cmake_minimum_required(VERSION 3.10)
project(cs_msbt VERSION 0.1.0)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_EXTENSIONS OFF)
set(CMAKE_POSITION_INDEPENDENT_CODE ON)
set(CMAKE_SHARED_LIBRARY_PREFIX "")

if(WIN32)
    set(CMAKE_CXX_STANDARD_LIBRARIES "-static -static-libgcc -static-libstdc++")
endif()

include(CTest)
enable_testing()

add_library(cs_msbt SHARED
    ./src/include/native.h

    ./src/include/cs_msbt.h
    ./src/cs_msbt.cpp
)

add_subdirectory(${PROJECT_SOURCE_DIR}/lib/msbt)
target_link_libraries(cs_msbt PRIVATE msbt)
target_include_directories(cs_msbt PRIVATE ./src/include)

set(CPACK_PROJECT_NAME ${PROJECT_NAME})
set(CPACK_PROJECT_VERSION ${PROJECT_VERSION})
include(CPack)

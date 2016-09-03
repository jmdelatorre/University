cmake_minimum_required(VERSION 3.5)
project(Tarea01)

set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -std=c++11")

set(SOURCE_FILES
    Programa/src/common/city.c
    Programa/src/common/city.h
    Programa/src/common/city_io.c
    Programa/src/common/color.c
    Programa/src/common/color.h
    Programa/src/judge/checker.c
    Programa/src/judge/checker.h
    Programa/src/judge/judge.c
    Programa/src/judge/reader.c
    Programa/src/judge/reader.h
    Programa/src/solver/solver.c
    Programa/src/solver/stack.c
    Programa/src/solver/stack.h
    Programa/src/watcher/drawer.c
    Programa/src/watcher/drawer.h
    Programa/src/watcher/watcher.c)

add_executable(Tarea01 ${SOURCE_FILES})
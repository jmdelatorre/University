cmake_minimum_required(VERSION 3.5)
project(Tarea03)

set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -std=c++11")

set(SOURCE_FILES
    Programa/obj/helper/multinomial.o
    Programa/obj/helper/primes.o
    Programa/obj/seer/seer.o
    Programa/obj/solver/main.o
    Programa/obj/watcher/watcher.o
    Programa/src/helper/multinomial.c
    Programa/src/helper/multinomial.h
    Programa/src/helper/primes.c
    Programa/src/helper/primes.h
    Programa/src/seer/seer.c
    Programa/src/solver/main.c
    Programa/src/solver/matrix.h
    Programa/src/watcher/watcher.c
    Programa/src/watcher/watcher.h
    Programa/tests/easy/basic
    Programa/tests/easy/limited
    Programa/tests/easy/pebble
    Programa/tests/easy/x
    Programa/tests/hard/disco
    Programa/tests/hard/neon
    Programa/tests/hard/sprinkle
    Programa/tests/hard/steps
    Programa/tests/lunatic/candy
    Programa/tests/lunatic/charmander
    Programa/tests/lunatic/hanabi
    Programa/tests/lunatic/stars
    Programa/tests/normal/checkers
    Programa/tests/normal/crossing
    Programa/tests/normal/flower
    Programa/tests/normal/holy
    Programa/Makefile
    Programa/seer
    Programa/solver
    Programa/watcher_board.pdf
    Programa/watcher_window.pdf
    "Tarea 3.pdf" Programa/src/solver/lista.h)

add_executable(Tarea03 ${SOURCE_FILES})
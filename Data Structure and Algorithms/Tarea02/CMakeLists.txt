cmake_minimum_required(VERSION 3.5)
project(Tarea_2)

set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -std=c++11")

set(SOURCE_FILES
    Programa/assets/materials/billiard.mtl
    Programa/assets/materials/candy.mtl
    Programa/assets/materials/magic.mtl
    Programa/assets/materials/metal.mtl
    Programa/assets/materials/starter.mtl
    Programa/assets/materials/utah-teapot.mtl
    Programa/assets/mesh/bulbasaur.obj
    Programa/assets/mesh/charmander.obj
    Programa/assets/mesh/cube.obj
    Programa/assets/mesh/icosahedron.obj
    Programa/assets/mesh/metal_plate.obj
    Programa/assets/mesh/nordehasoci.obj
    Programa/assets/mesh/plane_y.obj
    Programa/assets/mesh/sphere_ico.obj
    Programa/assets/mesh/squirtle.obj
    Programa/assets/mesh/torus_48_24.obj
    Programa/assets/mesh/utah-teapot.obj
    Programa/assets/mesh/uvsphere.obj
    Programa/assets/textures/10-ball.png
    Programa/assets/textures/8-ball.png
    Programa/assets/textures/bulbasaur_body.png
    Programa/assets/textures/bulbasaur_eyes.png
    Programa/assets/textures/charmander_body.png
    Programa/assets/textures/charmander_eyes.png
    Programa/assets/textures/hexagons.png
    Programa/assets/textures/hexagons_normal.png
    Programa/assets/textures/metal_plate.png
    Programa/assets/textures/metal_plate_normal.png
    Programa/assets/textures/squirtle_body.png
    Programa/assets/textures/squirtle_eyes.png
    Programa/scenes/easy/cube.scene
    Programa/scenes/easy/sphere.scene
    Programa/scenes/easy/teapot.scene
    Programa/scenes/easy/torus.scene
    Programa/scenes/normal/billiards.scene
    Programa/scenes/normal/infinicandy.scene
    Programa/scenes/normal/metallic.scene
    Programa/scenes/normal/starters.scene
    Programa/src/modules/color.h
    Programa/src/modules/geometry.c
    Programa/src/modules/geometry.h
    Programa/src/modules/material.h
    Programa/src/modules/matrices.c
    Programa/src/modules/matrices.h
    Programa/src/modules/parser.c
    Programa/src/modules/parser.h
    Programa/src/modules/randoms.c
    Programa/src/modules/randoms.h
    Programa/src/modules/scene.c
    Programa/src/modules/scene.h
    Programa/src/modules/texture.c
    Programa/src/modules/texture.h
    Programa/src/modules/vectors.c
    Programa/src/modules/vectors.h
    Programa/src/raytracer/raytracer.c
    Programa/src/raytracer/renderer.c
    Programa/src/raytracer/renderer.h
    Programa/src/solution/geometry.h
    Programa/src/solution/manager.c
    Programa/src/solution/manager.h
    Programa/src/solution/scene.h
    Programa/src/solution/vector.h
    Programa/src/structures_test/structures_test.c
    Programa/Makefile
    "Tarea 2.pdf")

add_executable(Tarea_2 ${SOURCE_FILES})
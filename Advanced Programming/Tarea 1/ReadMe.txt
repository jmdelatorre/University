Cosas que Asumi:
No existen movimientos diagonales (ni ataques) y ademas los movimientos solo pueden ser horizontal o vertical no ambos en un mismo turno.
Los ingenieros no pueden curar a clases aereas (no tenia sentido, lo pregunte en el foro y cuando vi la respuesta fue muy tarde :( )


Que Funciona:
Funcionan perfecto los Anti Aereo, Anti Infanteria, Arqueros, Aviones Anti Aereo, Bases, Bombarderos, Ingenieros y los Medicos.
Al decir que funcionan perfecto, me refiero a que atacan debidamente, reciben daño y ocupan sus habilidades Especiales
Las bases tambien funcionan perfectamente
Empezar de nuevo
Ver que equipo gano
El random
Empate cuando se demora demasiado
La Consola, a veces se confunde de equipo y marca una unidad como ejercito del otro equipo. Pero esto es solo en la interfaz, estas unidades nunca atacan a su mismo equipo.
Los ejercitos Aereos pueden estar ubicados en el mismo lugar que una unidad terrestre pero en la consola solo aparece una unidad.
Ubiacan de manera Aleatoria las unidades
Texto indicando el movimiento
Combustible

Que No Funciona:
Los Groupie, Kamikaze, Desmoralizadores no funcionan. 
Podria haberlos puestos pero sin la implementacion de ataque. Preferi quitarlos por completo.
El sorteo para ver quien parte



Como Funciona (Clases y Metodos)
Mi tarea consiste en 16 Clases que explicare a Continuacion

Program:
Pregunta en consola cuantas unidades va a tener cada equipo.
Crea los dos equipos a base de esta informacion
Corre el metodo Atacar de la Clase equipo.
Imprime en pantalla cuando un equipo gana
Empieza de nuevo en el caso que sea requerido

Equipo:
Crea las unidades basandose en la informacion entregada en Consola. Los posiciona de forma aleatoria.
Contiene todos las unidades  de cada equipo y ademas recibe al otro equipo como un objeto para poder atacarlo.
Tiene el metodo atacar, que decide para todas las unidades si se van a mover o atacar y tambien modifica las unidades del otro equipo cuando son atacadas
El metodo atacar Funciona de la siguiente forma.
Para todas las unidades de un equipo, primero revisa si esta vivo o no (para pode hacer un movimiento)
Tambien, revisa si tiene combustible dispoible
Luego con un metodo de "Unidades-AtacaOMueve" Revisa si va a moverse o atcar basandose en la distancia de su posible contricante
Se mueve si esta lejos, lo ataca si esta cerca.
Cuando ataca, revisa primero que unidades es en base a su posicion en el mapa con el metodo Es de Unidades.
Luego lo ataca (o utliza alguna habilidad especial) con los respectivos metodos

Unidades:
El constructor dibuja en consola su ubicacion inicial
Contiene varios metodos.
Es: Ayuda a decidir que unidad es la que se esta atacando.
Atacado: Cuando una unidad es atacado se inicia este metodo. Disminuye el HP de la unidad. Dependiendo de cuanto HP quedo, se decide si muere o si sigue vivo. SI muere se borra del mapa y ademas el boolean se convierte en false.
Posicion Contrincante: Entrega un mapa con todos los posibles contricantes de una sola unidad
Distancia: Saca la distancia entre la unidad y sus contrincantes (utiliza el metodo explicado anteriormente) en modulo. Luego con esta distancia dependiendo del rango de ataque se mueve utilizando el motodo MovimientoMapa.
AtacaOMueve: Dependiendo de la distancia retorna un true o false dependiendo si va a moverse o atacar. Si va a atacar revisa las distancias para ver cuanto le conviene moverse (para no pasarse)
MovimientoMapa: Metodo que mueve una unidad. Las unidades se mueven dependiendo de la distancia en el eje X y en el eje Y. Se mueve por el eje donde hay mas distancia hacia el contrincante
Empujar: Metodo utilizado por las maquinas terrestres anti infanteria para empujar
Recuperar: Meotodo para los ingenierios y medicos para curar
Mejorar: Metodos de los ingenieros para mejorar a las otras unidades

Las Clases Anti Aereo, Anti Infanteria, Arquero, Avion Anti Aereo, base, Bombardero, Desmoralizador, Groupie, Guerrero, Ingeniero, Kamikaze, y Medico  heredan de unidadad.
Estas contienen todos los metodos explicados anteriormente. Ademas, contiene sus atributos especificos (HP, ataque etc)

La Clase Mi random la utilizo para poder usar de mejor manera el random


Que pude hacer mejor:

Los movimientos (utilizando por ej. A*) de esta manera se podian hacer movimientos más inteligentes
Los ataques... utilizo demasaido codigo para atacar ( en especial para saber a quien estoy atacando)
La clase Equipo es hace lo que tiene q hacer, pero en muchas lineas de codigo
Mejorar la visualicion de las unidades en la Consola


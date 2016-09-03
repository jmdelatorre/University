audioreader // Leer una partitura. Entrega la info de la partitura. 

audiowriter // Escribir el archivo wav. Con la informacion de la partitura empieza creando el formato wav. Luego con la clase audiomixer le retorna solo el data de los samples para unirlos al wav final

audiomixer // Retorna de un sample, solo el data

partiturawriter // escribe una partitura

http://csharp.net-tutorials.com/xml/writing-xml-with-the-xmldocument-class/


chunksize = 36 + subchunk2size

subchunk1 = 16
2 canales
bits per sample 16
subchunk2ID = lo copiamos
subchunk2Size = numeroDeSamples (numero de muestra)*numeroDeCanales (2)*bitsPerSample (16) / 8

L0 L1 R0 R1
S0+S1*256=short
offset + ((pos -1 ) / (temp/60)) *Fs (posicion en el muestreo)

0.01*Fs=

los maximos de la muestra, (tanto minimo como maximo) se dividien por el numero mayor, y luego se multiplica por 32688
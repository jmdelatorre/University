using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    [Serializable()]
    public class Jugador
    {
        public int posX;
        public int posY;
        public Action<string> Movimiento;
        public Action<int, int> PosicionInicial;
        public Action<int, int> PosicionLLave;
        public Action<int, int> PosicionPozo;
        public Action<string> mirar;
        public Datos datos;
        public string nombre;
        public Mapa mapa;

        public Jugador(Mapa mapa_, string nombre_)
        {
            nombre = nombre_;
            posX = 5;
            posY = 5;
            mapa = mapa_;


        }
        public Jugador(string nombre_)
        {
            nombre = nombre_;
            posX = 5;
            posY = 5;

        }
        public void crearDatos()
        {
            datos = new Datos(posX, posY, mapa, nombre);
        }
        public void Actualizar()
        {

            PosicionInicial(datos.posX, datos.posY);
        }
        void inicial()
        {
            datos.posX = posX;
            datos.posY = posY;
            Actualizar();
        }

        public void mover(string mensaje)
        {
            if (mensaje == "up")
            {
                if (datos.direccion == "up")
                {
                    if (datos.map.mapaCoords[datos.posX, datos.posY - 1] == null)
                    {
                        datos.posY--;
                        Movimiento(mensaje);
                    }
                    if (datos.map.mapaCoords[datos.posX, datos.posY - 1] == "pozo")
                    {
                        inicial();
                    }
                }
                else
                {
                    datos.direccion = "up";
                    mirar(mensaje);
                }

            }
            if (mensaje == "down")
            {
                if (datos.direccion == "down")
                {
                    if (datos.map.mapaCoords[datos.posX, datos.posY + 1] == null)
                    {
                        datos.posY++;
                        Movimiento(mensaje);
                    }
                    if (datos.map.mapaCoords[datos.posX, datos.posY + 1] == "pozo")
                    {
                        inicial();
                    }
                }
                else
                {
                    datos.direccion = "down";
                    mirar(mensaje);
                }

            }
            if (mensaje == "left")
            {
                if (datos.direccion == "left")
                {
                    if (datos.map.mapaCoords[datos.posX - 1, datos.posY] == null)
                    {
                        datos.posX--;
                        Movimiento(mensaje);
                    }
                    if (datos.map.mapaCoords[datos.posX - 1, datos.posY] == "pozo")
                    {
                        inicial();
                    }
                }
                else
                {
                    datos.direccion = "left";
                    mirar(mensaje);
                }


            }
            if (mensaje == "right")
            {
                if (datos.direccion == "right")
                {
                    if (datos.map.mapaCoords[datos.posX + 1, datos.posY] == null)
                    {
                        datos.posX++;
                        Movimiento(mensaje);
                    }
                    if (datos.map.mapaCoords[datos.posX + 1, datos.posY] == "pozo")
                    {
                        inicial();
                    }
                }
                else
                {
                    datos.direccion = "right";
                    mirar(mensaje);
                }
            }
            if (mensaje == "a")
            {
                if (datos.direccion == "up")
                {
                    if (datos.map.mapaCoords[datos.posX, datos.posY - 1] == "llave")
                    {
                        datos.llave = true;
                    }
                }
                if (datos.direccion == "left")
                {
                    {
                        if (datos.map.mapaCoords[datos.posX - 1, datos.posY] == "llave")
                        {
                            datos.llave = true;
                        }

                    }
                    if (datos.direccion == "right")
                    {
                        {
                            if (datos.map.mapaCoords[datos.posX + 1, datos.posY] == "llave")
                            {
                                datos.llave = true;
                            }

                        }
                        if (datos.direccion == "down")
                        {

                            if (datos.map.mapaCoords[datos.posX, datos.posY + 1] == "llave")
                            {
                                datos.llave = true;
                            }

                        }

                    }
                }
            }
        }
    }






        [Serializable()]
        public class Datos
        {
            public int posX;
            public int posY;
            public Mapa map;
            public List<string> NombreUsuarios = new List<string>();
            public List<string> IPClientes = new List<string>();
            public string direccion;
            public bool llave;

            public Datos(int posX_, int posY_, Mapa map_, string nombre)
            {
                llave = false;
                direccion = "down";
                posX = posX_;
                posY = posY_;
                map = map_;
                agregarNombre(nombre);
            }
            public void agregarIP(string IP)
            {
                IPClientes.Add(IP);
            }
            public void agregarNombre(string nombre)
            {
                NombreUsuarios.Add(nombre);
            }
        }
    }


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreriaT3;

namespace Tarea_3
{
    public class Lista<T> : ILista<T>
    {
        int tamañoLista;


        public Lista() // constructor de la lista 
        {
            tamañoLista = 0;
            primero = null;
        }

        public T this[int index] //permite el acceso a los items de la lista y a setearlos
        {
            get
            {
                T objeto = default(T);
                int n;
                Nodo temp = primero;
                for (n = 0; n <= index && n < tamañoLista;
                     n++, temp = temp.Next)
                {

                    if (n == index) // retorna el objeto en la posicion n
                    {
                        objeto = (T)temp.Objeto;
                    }

                }
                return objeto;

            }
            set
            {
                Nodo temp;
                int i;
                for (temp = primero, i = 0;
                    temp != null && i != index;
                    temp = temp.Next, i++)
                {
                    if (i == index)
                    {
                        temp.Objeto = value; //pone el objeto en la posicion n
                    }
                    temp = temp.Next;
                }
            }
        }

        public int Count
        {
            get { return tamañoLista; } //retorna el tamaño de la lista 
            set { tamañoLista = tamañoLista; }
        }

        public void Add(T item)
        {
            tamañoLista++; //aumenta el tamaño de la lista
            Nodo nodito = new Nodo { Objeto = item }; //crea un nodo con el objeto entregado
            if (primero == null)
            {
                primero = nodito;
            }
            else
            {
                Last.Next = nodito;
            }

        }

        public bool Contains(T item)
        {
            Nodo nodito = new Nodo { Objeto = item };
            bool LoContiene = false;
            Nodo temp = primero;
            for (int i = 0; i < tamañoLista; i++, temp = temp.Next) // revisa los itmes de la lista para ver si contiene el objeto
            {
                if (temp.Objeto.Equals(nodito.Objeto))
                {
                    LoContiene = true;
                    break;
                }
            }
            return LoContiene;
        }

        public int IndexOf(T item)
        {
            Nodo nodito = new Nodo { Objeto = item }; ;
            Nodo temp = primero;
            int index = 0;
            for (int i = 0; i < tamañoLista; i++, index++, temp = temp.Next) //cuando encuentra el pbjeto retorna la posicion donde lo encontro
            {

                if (temp.Objeto.Equals(nodito.Objeto))
                {
                    break;
                }

            }
            return index;
        }

        public bool Remove(T item) //remueve el item de la lista
        {
            bool remove = false;
            Nodo nodito = new Nodo { Objeto = item }; ;
            Nodo temp = primero;

            if (temp.Objeto.Equals(nodito.Objeto))
            {
                temp = temp.Next;
                remove = true;
            }
            else
            {
                for (int i = 0; i < tamañoLista; i++, temp = temp.Next)
                {
                    if (temp.Next.Objeto.Equals(nodito.Objeto))
                    {
                        temp.Next = temp.Next.Next; //remueve un nodo y conecta un nodo anterior con el subsiguiente para que la lista siga unida pero sin el item
                        remove = true;
                        break;
                    }

                }
            }
            if (remove)
            {
                tamañoLista--;
            }
            return remove;

        }


        // cramos los nodos
        public class Nodo
        {
            public Nodo Next;
            public object Objeto;
        }


        private Nodo primero = null;
        public Nodo First // crea el primer nodo
        {
            get
            {
                return primero;
            }
        }
        public Nodo Last // crea el ultimo nodo y ademas lo conecta con el resto 
        {
            get
            {
                Nodo temp = primero;
                if (temp == null)
                {
                    return null;
                }
                while (temp.Next != null)
                {
                    temp = temp.Next;
                }
                return temp;
            }
        }



    }
}


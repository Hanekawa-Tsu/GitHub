using ApiEstructurasDemo.Models;

namespace ApiEstructurasDemo.Data
{
    public static class NodoRepositorio
    {
        private static NodoElemento[] nodos = new NodoElemento[100]; // arreglo fijo
        private static int contador = 0; // lleva control de cuántos nodos hay

        static NodoRepositorio()
        {
            // Inicializamos con dos nodos
            nodos[0] = new NodoElemento { Id = 10, Valor = "Raíz Inicial (ABB)" };
            nodos[1] = new NodoElemento { Id = 5, Valor = "Hijo Izquierdo" };
            contador = 2;
        }

        public static NodoElemento[] ObtenerTodos()
        {
            NodoElemento[] resultado = new NodoElemento[contador];
            for (int i = 0; i < contador; i++)
            {
                resultado[i] = nodos[i];
            }
            return resultado;
        }

        public static bool Insertar(NodoElemento nuevo)
        {
            if (contador >= nodos.Length) return false;
            nodos[contador] = nuevo;
            contador++;
            return true;
        }
    }
}


public class ArbolBinario
{
    public NodoArbol Raiz { get; set; }

    public ArbolBinario()
    {
        Raiz = null;
    }

    public void Agregar(string valor)
    {
        NodoArbol nuevo = new NodoArbol(valor);

        if (Raiz == null)
        {
            Raiz = nuevo;
            return;
        }

        AgregarNodo(Raiz, nuevo);
    }

    private void AgregarNodo(NodoArbol actual, NodoArbol nuevo)
    {
        if (nuevo.Valor.CompareTo(actual.Valor) < 0)
        {
            if (actual.Izquierda == null)
                actual.Izquierda = nuevo;
            else
                AgregarNodo(actual.Izquierda, nuevo);
        }
        else
        {
            if (actual.Derecha == null)
                actual.Derecha = nuevo;
            else
                AgregarNodo(actual.Derecha, nuevo);
        }
    }
}
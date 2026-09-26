public class NodoArbol
{
    public string Valor { get; set; }
    public NodoArbol Izquierda { get; set; }
    public NodoArbol Derecha { get; set; }

    public NodoArbol(string valor)
    {
        Valor = valor;
        Izquierda = null;
        Derecha = null;
    }
}
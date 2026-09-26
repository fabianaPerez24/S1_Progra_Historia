using System.Collections.Generic;

public class Pila
{
    private Stack<string> elementos = new Stack<string>();

    public void Agregar(string elemento)
    {
        elementos.Push(elemento);
    }

    public string Sacar()
    {
        if (elementos.Count == 0)
            return null;

        return elementos.Pop();
    }

    public int Cantidad()
    {
        return elementos.Count;
    }
}
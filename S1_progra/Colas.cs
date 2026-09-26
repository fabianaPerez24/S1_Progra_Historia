using System.Collections.Generic;

public class Cola
{
    private Queue<string> elementos = new Queue<string>();

    public void Agregar(string elemento)
    {
        elementos.Enqueue(elemento);
    }

    public string Sacar()
    {
        if (elementos.Count == 0)
            return null;

        return elementos.Dequeue();
    }

    public int Cantidad()
    {
        return elementos.Count;
    }
}
using System;
using System.Collections.Generic;

public class Nodo
{
    public string Nombre { get; set; }
    public string Descripcion { get; set; }

    public List<Nodo> Opciones { get; set; }
    public Action Accion { get; set; }

    public Nodo(string nombre, string descripcion)
    {
        Nombre = nombre;
        Descripcion = descripcion;
        Opciones = new List<Nodo>();
    }

    public void AgregarOpcion(Nodo nodo)
    {
        Opciones.Add(nodo);
    }

    public void EliminarOpcion(Nodo nodo)
    {
        Opciones.Remove(nodo);
    }
}

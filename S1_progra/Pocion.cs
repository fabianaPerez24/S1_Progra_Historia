using Promedio1.scripts;

internal class Pocion : Item
{
    public int Curacion { get; set; }

    public Pocion(string nombre, string descripcion, int curacion)
        : base(nombre, descripcion)
    {
        Curacion = curacion;
    }
}
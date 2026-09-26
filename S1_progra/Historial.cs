public class Historial
{
    public string Lugar { get; set; }
    public Historial Siguiente { get; set; }

    public Historial(string lugar)
    {
        Lugar = lugar;
        Siguiente = null;
    }
}

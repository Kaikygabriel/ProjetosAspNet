using System.ComponentModel;

namespace AlugAI.Domain.Entities;

public class RentalHouse :Entity
{
    public int Size { get; set; }
    public decimal Price { get; set; }
    public Provider ProviderFromHouse { get; set; } = null!;
    private bool _alugada;

    public bool Alugada
    {
        get
        {
            return _alugada;
        }
        set
        {
            _alugada = value;
        }
    }
}
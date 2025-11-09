using Barbearia.Domain.BackOffice.Entities.Abstraction;
using Barbearia.Domain.BackOffice.Exception;

namespace Barbearia.Domain.BackOffice.Entities;

public class Agend : Entity
{
    public Agend(decimal value, User employee, User customer, DateTime day)
    {
        if (value < 1 || day < DateTime.Now)
            throw new AgendException("Error in parameters of constructor!");
        Value = value;
        Employee = employee;
        Customer = customer;
        Day = day;
    }

    public decimal Value { get; set; }
    public User Employee { get; set; }
    public User Customer { get; set; }
    public DateTime Day { get; set; }
}
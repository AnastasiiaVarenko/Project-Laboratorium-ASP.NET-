namespace Laboratorium_2.Models;

public enum Operators
{
    ADD, SUB, MUL, DIV
}
public class Calculator
{
    public Operators? Operator { get; set; }
    public double? X { get; set; }
    public double? Y { get; set; }

    public String Op
    {
        get
        {
            return Operator switch
            {
                Operators.ADD => "+",
                Operators.SUB => "-",
                Operators.MUL => "*",
                Operators.DIV => "/",
                _ => "",
            };
        }
    }

    public bool IsValid()
    {
        return Operator != null && X != null && Y != null;
    }

    public double Calculate()
    {
        return Operator switch
        {
            Operators.ADD => (double)(X + Y),
            Operators.SUB => (double)(X - Y),
            Operators.MUL => (double)(X * Y),
            Operators.DIV => (double)(X / Y),
            _ => double.NaN,
        };
    }
}
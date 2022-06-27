namespace Calculator;

public abstract class Expression
{
    public abstract double Evaluate(double error);
}

public abstract class BinaryExpression : Expression
{
    protected readonly Expression left;
    protected readonly Expression right;

    public BinaryExpression(Expression left, Expression right)
    {
        this.left = left;
        this.right = right;
    }

    public override double Evaluate(double error)
    {
        double leftValue = this.left.Evaluate(error);
        double rightValue = this.right.Evaluate(error);

        return this.Evaluate(leftValue, rightValue);
    }

    protected abstract double Evaluate(double left, double right);
}

public class Add : BinaryExpression
{
    public Add(Expression left, Expression right) : base(left, right)
    {

    }

    protected override double Evaluate(double left, double right)
    {
        return left + right;
    }

    public override string ToString()
    {
        return $"({left.ToString()}) + ({right.ToString()})";
    }
}

public class Subtract : BinaryExpression
{
    public Subtract(Expression left, Expression right) : base(left, right)
    {

    }

    protected override double Evaluate(double left, double right)
    {
        return left - right;
    }

    public override string ToString()
    {
        return $"({left.ToString()}) - ({right.ToString()})";
    }
}

public class Multiply : BinaryExpression
{
    public Multiply(Expression left, Expression right) : base(left, right)
    {

    }

    protected override double Evaluate(double left, double right)
    {
        return left * right;
    }

    public override string ToString()
    {
        return $"({left.ToString()}) * ({right.ToString()})";
    }
}

public class Divide : BinaryExpression
{
    public Divide(Expression left, Expression right) : base(left, right)
    {

    }

    protected override double Evaluate(double left, double right)
    {
        return left / right;
    }

    public override string ToString()
    {

        return $"({left.ToString()}) / ({right.ToString()})";
    }
}

public class Pow : BinaryExpression
{
    public Pow(Expression left, Expression right) : base(left, right)
    {

    }

    protected override double Evaluate(double left, double right)
    {
        return Math.Pow(left, right);
    }

    public override string ToString()
    {
        return $"({left.ToString()}) ^ ({right.ToString()})";
    }
}

public abstract class UnaryExpression : Expression
{
    protected readonly Expression inner;

    public UnaryExpression(Expression inner)
    {
        this.inner = inner;
    }

    public override double Evaluate(double error)
    {
        return this.Evaluate(this.inner.Evaluate(error), error);
    }

    protected abstract double Evaluate(double inner, double error);
}

public class Sin : UnaryExpression
{
    public Sin(Expression inner) : base(inner)
    {

    }

    protected override double Evaluate(double inner, double error)
    {
        return Aproximation.MacLaurinSin(inner, error);
    }

    public override string ToString()
    {
        return $"sin({inner.ToString()})";
    }
}

public class Cos : UnaryExpression
{
    public Cos(Expression inner) : base(inner)
    {

    }

    protected override double Evaluate(double inner, double error)
    {
        return Aproximation.MacLaurinCos(inner, error);
    }

    public override string ToString()
    {
        return $"cos({inner.ToString()})";
    }
}

public class Tan : UnaryExpression
{
    public Tan(Expression inner) : base(inner)
    {

    }

    protected override double Evaluate(double inner, double error)
    {
        return Aproximation.MacLaurinSin(inner, error) / Aproximation.MacLaurinCos(inner, error);
    }

    public override string ToString()
    {
        return $"tan({inner.ToString()})";
    }
}

public class Cot : UnaryExpression
{
    public Cot(Expression inner) : base(inner)
    {

    }

    protected override double Evaluate(double inner, double error)
    {
        return Aproximation.MacLaurinCos(inner, error) / Aproximation.MacLaurinSin(inner, error);
    }

    public override string ToString()
    {
        return $"cot({inner.ToString()})";
    }
}

public class Sec : UnaryExpression
{
    public Sec(Expression inner) : base(inner)
    {

    }

    protected override double Evaluate(double inner, double error)
    {
        return 1 / Aproximation.MacLaurinCos(inner, error);
    }

    public override string ToString()
    {
        return $"sec({inner.ToString()})";
    }
}

public class Csc : UnaryExpression
{
    public Csc(Expression inner) : base(inner)
    {

    }

    protected override double Evaluate(double inner, double error)
    {
        return 1 / Aproximation.MacLaurinSin(inner, error);
    }

    public override string ToString()
    {
        return $"csc({inner.ToString()})";
    }
}

public class Arcsin : UnaryExpression
{
    public Arcsin(Expression inner) : base(inner)
    {

    }

    protected override double Evaluate(double inner, double error)
    {   
        if (inner > 1 || inner < -1) throw new ArgumentException("Domain error: The x of arcsin(x) must be between [-1, 1]");
        return Aproximation.MacLaurinArcsin(inner, error);
    }

    public override string ToString()
    {
        return $"arcsin({inner.ToString()})";
    }
}
public class Arccos : UnaryExpression
{
    public Arccos(Expression inner) : base(inner)
    {

    }

    protected override double Evaluate(double inner, double error)
    {   
        if (inner > 1 || inner < -1) throw new ArgumentException("Domain error: The x of arccos(x) must be between [-1, 1]");
        return Math.PI / 2 - Aproximation.MacLaurinArcsin(inner, error);
    }

    public override string ToString()
    {
        return $"arccos({inner.ToString()})";
    }
}

public class Arctan : UnaryExpression
{
    public Arctan(Expression inner) : base(inner)
    {

    }

    protected override double Evaluate(double inner, double error)
    {   
        return Aproximation.MacLaurinArctan(inner, error);
    }

    public override string ToString()
    {
        return $"arctan({inner.ToString()})";
    }
}

public class Arccot : UnaryExpression
{
    public Arccot(Expression inner) : base(inner)
    {

    }

    protected override double Evaluate(double inner, double error)
    {   
        return Math.PI / 2 - Aproximation.MacLaurinArctan(inner, error);
    }

    public override string ToString()
    {
        return $"arccot({inner.ToString()})";
    }
}

public class Arcsec : UnaryExpression
{
    public Arcsec(Expression inner) : base(inner)
    {

    }

    protected override double Evaluate(double inner, double error)
    {   
        if (inner < 1 && inner > -1) throw new ArgumentException("Domain error: The x in arcsec(x) must belong to (-inf, -1) U (1, +inf)");
        return Math.PI / 2 - Aproximation.MacLaurinArcsin(1 / inner, error);
    }

    public override string ToString()
    {
        return $"arcsec({inner.ToString()})";
    }
}

public class Arccsc : UnaryExpression
{
    public Arccsc(Expression inner) : base(inner)
    {

    }

    protected override double Evaluate(double inner, double error)
    {   
        if (inner < 1 && inner > -1) throw new ArgumentException("Domain error: The x in arcsc(x) must belong to (-inf, -1) U (1, +inf)");
        return Aproximation.MacLaurinArcsin(1 / inner, error);
    }

    public override string ToString()
    {
        return $"arccsc({inner.ToString()})";
    }
}

public class Constant : Expression
{
    double value;

    public Constant(double value)
    {
        this.value = value;
    }

    public override string ToString()
    {
        return value.ToString();
    }

    public override double Evaluate(double error)
    {
        return this.value;
    }
}

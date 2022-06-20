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
        return Aproximation.TaylorSin(inner, error);
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
        return Aproximation.TaylorCos(inner, error);
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
        return Aproximation.TaylorSin(inner, error) / Aproximation.TaylorCos(inner, error);
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
        return Aproximation.TaylorCos(inner, error) / Aproximation.TaylorSin(inner, error);
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
        return 1 / Aproximation.TaylorCos(inner, error);
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
        return 1 / Aproximation.TaylorSin(inner, error);
    }

    public override string ToString()
    {
        return $"csc({inner.ToString()})";
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

namespace Calculator;

public static class Functions{

    public static double Cos(double x, double error)
    {
        return Aproximation.MacLaurinCos(x, error);
    }

    public static double Sen(double x, double error)
    {
        return Aproximation.MacLaurinSin(x, error);
    }
    public static double Tan(double x, double error)
    {
        return Aproximation.MacLaurinSin(x, error) / Aproximation.MacLaurinCos(x, error);
    }

    public static double Cot(double x, double error)
    {
        return Aproximation.MacLaurinCos(x, error) / Aproximation.MacLaurinSin(x, error);
    }
    
    public static double Sec(double x, double error)
    {
        return 1 / Aproximation.MacLaurinCos(x, error);
    }
    public static double Csc(double x, double error)
    {
        return 1 / Aproximation.MacLaurinSin(x, error);
    }
    public static double Arcsen(double x, double error)
    {
        if (x < -1 || x > 1) throw new ArgumentException("Arcsin domain is [-1, 1]");
        return Aproximation.MacLaurinArcsen(x, error);
    }
    public static double Arccos(double x, double error)
    {   
        if (x < -1 || x > 1) throw new ArgumentException("Arccos domain is [-1, 1]");
        return Math.PI / 2 - Aproximation.MacLaurinArcsen(x, error);
    }
    public static double Arcsec(double x, double error)
    {
        if (x > -1 && x < 1) throw new ArgumentException("Arcsec domain is (-inf, -1] U [1, +inf)");
        return Math.PI / 2 - Aproximation.MacLaurinArcsen(1 / x, error);
    }
    public static double Arccsc(double x, double error)
    {
        if (x > -1 && x < 1) throw new ArgumentException("Arccsc domain is (-inf, -1] U [1, +inf)");
        return Aproximation.MacLaurinArcsen(1 / x, error);
    }

    public static double Arctan(double x, double error)
    {
        return Math.PI / 2 - Arccot(x, error);
    }
    public static double Arccot(double x, double error)
    {   
        if (x > -0.5 && x < 0.5) return Aproximation.MacLaurinArccot(x, error);
        else {
            double temp = Aproximation.MacLaurinArcsen(1 / Math.Sqrt(1 + x * x), error);
            return (x < 0 ? Math.PI - temp : temp);
        }
    }

}
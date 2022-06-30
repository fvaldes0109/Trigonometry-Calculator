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
        return Aproximation.MacLaurinArcsin(x, error);
    }
    public static double Arccos(double x, double error)
    {
        return Math.PI / 2 - Aproximation.MacLaurinArcsin(x, error);
    }
    public static double Arcsec(double x, double error)
    {
        return Math.PI / 2 - Aproximation.MacLaurinArcsin(1 / x, error);
    }
    public static double Arccsc(double x, double error)
    {
        return Aproximation.MacLaurinArcsin(1 / x, error);
    }

    public static double Arctan(double x, double error)
    {
        return Aproximation.MacLaurinArctan(x, error);
    }
    public static double Arccot(double x, double error)
    {
        return Math.PI / 2 - Aproximation.MacLaurinArctan(x, error);
    }

}
namespace Calculator;

public static class Functions{

    public static double Cos(double x)
    {
        return Math.Cos(x);
    }

    public static double Sen(double x)
    {
        return Math.Sin(x);
    }
    public static double Tan(double x)
    {
        if (x == Math.PI/2) throw new Exception("Tan(p/2) is not defined");
        return Math.Tan(x);
    }

    public static double Cot(double x)
    {
        if(x == 0) throw new Exception("Cot(0) is not defined");
        return 1/Math.Tan(x);
    }
    
    public static double Sec(double x)
    {
        return 1/Math.Cos(x);
    }
    public static double Csc(double x)
    {
        return 1/Math.Sin(x);
    }
    public static double Arcsen(double x)
    {
        return Math.Asin(x);
    }
    public static double Arccos(double x)
    {
        return Math.Acos(x);
    }
    public static double Arcsec(double x)
    {
        return Math.PI/2-Math.Asin(1/x);
    }
    public static double Arccsc(double x)
    {
        return Math.Asin(1/x);
    }

    public static double Arctan(double x)
    {
        return Math.Atan(x);
    }
    public static double Arccot(double x)
    {
        return Math.PI/2-Math.Atan(x);
    }

}
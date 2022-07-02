namespace Calculator;

public static class Aproximation {

    ///<summary>
    /// Uses MacLaurin series to calculate the cosine of x
    ///</summary>
    ///<param name ="x">The angle in radians</param>
    ///<param name ="error">The desired aproximation error for the result</param>
    public static double MacLaurinCos(double x, double error) {

        if (error <= 0) throw new ArgumentException("Error must be greater than 0");

        x = FromZeroTo2Pi(x); // Moving x to the interval [0, 2pi]
        double finalSign = 1;

        // Moving x to the first quadrant
        if (x > Math.PI / 2 && x < Math.PI) {
            x = Math.PI - x;
            finalSign = -1;
        }
        else if (x > Math.PI && x < 3 * Math.PI / 2) {
            x = x - Math.PI;
            finalSign = -1;
        }
        else if (x > 3 * Math.PI / 2 && x < 2 * Math.PI) {
            x = 2 * Math.PI - x;
        }

        double result = 0;
        double factorial = 1;
        double xPower = 1;
        double currentValue = 0;

        int i = 0;
        while (true) {

            double term = (1 - 2 * (i % 2)) * xPower / factorial;
            result += term;
            
            if (CheckError(ref result, currentValue, term, error)) break;
            else currentValue = result;

            xPower *= x * x;
            factorial *= (2 * i + 1) * (2 * i + 2);
            i++;
        }

        return result * finalSign;
    }

    ///<summary>
    /// Uses MacLaurin series to calculate the sine of x
    ///</summary>
    ///<param name ="x">The angle in radians</param>
    ///<param name ="error">The desired aproximation error for the result</param>
    public static double MacLaurinSin(double x, double error) {

        if (error <= 0) throw new ArgumentException("Error must be greater than 0");

        x = FromZeroTo2Pi(x); // Moving x to the interval [0, 2pi]
        double finalSign = 1;

        // Moving x to the first quadrant
        if (x > Math.PI / 2 && x < Math.PI) {
            x = Math.PI - x;
        }
        else if (x > Math.PI && x < 3 * Math.PI / 2) {
            x = x - Math.PI;
            finalSign = -1;
        }
        else if (x > 3 * Math.PI / 2 && x < 2 * Math.PI) {
            x = 2 * Math.PI - x;
            finalSign = -1;
        }

        double result = 0;
        double factorial = 1;
        double xPower = x;
        double currentValue = 0;

        int i = 1;
        while (true) {

            double term = (-1 + 2 * (i % 2)) * xPower / factorial;
            result += term;

            if (CheckError(ref result, currentValue, term, error)) break;
            else currentValue = result;

            xPower *= x * x;
            factorial *= (2 * i) * (2 * i + 1);
            i++;
        }

        return result * finalSign;
    }

    ///<summary>
    /// Uses MacLaurin series to calculate the arcsine of x
    ///</summary>
    ///<param name ="x">The angle in radians</param>
    ///<param name ="error">The desired aproximation error for the result</param>
    public static double MacLaurinArcsen(double x, double error) {
        
        if (error <= 0) throw new ArgumentException("Error must be greater than 0");
        if (x == 1 || x == -1) return x * Math.PI / 2;

        double result = 0;
        double factorial2n = 1;
        double factorialn = 1;
        double xPower = x;
        double power4 = 1;
        double currentValue = 0;

        int i = 0;
        while (true) {

            double term = factorial2n / (power4 * factorialn * factorialn * (2 * i + 1)) * xPower;
            result += term;

            if (CheckError(ref result, currentValue, term, error)) break;
            else currentValue = result;

            xPower *= x * x;
            factorialn *= i + 1;
            factorial2n *= (2 * i + 1) * (2 * i + 2);
            power4 *= 4;
            i++;
        }

        return result;
    }

    ///<summary>
    /// Uses MacLaurin series to calculate the arccot of x
    ///</summary>
    ///<param name ="x">The angle in radians</param>
    ///<param name ="error">The desired aproximation error for the result</param>
    public static double MacLaurinArccot(double x, double error) {
        
        if (error <= 0) throw new ArgumentException("Error must be greater than 0");
        if (x > 1 || x < -1) throw new ArgumentException("MacLaurin series for arccot(x) has a convergence interval of (1, -1)");
        if (x == 1 || x == -1) return x * Math.PI / 4;
        
        double result = 0;
        double xPower = x;
        double currentValue = 0;

        int i = 0;
        while (true) {
        
            double term = (1 - 2 * (i % 2)) * xPower / (2 * i + 1);
            result += term;

            if (CheckError(ref result, currentValue, term, error)) break;
            else currentValue = result;

            xPower *= x * x;
            i++;
        }

        return Round(Math.PI / 2 - result, error);
    }

    static double FromZeroTo2Pi(double x) {

        while(x > Math.PI * 2) {
            x -= Math.PI * 2;
        }

        while(x < 0) {
            x += Math.PI * 2;
        }

        return x;
    }

    public static double Round(double numb, double error)
    {
        int count = 0;
        
        while(error < 1)
        {   
            error *= 10;
            count++;
        }

        double rounded = Math.Round(numb, count);

        return (rounded == -0 ? 0 : rounded);
    }

    ///<summary>
    /// Checks if the given error has been reached. If it was, rounds the current value
    ///</summary>
    private static bool CheckError(ref double current, double previous, double term, double error) {

        if (double.IsNaN(current)) {
            System.Console.WriteLine("No se pudo alcanzar el error deseado. Error alcanzado: " + term);
            current = previous;
            return true;
        }

        // The result will be rounded with one extra decimal place, in order to give more accuracy 
        // while calculating a large expression
        if (Math.Abs(term) <= error / (double)10) {
            current = Round(current, error / (double)10);
            return true;
        }
        return false;
    }
}
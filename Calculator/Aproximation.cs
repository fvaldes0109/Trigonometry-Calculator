namespace Calculator;

public static class Aproximation {

    ///<summary>
    /// Uses MacLaurin series to calculate the cosine of x
    ///</summary>
    ///<param name ="x">The angle in radians</param>
    ///<param name ="error">The desired aproximation error for the result</param>
    public static double TaylorCos(double x, double error) {

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

        for (int i = 0; i < 20; i++) {

            result += (1 - 2 * (i % 2)) * xPower / factorial;
            System.Console.WriteLine($"{i}: {result} --- {factorial}");

            xPower *= x * x;
            factorial *= (2 * i + 1) * (2 * i + 2);
        }

        return result * finalSign;
    }

    ///<summary>
    /// Uses MacLaurin series to calculate the sine of x
    ///</summary>
    ///<param name ="x">The angle in radians</param>
    ///<param name ="error">The desired aproximation error for the result</param>
    public static double TaylorSin(double x, double error) {

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

        for (int i = 1; i < 20; i++) {

            result += (-1 + 2 * (i % 2)) * xPower / factorial;
            System.Console.WriteLine($"{i}: {result} --- {factorial}");

            xPower *= x * x;
            factorial *= (2 * i) * (2 * i + 1);
        }

        return result * finalSign;
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
}
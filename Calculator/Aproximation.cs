namespace Calculator;

public static class Aproximation {

    public static double TaylorCos(double x) {

        while(x > Math.PI * 2) {
            x -= Math.PI * 2;
        }

        while(x < -Math.PI * 2) {
            x += Math.PI * 2;
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

        return result;
    }

        public static double TaylorSin(double x) {

        while(x > Math.PI * 2) {
            x -= Math.PI * 2;
        }

        while(x < -Math.PI * 2) {
            x += Math.PI * 2;
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

        return result;
    }
}
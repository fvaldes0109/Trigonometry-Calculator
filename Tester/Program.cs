using Calculator;

// System.Console.WriteLine( Calculate.CalculateInput("(sen(1) + cos(0.01)) / tan(sen(1)) ^ p + arccsc(tan(0.4) + 5) * cot(1) - arcsen(0.3) / e - sec(0.1) * csc(-2.3) + arctan(0.1) ^ arccos(-0.05) - 1 - arccot(0.5-p/6) - arcsec(4 + 1)", 0.000001) );

// System.Console.WriteLine(Calculate.CalculateInput("sen(tan(p/3))", 0.1));
System.Console.WriteLine("Calculadora de expresiones con funciones trigonométricas\n");

while (true) {

    System.Console.WriteLine("Escriba la expresión a calcular");
    string expression = Console.ReadLine()!;
    Console.Write("Escriba el error deseado: ");
    double error = double.Parse(Console.ReadLine()!);

    try {

        double result = Calculate.CalculateInput(expression, error);
        System.Console.WriteLine("Resultado: " + result + "\n");
    }
    catch (System.Exception ex) {
        System.Console.WriteLine(ex);
    }
}
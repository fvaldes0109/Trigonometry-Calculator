namespace Calculator;

public static class Calculate{

    private static double _error;

    ///<summary>
    ///Parse and calculate a string
    ///</summary>
    ///<returns> The value of the expression in double </returns>
    public static double CalculateInput(string input, double error){
        
        _error = error;

        //Dictionary of constants, including numbers
        Dictionary<char, double> constants = new Dictionary<char, double>{
            {'0', 0}, {'1', 1}, {'2', 2}, {'3', 3}, {'4', 4}, {'5', 5}, {'6', 6}, {'7', 7}, {'8', 8}, {'9', 9},
            {'e', Math.Exp(1)}, {'p', Math.PI}
        };

        //Dictionary of operators, with their priority assigned
        Dictionary<string, int> operators = new Dictionary<string, int>{
            {"+", 1} , {"-", 1}, 
            {"*", 2},  {"/", 2}, 
            {"^", 3}, {"root", 3},
            {"cos", 4}, {"sen", 4}, {"cot", 4}, {"tan", 4},  {"csc", 4}, {"sec", 4},
            {"arccos", 4}, {"arcsen", 4}, {"arccot", 4}, {"arctan", 4}, {"arccsc", 4}, {"arcsec", 4},
            {"(", 5}, {")", 5} };

        string[] tokens = Tokenizer(input, constants, operators);

        ValidateBrackets(tokens);
        tokens = NegativeParser(tokens, constants);
        // Print(tokens);
        return Result(tokens, operators, constants);
    }

    private static void Print(string[] tokens)
    {
        for (int i = 0; i < tokens.Length; i++)
        {
            System.Console.Write(tokens[i]);
        }
            System.Console.WriteLine();
    }

    private static string[] NegativeParser(string[] tokens, Dictionary<char, double> constants)
    {
        List<string> TempTokens = new List<string>();

        for (int i = 0; i < tokens.Length; i++)
        {
            if(tokens[i] == "-"){
                if(i!=0 && (double.TryParse(tokens[i-1], out double a) || tokens[i-1] == ")"|| constants.ContainsKey(tokens[i-1][0]))) TempTokens.Add("+"); 
            
                TempTokens.Add("("); TempTokens.Add("0"); TempTokens.Add("-");
                TempTokens.Add("1"); TempTokens.Add(")"); TempTokens.Add("*");
            }

            else TempTokens.Add(tokens[i]);
        }

        return TempTokens.ToArray();
    }


    //Verify if the brackets on the expression are valid
    private static void ValidateBrackets(string[] tokens)
    {
        int count = 0;

        for (int i = 0; i < tokens.Length; i++)
        {
            if(tokens[i] == "(") count++;
            if(tokens[i] == ")") count--;
        }

        if (count != 0) throw new Exception("Brackets are not balance");
    }

    //Calculate de value of the expression
    private static double Result(string[] tokens, Dictionary<string, int> operators, Dictionary<char, double> constants)
    {
       Stack<double> Values = new Stack<double>();
       Stack<string> Ops = new Stack<string>();

       for (int i = 0; i < tokens.Length; i++)
       {
            //If the current token is a number, add it to the stack
            if(!operators.ContainsKey(tokens[i]))
            {
                if(tokens[i] == "e" || tokens[i] == "p")
                Values.Push(constants[tokens[i][0]]);

                else
                Values.Push(double.Parse(tokens[i]));
            }

            else
            {
                //If its an open bracket add it to the stack
                if(tokens[i] == "(")
                {
                Ops.Push(tokens[i]);
                }
                
                //If its a close bracket, calculate the inside's expression and add it to the stack of values
                else if(tokens[i] == ")")
                {
                    while(Ops.Peek() != "(")
                    {
                        if(operators[Ops.Peek()] == 4)
                        Values.Push( applyOp(Ops.Pop(),
                                             Values.Pop()));

                        else
                        Values.Push( applyOp(Ops.Pop(),
                                             Values.Pop(),
                                             Values.Pop()));
                    }

                    Ops.Pop();                   
                }

                else
                {      
                    //If an operator has higher priority that their precedence, apply the operator to their respectives values             
                    while (Ops.Count > 0 && 
                        hasPriority(tokens[i], Ops.Peek(), operators))
                    {
                        if(operators[Ops.Peek()] == 4)
                        Values.Push( applyOp(Ops.Pop(),
                                             Values.Pop()));

                        else
                        Values.Push( applyOp(Ops.Pop(),
                                             Values.Pop(),
                                             Values.Pop()));
                    }

                    Ops.Push(tokens[i]);
                }
            }     
       }


       //The string was fully parsed, apply remaining operators
       while(Ops.Count > 0)
       {
            if(operators[Ops.Peek()] == 4)
            Values.Push( applyOp(Ops.Pop(),
                                    Values.Pop()));

            else
            Values.Push( applyOp(Ops.Pop(),
                                 Values.Pop(),
                                 Values.Pop()));
       }

       return Values.Pop();

    }


    //Return true if the previous operator has higher priority than the current operator
    private static bool hasPriority(string op1, string op2, Dictionary<string, int> operators)
    {
        if(op2 == "(") return false;
        return operators[op2] >= operators[op1];
    }

    public static double applyOp(string op, double b, double a = 0)
    {
        switch (op)
        {
            case "+":
                return a+b;
            case "-":
                return a-b;
            
            case "*":
                return a*b;
            case "/":
                if (b == 0) throw new Exception("Cannot divide by zero");
                 return a/b;
            
            case "^":
                return Math.Pow(a, b);
            case "root":
                return Math.Pow(b, 1/a);
            
            case "cos":
                return Functions.Cos(b, _error);
            case "sen":
                return Functions.Sen(b, _error);
            case "tan":
                return Functions.Tan(b, _error);
            case "cot":
                return Functions.Cot(b, _error);
            case "sec":
                return Functions.Sec(b, _error);
            case "csc":
                return Functions.Csc(b, _error);

            case "arccos":
                return Functions.Arccos(b, _error);
            case "arcsen":
                return Functions.Arcsen(b, _error);
            case "arctan":
                return Functions.Arctan(b, _error);
            case "arccot":
                return Functions.Arccot(b, _error); 
            case "arccsc":
                return Functions.Arccsc(b, _error);
            case "arcsec":
                return Functions.Arcsec(b, _error);
            
            default: return 0;
        }
    }

    ///<summary>
    ///Process the raw string into a set of tokens or cells
    ///</summary>
    ///<returns> A set of tokens in string[] form</returns>

    private static string[] Tokenizer(string input, Dictionary<char, double> constants, Dictionary<string, int> operators)
    {

        List<string> tokens = new List<string>();

        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == ' ') continue;
            

            if(constants.ContainsKey(input[i]))
            {
                string number = "";
                number += input[i];

                while( ((input.Length > i+1 && constants.ContainsKey(input[i+1]) && input[i+1] != 'e' && input[i+1] != 'p'))
                        || (input.Length > i+1 && input[i+1] == '.')){
                    
                    i++;
                    number += input[i];
                }

                tokens.Add(number);
            }

            else if(operators.ContainsKey(input[i].ToString())) tokens.Add(input[i].ToString());
                    

            else if(operators.ContainsKey(input.Substring(i, 3))){
                tokens.Add(input.Substring(i, 3));
                i += 2;
            }

            else if(operators.ContainsKey(input.Substring(i, 4))){
                tokens.Add(input.Substring(i, 3));
                i += 3;
            }


            else if(operators.ContainsKey(input.Substring(i, 6))){ 
                tokens.Add(input.Substring(i, 6));
                i += 5;
            }
            

            else throw new Exception("Incorrect character");
        }

        return tokens.ToArray();

    }
}
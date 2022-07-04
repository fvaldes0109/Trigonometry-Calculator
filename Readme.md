# Proyecto de Análisis Matemático

## Equipo

- Fernando Valdés García C 212
- Loitzel Ernesto Morales Santiesteban C 212
- Naomi Lahera Champagne C 211
- Jorge Alberto Aspiolea González C 212
- David Sánchez Iglesias C 212

## Tema 1

**Objetivo**  
Ofrecer un valor aproximado de funciones trigonométricas a partir de series de potencias.

El presente proyecto consta de 2 partes pricipales: Aproximation y Calculate.

En “Aproximation” se realiza la aproximación, o sea, se convierte la función trigonométrica en una serie numérica, usando la serie de MacLaurin como pauta. La serie de MacLaurin es un caso especial de la serie de Taylor de una función, la diferencia está en que la de MacLaurin se centra en 0, lo que facilita también su cálculo en este caso.
Las funciones trigonométricas pueden ser expresadas en función del seno y el coseno. Sabiendo eso, en este proyecto se implementó la serie de MacLaurin de esas dos funciones, del arco-seno y la de la arco-cotangente, el resto será expresado en función de ellas, por ejemplo, la tangente se calculará como seno/coseno, esto es la serie de MacLaurin del seno dividida por la serie de MacLaurin del coseno. De este modo, las demás funciones trigonométricas son calculadas de la siguiente manera:

- ## $\tan x = \frac{sen x}{\cos x}$

- ## $\cot x = \frac{\cos x}{sen x}$

- ## $\sec x = \frac{1}{\cos x}$
  
- ## $\csc x = \frac{1}{sen x}$

- ## $\arccos x = \frac{\pi}{2} - arcsen x$

- ## $arcsec x = \frac{\pi}{2} - arcsen (\frac{1}{x})$

- ## $arccsc x = arcsen (\frac{1}{x})$, esta propiedad la demostraremos a continuación
  - ### Sea arccsc(x) = $\theta$
  - ### => x = csc($\theta$)
  - ### => x = $\frac{1}{sen(\theta)}$
  - ### => $sen(\theta) = \frac{1}{x}$
  - ### => $\theta = arcsen(\frac{1}{x})$
  - ### => $arccsc(x) = arcsen(\frac{1}{x})$
  
- ## $arctan x = \frac{\pi}{2} - arccot x$

**Serie de MacLaurin:**

## f(x) = f(0) + f'(0)x + $\frac{f''(0)}{2!}x^2$ + ... + $\frac{f^{(n)}(0)}{n!}x^n$ + ... = $\sum_{n=0}^{\infin} \frac{f^{(n)}(0)}{n!}x^n$

## Aproximation

Las series del seno y el coseno fueron implementadas de la siguiente manera:

1. Usando el método: “FromZeroTo2Pi” se toma el ángulo ingresado y se le resta 360º cuantas veces sea necesario hasta que se ubique el en intervalo (${-2\pi, 2\pi}$).

```csharp
static double FromZeroTo2Pi(double x) {
    while(x > Math.PI * 2) {
        x -= Math.PI * 2;
    }
    while(x < 0) {
        x += Math.PI * 2;
    }
    return x;
    }
```

2. Luego se procede a aplicar fórmulas de reducción para llevar el ángulo al 1er cuadrante y facilitar aun más su cálculo.
3. Finalmente se calcula la sumatoria. Esto se hace, primero determinando el sumando, según la fórmula de la serie de MacLaurin, y luego sumándolo con el resultado anterior. En este punto se verifica si el resultado satisface el error seleccionado por el usuario, comprobando si el valor absoluto del nuevo sumando es menor que el error ingresado por el usuario. Si no se cumple esto último, el ciclo se repite con el siguiente sumando.

## cos(x) = $\sum_{n=0}^{\infin} \frac{(-1)^n}{2n!}x^{2n}$

```csharp
while (true) {
    double term = (1 - 2 * (i % 2)) * xPower / factorial;
    result += term; 
    ... 
}
```

## sen(x) = $\sum_{n=1}^{\infin} \frac{(-1)^{n-1}}{(2n - 1)!}x^{2n - 1}$

```csharp
while (true) {
    double term = (-1 + 2 * (i % 2)) * xPower / factorial;
    result += term;
    ...
}
```

En el caso del arco-seno es solamente determinar cada sumando y realizar la suma y la comprobación del error.

## arcsen(x) = $\sum_{n=0}^{\infin} \frac{(2n)!}{4^n(n!)^2(2n + 1)}x^{2n + 1}$

```csharp
while (true) {
    double term = factorial2n / (power4 * factorialn * factorialn * (2 * i + 1)) * xPower;
    result += term;
    ...
}
```

El cálculo de el arco-cotangente no es tan sencillo por ende se dividió en dos casos: si el valor a calcular está entre -0.5 y 0.5 entonces se calcula usando la fórmula de MacLaurin para la arco-cotangente, pero a medida que el parámetro que se pasa como valor de la x está más próximo a 1 o -1, la serie pierde precisión, sin embargo, la arco-cotangente también puede ser expresada en función del arco-seno con una fórmula que solo pierde precisión cuando x se acerca a 0. Por tanto, si |x|>0.5, se expresa la arco-cotangente en función del arco-seno para mayor exactitud en el cálculo.

## $arccot(x) = \frac{\pi}{2} - \sum_{n=0}^{\infin} \frac{(-1)^n}{2n + 1}x^{2n + 1}$ si -0.5 < x < 0.5
## $arccot(x) = arcsen(\frac{1}{\sqrt{1 + x^2}})$ si x < -0.5 o x > 0.5

Nota: Obsérvese que la primera fórmula no es mas que el arco-cotangente expresado en función del  arco-tangente, el cual es calculado utilizando su desarrolo en serie de potencias. A continuación demostraremos esta relación.

### Sea arctan(x) = $\theta$ 
### => x = tan($\theta$)
### => $x = cot(\frac{\pi}{2} - \theta)$
### => $arccot(x) = \frac{\pi}{2} - \theta$
### => $arccot(x) = \frac{\pi}{2} - arctan(x)$

```csharp
while (true) {    
    double term = (1 - 2 * (i % 2)) * xPower / (2 * i + 1);
    result += term;
    ...
}
return Round(Math.PI / 2 - result, error /(double)10);
```

## Calculate

En “Calculate” se transforma la expresión ingresada en el “Tester”, usando notación posfija, en valores y operadores con los cuales poder ejecutar los métodos antes expuestos.

## Bibliografía

- Spivack, Michael: Calculus. Calculo infinitesimal. Página 510
- Artículo: [Expression evaluation](http://geeksforgeeks.org/expression-evaluation)

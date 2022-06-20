/*
Ejercicio: generar un cartón válido para jugar al bingo. Las condiciones son:
1 - Cartón de 3 filas por 9 columnas
2 - El cartón debe tener 15 números y 12 espacios en blanco
3 - Cada fila debe tener 5 números
4 - Cada columna debe tener 1 o 2 números
5 - Ningún número puede repetirse
6 - La primer columna contiene los números del 1 al 9, la segunda del 10 al 19, la tercera del 20 al 29, 
    así sucesivamente hasta la ultima columna la cual contiene del 80 al 90.
*/

var numeros = new int[15];
var random = new Random();
var menorNumero = 1;
var mayorNumero = 10;

for (int i = 0; i < numeros.Length; i++)
{
    var candidato = random.Next(menorNumero, mayorNumero);
   
    if(i > 0)
    {
        bool esRepetido;

        do
        {
            esRepetido = false;
            for (int j = 0; j < i; j++)
            {
                if (candidato == numeros[j])
                    esRepetido = true;
            }
            if (esRepetido)
            {
                candidato = random.Next(menorNumero, mayorNumero);
   
         }
        }
        while (esRepetido);
        do
        {
            var aux = candidato;
            var decenasDelCandidato = 0;
            while (aux >= 10)
            {
                aux -= 10;
                decenasDelCandidato++;
            }

            var numerosEnEstaDecena = 1;
            var decenasDelNumeroExistente = 0;
            for (int j = 0; j < i; j++)
            {
                //Recorro los elementos que tengo, y me fijo que no tenga más de 3 números con la misma
                //cantidad de decenas
                aux = numeros[j];
                while (aux >= 10)
                {
                    aux -= 10;
                    decenasDelNumeroExistente++;
                }
                if (decenasDelNumeroExistente == decenasDelCandidato)
                    numerosEnEstaDecena++;

                decenasDelNumeroExistente = 0;
            }
            if (numerosEnEstaDecena < 3)
            {
                numeros[i] = candidato;
            }
            else
            {
                candidato = random.Next(menorNumero, mayorNumero);
            }
        }
        while (numeros[i] == 0);
    }
    else
    {
        numeros[i] = candidato;
    }

    if(mayorNumero < 90)

    {
        if (menorNumero == 1)
            menorNumero += 9;
        else
            menorNumero += 10;

        mayorNumero = menorNumero + 10;
    }
    else if(menorNumero != 1)
    {
        menorNumero = 1;
    }
}

var carton = new string[9, 3];

for (int i = 0; i < numeros.Length; i++)
{
    //Verifico en qué columna va el número
    var aux = numeros[i];
    var decenas = 0;
    while (aux >= 10)
    {
        aux -= 10;
        decenas++;
    }

    var columna = decenas;
    var filaAleatoria = random.Next(3);

    var cantVueltas = 0;
    do
    {
        //Verificamos que en la fila haya menos de 5 números
        var cantNumerosEnFila = 0;
        for (int j = 0; j < 9; j++)
        {
            if (!string.IsNullOrEmpty(carton[j, filaAleatoria]))
            {
                cantNumerosEnFila++;
            }
        }

        if (string.IsNullOrEmpty(carton[columna, filaAleatoria]) && cantNumerosEnFila < 5)
        {
            carton[columna, filaAleatoria] = numeros[i].ToString().PadLeft(2, '0');
        }
        else
        {
            filaAleatoria = random.Next(3);
        }

        cantVueltas++;
        if(cantVueltas > 1000)
        {
            throw new ApplicationException("Bucle infinito. Mala suerte :(");
        }
    }
    while (string.IsNullOrEmpty(carton[columna, filaAleatoria]) || 
           int.Parse(carton[columna, filaAleatoria]) != numeros[i]);
}
for (int i = 0; i < 9; i++)
{
    for (int j = 0; j < 3; j++)
    {
        if (string.IsNullOrEmpty(carton[i, j]))
        {
            //Relleno con asteriscos
            carton[i, j] = "**";
        }
        else
        {
            //Ordeno
            for (int k = 0; k < j; k++)
            {
                if (string.IsNullOrEmpty(carton[i, k]) || carton[i,k] == "**")
                    continue;

                if (int.Parse(carton[i,j]) < int.Parse(carton[i, k]))
                {
                    var aux = carton[i, j];
                    carton[i, j] = carton[i, k];
                    carton[i, k] = aux;
                }
            }
        }
    }
}

for (int i = 0; i < 3; i++)
{
    for (int j = 0; j < 9; j++)
    {
        if(j == 0)
        {
            Console.Write("|");
        }
        Console.Write(" " + carton[j, i] + " |");
    }
    Console.WriteLine();
}
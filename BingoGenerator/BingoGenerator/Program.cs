/*
Ejercicio: generar un cartón válido para jugar al bingo. Las condiciones son:
1 - Quince números del 1 al 89 inclusive
2 - Un cartón de 9 columnas x 3 filas
3 - Cada columna contiene 1 decena particular. Por ejemplo, la 1ra columna puede contener números del 1 al 9 inclusive,
    la 2da del 10 al 19 inclusive, etc.
4 - 
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

            var numerosEnEstaDecena = 0;
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
            }
            if (numerosEnEstaDecena < 3)
            {
                numeros[i] = candidato;
            }
            else
            {
                candidato = random.Next(1, 90);
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

    do
    {
        if (string.IsNullOrEmpty(carton[columna, filaAleatoria]))
        {
            carton[columna, filaAleatoria] = numeros[i].ToString().PadLeft(2, '0');
        }
        else
        {
            filaAleatoria = random.Next(3);
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
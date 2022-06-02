﻿var numbers = new int[15];
var random = new Random();
var lowestNumber = 1;
var highestNumber = 10;

for (int i = 0; i < numbers.Length; i++)
{
    var candidate = random.Next(lowestNumber, highestNumber);
    if(i > 0)
    {
        bool repeated;

        do
        {
            repeated = false;
            for (int j = 0; j < i; j++)
            {
                if (candidate == numbers[j])
                    repeated = true;
            }
            if (repeated)
            {
                candidate = random.Next(lowestNumber, highestNumber);
            }
        }
        while (repeated);
        do
        {
            var aux = candidate;
            var candidateTens = 0;
            while (aux >= 10)
            {
                aux -= 10;
                candidateTens++;
            }

            var numbersWithinThisTens = 0;
            var numberTens = 0;
            for (int j = 0; j < i; j++)
            {
                //Recorro los elementos que tengo, y me fijo que no tenga más de 3 números con la misma
                //cantidad de decenas
                aux = numbers[j];
                while (aux >= 10)
                {
                    aux -= 10;
                    numberTens++;
                }
                if (numberTens == candidateTens)
                    numbersWithinThisTens++;
            }
            if (numbersWithinThisTens < 2)
            {
                numbers[i] = candidate;
            }
            else
            {
                candidate = random.Next(1, 90);
            }
        }
        while (numbers[i] == 0);
    }
    else
    {
        numbers[i] = candidate;
    }

    if(highestNumber < 90)
    {
        if (lowestNumber == 1)
            lowestNumber += 9;
        else
            lowestNumber += 10;

        highestNumber = lowestNumber + 10;
    }
    else if(lowestNumber != 1)
    {
        lowestNumber = 1;
    }
}

var carton = new string[9, 3];

for (int i = 0; i < numbers.Length; i++)
{
    //Verifico en qué columna va el número
    var aux = numbers[i];
    var tens = 0;
    while (aux >= 10)
    {
        aux -= 10;
        tens++;
    }

    var column = tens;
    var randomRow = random.Next(3);

    do
    {
        if (string.IsNullOrEmpty(carton[column, randomRow]))
        {
            carton[column, randomRow] = numbers[i].ToString().PadLeft(2, '0');
        }
        else
        {
            randomRow = random.Next(3);
        }
    }
    while (string.IsNullOrEmpty(carton[column, randomRow]) || 
           int.Parse(carton[column, randomRow]) != numbers[i]);
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
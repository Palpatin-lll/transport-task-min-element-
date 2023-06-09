﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace transportTaskMinElementConsole
{
    class Program
    {
        static void Main()
        {
            bool repeat = true;
            while (repeat)
            {
                Console.WriteLine("Введите количество складов:");
                int numSources = int.Parse(Console.ReadLine());

                Console.WriteLine("Введите количество розничных магазинов:");
                int numPuti = int.Parse(Console.ReadLine());

                int[,] supplies = new int[numSources, 1];
                int[,] demands = new int[1, numPuti];
                int[,] costs = new int[numSources, numPuti];
                int[,] allocations = new int[numSources, numPuti];

                // Ввод запасов складов
                for (int i = 0; i < numSources; i++)
                {
                    Console.WriteLine($"Введите запас склада №{i + 1}:");
                    supplies[i, 0] = int.Parse(Console.ReadLine());
                }

                // Ввод потребностей магазина
                for (int i = 0; i < numPuti; i++)
                {
                    Console.WriteLine($"Введите кол-во товара нужное в магазин №{i + 1}:");
                    demands[0, i] = int.Parse(Console.ReadLine());
                }

                // Ввод стоимостей перевозки грузов между складом и магазином
                for (int i = 0; i < numSources; i++)
                {
                    for (int j = 0; j < numPuti; j++)
                    {
                        Console.WriteLine($"Введите стоимость перевозки груза от склада {i + 1} до магазина {j + 1}:");
                        costs[i, j] = int.Parse(Console.ReadLine());
                    }
                }

                int source = 0;
                int dest = 0;
                while (source < numSources && dest < numPuti)
                {
                    int minCost = int.MaxValue;

                    //Поиск минимальной ячейки стоимоси в текущей строке
                    for (int j = 0; j < numPuti; j++)
                    {
                        if (costs[source, j] < minCost && demands[0, j] > 0)
                        {
                            minCost = costs[source, j];
                            dest = j;
                        }
                    }

                    // Если не нашли ячейку с отрицательным коэффициентом, переходим к следующей строке
                    if (minCost == int.MaxValue)
                    {
                        source++;
                        continue;
                    }

                    // По другому находим количество груза, которое можно перевезти
                    int quantity = Math.Min(supplies[source, 0], demands[0, dest]);
                    allocations[source, dest] = quantity;

                    // Уменьшаем оставшиеся запасы и потребности
                    supplies[source, 0] -= quantity;
                    demands[0, dest] -= quantity;

                    // Если запас выбранного склада закончился, переходим к следующему 
                    if (supplies[source, 0] == 0)
                        source++;
                }

                // Выводим результат
                Console.WriteLine("Оптимальный план:");
                for (int i = 0; i < numSources; i++)
                {
                    for (int j = 0; j < numPuti; j++)
                    {
                        Console.Write($"{allocations[i, j]}\t");
                    }
                    Console.WriteLine();
                }

                // Считаем общую стоимость
                int totalCost = 0;
                for (int i = 0; i < numSources; i++)
                {
                    for (int j = 0; j < numPuti; j++)
                    {
                        totalCost += allocations[i, j] * costs[i, j];
                    }
                }

                Console.WriteLine($"Общая стоимость перевозки: {totalCost}");
                Console.ReadLine();
            }
            Console.Write("Хотите ли вы запустить алгоритм еще раз? (да/нет): ");
            string repeatChoice = Console.ReadLine();
            repeat = repeatChoice.ToLower() == "да";
        }
    }
}

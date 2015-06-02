using System;
using System.Collections.Generic;
using AED;

namespace AED_LAB_ARVBIN
{
    class Program
    {
        static void Main(string[] args)
        {
            //Recebendo o arquivo do úsuario
            Console.Write("===============================================================================\n");
            Console.Write("                                 Bem Vindo!\n");
            Console.Write("===============================================================================\n\n\n");
            Console.Write("Favor digitar o caminho do arquivo que deseja ler: ");
            string arquivoLeitura = Console.ReadLine();

            //Limpeza de Tela
            Console.Clear();

            Console.ReadKey();
        }
    }
}

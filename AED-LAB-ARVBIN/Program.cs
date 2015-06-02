using System;
using System.Collections.Generic;
using AED;

namespace AED_LAB_ARVBIN
{
    class Program
    {
        static void Main(string[] args)
        {
            CArvBin arvBin = new CArvBin();

            //Recebendo o arquivo do úsuario
            Console.Write("===============================================================================\n");
            Console.Write("                                 Bem Vindo!\n");
            Console.Write("===============================================================================\n\n\n");
            Console.Write("Favor digitar o diretório do arquivo que deseja ler: ");
            string arquivoLeitura = Console.ReadLine();

            //Passa como parâmetro o caminho do arquivo a ser utilizado.
            arvBin.RecebeDiretorioArquivo(arquivoLeitura);

            //Limpeza de Tela
            Console.Clear();

            Console.ReadKey();
        }
    }
}

using System;
using System.IO;
using System.Collections;

namespace AED
{
    #region Classes CNo e CArvBin - Árvore Binária
    class CNo
    {
        public int item;
        public CNo Esq, Dir;

        public CNo(int valorItem)
            : this(valorItem, null, null)
        {
        }

        public CNo(int valorItem, CNo NoEsq, CNo NoDir)
        {
            item = valorItem;
            Esq = NoEsq;
            Dir = NoDir;
        }

    }

    class CArvBin
    {
        string ArquivoLeitura;
        StreamReader arquivoLeitura;
        StreamWriter arquivoEscrita = new StreamWriter("Resultado.txt", true);

        int Quant = 0; // controla quantidade de elementos na árvore
        int Maior = -1; // controla qual o menor elemento contido na árvore
        int Menor = 999; // controla qual o maior elemento contido na árvore
        int QtdNoFolha = 0; // controla quantos elementos da árvore são Nós Folha
        int QtdNoInterno = 0; // controla quantos elementos da árvore são Nós Internos

        private CNo Raiz;

        public CArvBin()
        {
            Raiz = null;
        }

        public void Inserir(int item)
        {
            Insere(item, ref Raiz);
        }

        private void Insere(int item, ref CNo no)
        {
            // insere um item na arvore a partir da raiz
            if (no == null) // se estiver vazia
            {
                no = new CNo(item);
                no.Esq = no.Dir = null; // inic. filhos
            }
            else // se a arvore nao esta vazia
            {
                // se for menor
                if (item < no.item)
                    Insere(item, ref no.Esq); // tentar a esq.
                else
                    // se for maior
                    if (item > no.item)
                        Insere(item, ref no.Dir); // tentar a esq.
                    else
                    {
                        Console.WriteLine("Elemento ja existente!!!\n");
                    }
            }
        }

        // Percorre toda a estrutura para que no fim informe a quantidade de nós ela possui
        public int QuantidadeElementos(CNo no)
        {
            if (no == null) // se a árvore contém nó
                return 0;
            else
                Quant = 1 + QuantidadeElementos(no.Esq) + QuantidadeElementos(no.Dir); // Inicia pelos filhos da raíz, sendo assim soma +1 para contar com ela
            return Quant;
        }

        // Verifica qual nó possui o maior item
        public int MaiorElemento(CNo no)
        {
            if (no != null) // se a árvore contém nó 
            {
                if (Maior < no.item) // verifica se o valor da variável é menor que o valor do item
                    Maior = no.item;
                this.MaiorElemento(no.Esq);
                this.MaiorElemento(no.Dir);
                return Maior;
            }
            else
            {
                return 0;
            }

        }

        // Verifica qual nó possui o menor item
        public int MenorElemento(CNo no)
        {
            if (no != null) // se a árvore contém nó
            {
                if (Menor > no.item) // verifica se o valor da variável é menor que o valor do item
                    Menor = no.item;
                this.MenorElemento(no.Esq);
                this.MenorElemento(no.Dir);
                return Menor;
            }
            else
            {
                return 0;
            }

        }

        // Percorre toda a estrutura para que no fim informe a quantidade de nós folhas ela possui
        public int QuantidadeNoFolha(CNo no)
        {
            if (no != null)
            {
                if (no.Esq != null || no.Dir != null) // Verifica se o nó possui filhos, pois se não possuir ele é um nó folha
                {
                    QuantidadeNoFolha(no.Esq);
                    QuantidadeNoFolha(no.Dir);
                }
                else
                    QtdNoFolha++;
            }
            else
                return 0;
            return QtdNoFolha;
        }

        // Percorre toda a estrutura para que no fim informe a quantidade de nós internos ela possui
        public int QuantidadeNoInterno(CNo no)
        {
            if (no != null)
            {
                if (no.Esq != null || no.Dir != null) // Verifica se o nó possui filhos, pois se possuir ele é um nó interno
                {
                    QuantidadeNoInterno(no.Esq);
                    QtdNoInterno++;
                    QuantidadeNoInterno(no.Dir);
                }
                else
                    return 0;
            }
            else
                return 0;
            return QtdNoInterno; // +1 para incluir o nó raiz
        }

        // Controla sentido que irá percorrer a escritura dos detalhes dos nós
        public void DetalheNo(CNo no)
        {
            if (no != null)
            {
                arquivoEscrita.WriteLine(); // Quebra de linha
                EscreveDetalheNo(no); // Método recursivo que escreve no arquivo
                DetalheNo(no.Esq);
                DetalheNo(no.Dir);
            }
        }

        public int AlturaNo(CNo no)
        {
            int x, y;

            //Se No for nulo retorna -1
            if (no == null)
                return -1;

            x = AlturaNo(no.Esq);
            y = AlturaNo(no.Dir);

            if (x > y)
                return x + 1;
            else
                return y + 1;
        }

        // Escreve no arquivo os detalhes dos nós no sentido Pré Ordem
        private void EscreveDetalheNo(CNo no)
        {
            int QtdFilhos = 0;
            int FilhoEsq = 0;
            int FilhoDir = 0;
            if (no.Esq != null)
            {
                QtdFilhos++;
                FilhoEsq = no.Esq.item;
            }
            if (no.Dir != null)
            {
                QtdFilhos++;
                FilhoDir = no.Dir.item;
            }
            arquivoEscrita.WriteLine("Nó: " + no.item);
            arquivoEscrita.WriteLine("Qtde. filhos: " + QtdFilhos);
            arquivoEscrita.WriteLine("Filho esquerda: " + (FilhoEsq > 0 ? FilhoEsq.ToString() : "Não possui"));
            arquivoEscrita.WriteLine("Filho direira: " + (FilhoDir > 0 ? FilhoDir.ToString() : "Não possui"));
            arquivoEscrita.WriteLine("Nó interno ou folha?: " + (QtdFilhos > 0 ? "Interno" : "Folha"));
            arquivoEscrita.WriteLine("Altura e Profundidade do nó: " + AlturaNo(no) + " ");
        }

        public void Imprimir(int Ordem)
        {
            // Ordem = 1 => EmOrdem
            // Ordem = 2 => PreOrdem
            // Ordem = 3 => PosOrdem     
            switch (Ordem)
            {
                case 1: Console.WriteLine("\n\nImpressao em ordem\n");
                    EmOrdem(Raiz);
                    break;
                case 2: Console.WriteLine("\n\nImpressao pre ordem\n");
                    PreOrdem(Raiz);
                    break;
                case 3: Console.WriteLine("\n\nImpressao pos ordem\n");
                    PosOrdem(Raiz);
                    break;
                default: Console.WriteLine("Opcao invalida!!!\n\n");
                    break;
            }
        }

        private void EmOrdem(CNo no)
        {
            // percorre em ordem crescente
            if (no != null)
            {
                EmOrdem(no.Esq);
                Console.WriteLine("Chave = {0}\n", no.item);
                EmOrdem(no.Dir);
            }
        }

        //Impressão dos nós internos em Ordem no Arquivo passado por parametro
        private void EmOrdemNoInternoArquivo(CNo no)
        {
            // percorre em ordem crescente
            if ((no != null) && (no.Esq != null || no.Dir != null))
            {
                EmOrdemNoInternoArquivo(no.Esq);
                arquivoEscrita.Write(no.item + " ");
                EmOrdemNoInternoArquivo(no.Dir);
            }
        }

        //Impressão dos nós folhas em Ordem no Arquivo passado por parametro
        private void EmOrdemNoFolhaArquivo(CNo no)
        {
            // percorre em ordem crescente
            if (no != null)
            {

                EmOrdemNoFolhaArquivo(no.Esq);
                if (no.Esq == null && no.Dir == null)
                    arquivoEscrita.Write(no.item + " ");
                EmOrdemNoFolhaArquivo(no.Dir);
            }
        }

        private void PreOrdem(CNo no)
        {
            // percorre previlegiando a raiz sobre 
            // seus descendentes
            if (no != null)
            {
                Console.WriteLine("Chave = {0}\n", no.item);
                PreOrdem(no.Esq);
                PreOrdem(no.Dir);
            }
        }

        private void PosOrdem(CNo no)
        {
            // percorre previlegiando os descendentes 
            // sobre a raiz 
            if (no != null)
            {
                PosOrdem(no.Esq);
                PosOrdem(no.Dir);
                Console.WriteLine("Chave = {0}\n", no.item);
            }
        }

        public Boolean Achou(int itemProc)
        {
            // marcar o inicio (entra na arvore pela raiz)
            CNo no = Raiz;
            bool achou = false;

            // enquanto houver dados e nao achar o elemento
            while (no != null && no.item != itemProc)
            {
                // se for menor, avancar a esquerda,
                // se for maior, avancar a direita
                if (itemProc < no.item)
                    no = no.Esq;
                else
                    no = no.Dir;
            }
            // se nao achou, retorna FALSE, se achou, TRUE
            achou = no != null ? true : false;
            return achou;
        }

        public void Retira(int item)
        {
            CNo NoAnt = Raiz, NoRetira = Raiz;
            Remove(NoAnt, NoRetira, NoAnt, NoRetira, item);
        }

        private void Remove(CNo Aux, CNo AuxRaiz, CNo NoAnt, CNo NoRetira, int item)
        {
            if (AuxRaiz != null) //Testa se a árvore não esta nula...
            {

                if (item < AuxRaiz.item) /*Se o item é menor significa que o elemento procurado esta a esquerda
                                          * da atual AuxRaiz */
                {
                    Aux = AuxRaiz;
                    AuxRaiz = AuxRaiz.Esq;
                    Remove(Aux, AuxRaiz, NoAnt, NoRetira, item);
                }
                else
                    if (item > AuxRaiz.item)/*Se o item é maior significa que o elemento procurado esta a direita
                                             * da atual AuxRaiz */
                    {
                        Aux = AuxRaiz;
                        AuxRaiz = AuxRaiz.Dir;
                        Remove(Aux, AuxRaiz, NoAnt, NoRetira, item);
                    }
                    else //Encontrou o elemento procurado...
                    {
                        RemoveItem(Aux, AuxRaiz);
                    }

            }
            else//Não existe o elemento que está tentando remover...
                Console.WriteLine("Não existe este elemento na arvore!");
        }

        /// <summary>Método que retira um elemento da arvore binaria
        /// Método que elimina um elemento da arvore considerando as referencias que lhe são
        /// passados por parametro.
        /// 
        /// Ps.: Usa o algoritmo de remocao da sub-arvore a direita, o elemento mais a esquerda.
        /// </summary>
        /// <param name="AuxAnt">
        /// Variavel que contem a referencia do no anterior ao no que sera removido.
        /// </param>
        /// <param name="raiz">
        /// Variavel que contem a referencia do no que sera removido.
        /// </param>
        private void RemoveItem(CNo AuxAnt, CNo raiz)
        {

            if ((raiz.Dir == null) && (raiz.Esq == null))//Testa se o no que sera removido é no folha.
            {
                if (raiz.item > AuxAnt.item)
                    AuxAnt.Dir = null;
                else
                    AuxAnt.Esq = null;
            }
            else
            {
                if (raiz.Dir == null)//Testa se o no so tem um filho pela esquerda
                {
                    if (raiz.item > AuxAnt.item)
                        AuxAnt.Dir = raiz.Esq;
                    else
                        AuxAnt.Esq = raiz.Esq;
                }
                else
                {
                    if (raiz.Esq == null)//testa se o no so tem um filho pela direita
                    {
                        if (raiz.item > AuxAnt.item)
                            AuxAnt.Dir = raiz.Dir;
                        else
                            AuxAnt.Esq = raiz.Dir;
                    }
                    else
                    {
                        CNo Aux2 = raiz;
                        AuxAnt = raiz;
                        raiz = raiz.Dir;//noRaiz da subarvore
                        if (raiz.Esq == null)//testa se o no da sub-arvore tem filhos a esquerda
                        {
                            AuxAnt.Dir = raiz.Dir;
                        }
                        else
                        {
                            while (raiz.Esq != null)//busca o elemento mais a esquerda
                            {
                                Aux2 = raiz;
                                raiz = raiz.Esq;
                            }

                            AuxAnt.item = raiz.item;
                            if (raiz.Dir != null)//Testa se a direita do elemento mais a esquerda tem elementos
                                Aux2.Esq = raiz.Dir;
                            else
                            {
                                if (raiz.item > Aux2.item)
                                    Aux2.Dir = null;
                                else
                                    Aux2.Esq = null;
                            }
                        }
                    }
                }
            }
        }

        //Recebe o caminho do arquivo passado pelo usuário e lê o arquivo solicitado pelo usuário
        public void RecebeDiretorioArquivo(string ArqUsuario)
        {
            this.ArquivoLeitura = ArqUsuario;
            arquivoLeitura = new StreamReader(ArquivoLeitura);
            ArmazenarNumeroArquivo();
        }

        //Armazenar os números do arquivo informado pelo usuário criando uma árvore binária
        public void ArmazenarNumeroArquivo()
        {
            string linha = "";

            //Preenchendo o Árvore Binária com os números do arquivo             
            while (linha != null)
            {
                linha = arquivoLeitura.ReadLine();
                if (linha != null)
                {
                    int numero = int.Parse(linha);
                    Inserir(numero);
                }
            }
            arquivoLeitura.Close();
            ImprimirArquivo();
        }

        //Imprime o Arquivo de Resultados
        public void ImprimirArquivo()
        {
            arquivoEscrita.WriteLine("Nó raiz: " + Raiz.item);
            arquivoEscrita.WriteLine("Quantidade de nós da árvore: " + QuantidadeElementos(Raiz));
            arquivoEscrita.WriteLine("O valor do maior nó da árvore: " + MaiorElemento(Raiz));
            arquivoEscrita.WriteLine("O valor do menor nó da árvore: " + MenorElemento(Raiz));
            arquivoEscrita.WriteLine("A quantidade de nós folha da árvore: " + QuantidadeNoFolha(Raiz));
            arquivoEscrita.WriteLine("A quantidade de nós internos (isso inclui o nó raiz): " + QuantidadeNoInterno(Raiz));
            arquivoEscrita.Write("A impressão dos nós folha no percurso em-ordem: "); EmOrdemNoFolhaArquivo(Raiz); arquivoEscrita.WriteLine();
            arquivoEscrita.Write("A impressão dos nós internos no percurso em-ordem: "); EmOrdemNoInternoArquivo(Raiz);
            // Quebras de linhas
            arquivoEscrita.WriteLine();
            arquivoEscrita.WriteLine();
            arquivoEscrita.WriteLine("Informações dos nós (Pré-ordem): "); // Falta altura, produndidade do nó e fator de balanceamento
            arquivoEscrita.WriteLine("===================================");
            DetalheNo(Raiz);

            Console.Clear();
            Console.Write("==============================================================================\n");
            Console.Write("|            Impressão realizada com sucesso!                                |");
            Console.Write("|                               Favor verificar o arquivo Resultado.txt      |");
            Console.Write("==============================================================================\n");

            arquivoEscrita.Close();
        }
    }
    #endregion
}

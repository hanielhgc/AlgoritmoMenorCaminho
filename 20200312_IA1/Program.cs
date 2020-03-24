using System;
using System.Collections.Generic;
using System.Linq;

namespace _20200312_IA1
{
    public class Program
    {
        static void Main(string[] args)
        {



            int?[,] matrizAdjacencia = new int?[,] { { null, 2, 4, 2, null, null,null,null,null,null },
                                                     { null, null, null, null, 7, 4, 6, null,null,null},
                                                     { null, null, null, null, 3, 2, 4, null, null, null },
                                                     { null, null, null, null, 4, 1, 5, null, null, null},
                                                     { null, null, null, null, null, null, null, 1, 4, null},
                                                     { null, null, null, null, null, null, null, 6, 3, null},
                                                     { null, null, null, null, null, null, null, 3, 3, null},
                                                     { null, null, null, null, null, null, null, null, null, 3},
                                                     { null, null, null, null, null, null, null, null, null, 4},
                                                     { null, null, null, null, null, null, null, null, null, null}

            };

            Console.WriteLine("Matriz de Adjacência:");
            ImprimirMatriz(matrizAdjacencia);


            //O estado final é J. Obtendo os estados que levam à J

            int linhaAtual = Constantes.estados.Count;


            while (true)
            {

                if (linhaAtual == 0)
                {
                    break;
                }

                List<int> estadosAdjacentes = new List<int>();
                int? menorValor = int.MaxValue;

                Caminho caminho = new Caminho();

                for (int i = 0; i < Constantes.estados.Count; i++)
                {
                    int? ConteudoEstado = matrizAdjacencia[i, linhaAtual];

                    if (ConteudoEstado != null)
                    {
                        estadosAdjacentes.Add(i);
                        Console.WriteLine("Estado " + Constantes.estados[i] + " é adjacente.");

                        if (ConteudoEstado < menorValor)
                        {
                            menorValor = ConteudoEstado;
                            caminho.Custo = ConteudoEstado.Value;
                            caminho.Estado = Constantes.estados[i];
                        }
                    }

                }


                Console.WriteLine("Melhor caminho: " + caminho.Estado + " de custo " + caminho.Custo);
                Console.WriteLine("-------");
                linhaAtual = Constantes.estados.IndexOf(caminho.Estado);

            }


            //Console.WriteLine("\n Segunda parte - Hill Climbing: \n");

            ////segunda parte (hill climbing)

            //int linhaAtual2 = 0;

            //while (true)
            //{

            //    if (linhaAtual2 >= Constantes.estados.Count) {
                    
            //        break;
                
            //    }

            

            //List<int> intervalo = new List<int>();
            //Caminho caminho2 = new Caminho();

            //for (int i = 0; i < Constantes.estados.Count; i++)
            //{

            //    if (matrizAdjacencia[linhaAtual2, i]!=null)
            //    {

            //        intervalo.Add(i);
            //    }

            //}

            //    Random r = new Random();
            //int rInt = r.Next(intervalo.First(), intervalo.Last()+1); //for ints

            //Console.WriteLine("Caminho escolhido: "+Constantes.estados[rInt]+" de custo "+matrizAdjacencia[linhaAtual2, rInt]);

            //    linhaAtual2 +=1;

            //}


        }

        public static void ImprimirMatriz(int?[,] matriz)
        {
            int valor;

            var rowCount = matriz.GetLength(0);
            var colCount = matriz.GetLength(1);
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount; col++)

                    if (matriz[row, col] == null)
                    {
                        Console.Write("null \t");
                    }
                    else
                    {
                        Console.Write(String.Format("{0}\t", matriz[row, col]));
                    }

                Console.WriteLine();
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;

namespace HillClimbing
{
    public class Program
    {
        static void Main(string[] args)
        {



            /*  int?[,] matrizAdjacencia = new int?[,] { { null, 2, 4, 2, null, null,null,null,null,null },
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
   */


            //Criando arquivo de texto

            TextWriter tw;
            string path = @"C:\Users\Haniel\Source\Repos\AlgoritmoMenorCaminho\HillClimbing\saida.txt";
            if (!File.Exists(path))
            {
                File.Create(path);
                tw = new StreamWriter(path);
                tw.Close();
            }
            else if (File.Exists(path))
            {
         //       tw = new StreamWriter(path, true);

            }

            tw = new StreamWriter(path, true);

            String input = File.ReadAllText(@"C:\Users\Haniel\Source\Repos\AlgoritmoMenorCaminho\HillClimbing\entrada.txt");

            int i = 0, j = 0;
            int?[,] matrizAdjacencia = new int?[10,10];
            foreach (var row in input.Split('\n'))
            {
                j = 0;
                foreach (var col in row.Trim().Split(' '))
                {
                    matrizAdjacencia[i, j] = ToNullableInt(col.Trim());
                    j++;
                }
                i++;
            }


            tw.WriteLine("Matriz de Adjacência:");
            ImprimirMatriz(matrizAdjacencia);


            //O estado final é J. Obtendo os estados que levam à J

            int linhaAtual = Constantes.estados.Count;

            tw.WriteLine("\nGerando primeiro caminho aleatório: \n");


            List<Caminho> resultado = new List<Caminho>();

            while (true)
            {

                if (linhaAtual == 0)
                {
                    break;
                }

                List<int> estadosAdjacentes = new List<int>();
                int? menorValor = int.MaxValue;

                Caminho caminho = new Caminho();


                List<Caminho> caminhos = new List<Caminho>();


                for (int m = 0; m < Constantes.estados.Count; m++)
                {
                    int? ConteudoEstado = matrizAdjacencia[m, linhaAtual];



                    if (ConteudoEstado != null)
                    {
                        estadosAdjacentes.Add(m);
                        tw.WriteLine("Estado " + Constantes.estados[m] + " é adjacente.");

                        caminho.Custo = ConteudoEstado.Value;
                        caminho.Estado = Constantes.estados[m];


                        caminhos.Add(ObjectExtensions.Copy(caminho));
                    }

                }

                Random random = new Random();
                int index = random.Next(0, caminhos.Count);



                caminho = caminhos[index];

                resultado.Add(caminho.Copy());


                tw.WriteLine("Caminho Escolhido: " + caminho.Estado + " de custo " + caminho.Custo);
                tw.WriteLine("-------");
                linhaAtual = Constantes.estados.IndexOf(caminho.Estado);

            }


            //Criando a lista de perturbações


            tw.WriteLine("Experimentando 20 novos resultados.");
            for (int k = 0; k < 20; k++)
            {

                linhaAtual = Constantes.estados.Count;

                List<Caminho> perturbacoes = new List<Caminho>();

                while (true)
                {

                    if (linhaAtual == 0)
                    {
                        break;
                    }

                    List<int> estadosAdjacentes = new List<int>();
                    int? menorValor = int.MaxValue;

                    Caminho caminho = new Caminho();


                    List<Caminho> caminhos = new List<Caminho>();


                    for (int o = 0; o < Constantes.estados.Count; o++)
                    {
                        int? ConteudoEstado = matrizAdjacencia[o, linhaAtual];



                        if (ConteudoEstado != null)
                        {
                            estadosAdjacentes.Add(o);
                            //        tw.WriteLine("Estado " + Constantes.estados[i] + " é adjacente.");

                            caminho.Custo = ConteudoEstado.Value;
                            caminho.Estado = Constantes.estados[o];


                            caminhos.Add(ObjectExtensions.Copy(caminho));
                        }

                    }

                    Random random = new Random();
                    int index = random.Next(0, caminhos.Count);



                    caminho = caminhos[index];

                    perturbacoes.Add(caminho.Copy());


                    //                    tw.WriteLine("Caminho Escolhido: " + caminho.Estado + " de custo " + caminho.Custo);
                    //                  tw.WriteLine("-------");
                    linhaAtual = Constantes.estados.IndexOf(caminho.Estado);

                }


                //tw.WriteLine("Resultado: "+GetCustoTotal(resultado));
                //tw.WriteLine("Resultado perturbado: "+GetCustoTotal(perturbacoes));


                //Perturbando o resultado
                tw.WriteLine("Experimentando perturbações.");
                List<Caminho> novoResultado = new List<Caminho>();

                for (int n = 0; n < resultado.Count; n++)
                {
                    if (RandomBoolean())
                    {
                        novoResultado.Add(resultado[n]);
                    }
                    else
                    {
                        novoResultado.Add(perturbacoes[n]);
                    }
                }


                //Comparando o resultado atual com o perturbado
                if (GetCustoTotal(novoResultado) > GetCustoTotal(resultado))
                {
                    resultado = novoResultado;
                    tw.WriteLine("Encontrado novo caminho de melhor desempenho.");
                }
                else
                {
                    tw.WriteLine("Resultado anterior permanece.");
                }

            }



            tw.WriteLine("Resultado final:");
            for (int l = 0; l < resultado.Count; l++)
            {
                tw.WriteLine("Caminho: " + resultado[l].Estado + " de custo " + resultado[l].Custo);
                tw.WriteLine("-------");
            }


            tw.Close();



        }

        public static int GetCustoTotal(List<Caminho> percurso)
        {
            int count = 0;

            for (int i = 0; i < percurso.Count; i++)
            {
                count += percurso[i].Custo;
            }

            return count;
        }

        public static int? ToNullableInt(string s)
        {
            int i;
            if (int.TryParse(s, out i)) return i;
            return null;
        }

        public static bool RandomBoolean()
        {
            Random random = new Random();

            return random.Next() > (Int32.MaxValue / 2);
            // Next() returns an int in the range [0..Int32.MaxValue]
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

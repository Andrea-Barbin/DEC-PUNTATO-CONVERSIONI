using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarbinConvDecPuntato
{
    internal class Program
    {

        //INIZIO
        static void Main(string[] args)
        {
            //DICHIARAZIONE VARIABILI
            int[] dp = new int[4];
            bool[] b;
            bool ottValido;

            //CICLO FOR PER L'INSERIMENTO DEI 4 OTTETTI
            for (int i = 0; i < dp.Length; i++)
            {
                do
                {
                    do
                    {
                        Console.WriteLine($"Inserire il {i + 1}° ottetto:");
                        ottValido = int.TryParse(Console.ReadLine(), out dp[i]);

                        if (!ottValido)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Errore, l'ottetto è errato\n");
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                    } while (!ottValido);

                    //SEGNALAZIONE DI ERRORE IN CASO DI IP ERRATO
                    if (!IpCorretto(dp))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Errore, l'indirizzo IP non è stato inserito nel formato corretto.\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                } while (!IpCorretto(dp));

            }

            //OUTPUT INDIRIZZO IP IN DEC PUNTATO
            Console.WriteLine($"\nL'indirizzo IP inserito in DECIMALE PUNTATO è:");

            for (int i = 0; i < dp.Length; i++)
            {
                if (i != dp.Length - 1)
                {
                    Console.Write($"{dp[i]}.");
                }
                else
                {
                    Console.Write(dp[i]);
                }

            }

            Console.WriteLine();

            //CONVERSIONE DELL'INDIRIZZO IN BIN
            b = Convert_dp_to_binario(dp);


            //OUTPUT INDIRIZZO IP IN BIN
            Console.WriteLine($"\nL'indirizzo IP inserito in BINARIO è:");

            for (int i = 0; i < b.Length; i++)
            {
                if (b[i])
                {
                    Console.Write('1');
                }
                else
                {
                    Console.Write('0');
                }

                if (i == 7 || i == 15 || i == 23)
                {
                    Console.Write('.');
                }
            }
            Console.WriteLine();
            Console.WriteLine($"\nIl contenuto del corrispondente array di bool è:");

            foreach (bool ott in b)
            {
                Console.WriteLine(ott);

            }

            Console.ReadLine();

        }

        //METODO CHE VERIFICA LA CORRETTEZZA DI OGNI SINGOLO OTTETTO
        static bool IpCorretto(int[] dp)
        {
            bool corretto = true;


            //CICLO FOR PER VERIFICARE CHE I VALORI DEGLI OTTETI SIANO VALIDI
            for (int i = 0; i < dp.Length; i++)
            {
                if (Convert.ToInt32(dp[i]) < 0 || Convert.ToInt32(dp[i]) > 255)
                {
                    corretto = !corretto;
                }
            }

            return corretto;
        }

        //METODO CONVERSIONE DA DECIMALE PUNTATO A BINARIO
        static bool[] Convert_dp_to_binario(int[] dp)
        {
            bool[] b = new bool[32];
            int resto, lunghBin = 8, aggiungiZeri, c;
            string valoreConv = "";

            //CICLO FOR PER SCORRERE L'ARRAY CONTENENTE GLI OTTETTI
            for (int i = 0; i < dp.Length; i++)
            {
                //DO-WHILE PER LA CONVERSIONE IN BINARIO DELL'OTTETTO CORRENTE
                do
                {
                    resto = dp[i] % 2;
                    dp[i] = dp[i] / 2;
                    valoreConv = resto + valoreConv;

                } while (dp[i] != 0);

                //ISTRUZIONE DI SELEZIONE PER VERIFICARE SE E' NECESSARIO AGGIUNGERE ZERI PRIMA DEL VALORE BINARIO OTTENUTO
                if (valoreConv.Length < lunghBin)
                {
                    aggiungiZeri = lunghBin - valoreConv.Length;
                    for (int j = 0; j < aggiungiZeri; j++)
                    {
                        valoreConv = '0' + valoreConv;
                    }
                }

                c = 0;

                /*CICLO FOR CHE ASSEGNA ALLE CELLE DEL VETTORE BOOL (32 perchè in 4 ottetti ci sono 32 bit) 
                IL VALORE CORRISPONDENTE AD OGNI SINGOLA CIFRA CONTENUTA NELLA STRINGA valoreConv*/


                for (int j = valoreConv.Length * i; j < valoreConv.Length * (i + 1); j++)
                {
                    if (valoreConv[c] == '1')
                    {
                        b[j] = true;
                    }
                    c++;
                }

                valoreConv = "";
            }

            return b;

        }



        //static int[] Convert_binario_to_decimale_puntato(bool[] b)
        //{

        //    //return 0;
        //}


        //static int[] Convert_dp_to_intero(int[] dp)
        //{

        //}

        //static int[] Convert_binario_to_intero(bool[] bn)
        //{

        //}

        //FINE

    }
}

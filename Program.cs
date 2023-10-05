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
            int[] decPuntato2;
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
                            Console.WriteLine("Errore, è necessario inserire un'ottetto.\n");
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                    } while (!ottValido);

                    //SEGNALAZIONE DI ERRORE IN CASO DI IP ERRATO
                    if (!IpCorretto(dp))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Errore, l'ottetto non è stato inserito nel formato corretto.\n");
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
            Console.WriteLine($"\nL'indirizzo IP inserito convertito in BINARIO è:");

            for (int i = 0; i < b.Length; i++)
            {
                Console.Write(Convert.ToInt32(b[i]));
                if (i == 7 || i == 15 || i == 23)
                {
                    Console.Write('.');
                }
            }

            Console.WriteLine();
            Console.WriteLine($"\nIl contenuto del corrispondente array di bool, ossia b, è:");

            foreach (bool ott in b)
            {
                Console.WriteLine(ott);
            }

            //CONVERSIONE DA BIN A DEC PUNTATO
            decPuntato2 = Convert_binario_to_decimale_puntato(b);

            //OUTPUT INDIRIZZO IP IN DEC PUNTATO 
            Console.WriteLine();
            Console.WriteLine($"\nL'indirizzo IP convertito DA BIN A DEC PUNTATO è:");

            for (int i = 0; i < decPuntato2.Length; i++)
            {
                if (i != decPuntato2.Length - 1)
                {
                    Console.Write($"{decPuntato2[i]}.");
                }
                else
                {
                    Console.Write(decPuntato2[i]);
                }
            }


            //CONVERSIONE DA DEC PUNTATO A DECIMALE
            Console.WriteLine();
            Console.WriteLine($"\nL'indirizzo IP convertito da DEC PUNTATO a DEC è: {Convert_dp_to_intero(dp)}");

            //CONVERSIONE DA BIN A DECIMALE

            Console.WriteLine();
            Console.WriteLine($"\nL'indirizzo IP convertito da BIN a DEC è: {Convert_binario_to_intero(b)}");


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
            int resto, lunghBin = 8, aggiungiZeri, c, valoreCorrente;
            string valoreConv = "";

            //CICLO FOR PER SCORRERE L'ARRAY CONTENENTE GLI OTTETTI
            for (int i = 0; i < dp.Length; i++)
            {
                /*Copio il valore della cella corrente dell'array dp nella variabile valoreCorrente da usare per la conversione, 
                in modo tale da evitare che con le divisioni successive si azzerri il contenuto di dp[i].*/

                valoreCorrente = dp[i];

                //DO-WHILE PER LA CONVERSIONE IN BINARIO DELL'OTTETTO CORRENTE
                do
                {
                    resto = valoreCorrente % 2;
                    valoreCorrente = valoreCorrente / 2;
                    valoreConv = resto + valoreConv;

                } while (valoreCorrente != 0);

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

                /*CICLO FOR CHE ASSEGNA ALLE CELLE DEL VETTORE BOOL (32 perchè in 4 byte ci sono 32 bit) 
                IL VALORE CORRISPONDENTE AD OGNI SINGOLA CIFRA CONTENUTA NELLA STRINGA valoreConv*/

                /*Essendo che i bit vanno da 0 a 31, significa che: 
                il primo byte inizia al bit 0 e finisce al bit 7,
                il secondo byte inizia al bit 8 e finisce al bit 15,
                il terzo byte inizia al bit 16 e finisce al bit 23,
                il quarto byte inizia al bit 24 e finisce al bit 31.*/

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

        //METODO CONVERSIONE DA BIN A DEC PUNTATO

        static int[] Convert_binario_to_decimale_puntato(bool[] b)
        {
            int[] valoriDecPuntato = new int[4];
            int c = 0, pesoCifra = 8;

            //CICLO FOR PER SCORRERE TUTTO L'ARRAY b CONTENENTE I VALORI BOOLEANI CORRISPONDENTI ALLA CONVERSIONE DEI VALORI IN DEC PUNTATO
            for (int i = 0; i < b.Length; i++)
            {

                /*Partendo da sinistra, i pesi delle cifre in un byte vanno da 7 a 0, quindi man mano che si moltiplica una cifra 
                di un bit per la base elevata al peso procedendo da sinistra a destra, occorre decrementare il peso.*/

                pesoCifra--;
                valoriDecPuntato[c] += Convert.ToInt32(b[i]) * (int)Math.Pow(2, pesoCifra);

                /*Dato che i bit nell'array di bool sono numerati da 0 a 31 e che ogni byte è formato a 8 bite (8 celle dell'array), 
                il primo byte inizia alla posizione 0, il secondo alla posizione 8, il terzo alla posizione 16 e il quarto alla posizione 24)*/

                if (i == 7 || i == 15 || i == 23)
                {
                    c++;
                }

                /*Quando il peso cifra assume valore zero, cioè quando il programma ha convertito un byte, 
                allora il peso della cifra deve tornare a 8 in modo tale da puntare al primo bit del byte successivo*/

                if (pesoCifra == 0)
                {
                    pesoCifra = 8;
                }

            }

            return valoriDecPuntato;
        }

        //METODO CONVERSIONE DA DEC PUNTATO A DEC
        static int Convert_dp_to_intero(int[] dp)
        {
            int ipDec = 0;

            //CICLO FOR PER SCORRERE TUTTI I BYTE IN DEC PUNTATO CONTENUTI NELL'ARRAY dp
            for (int i = 0; i < dp.Length; i++)
            {
                //Alla variabile ipDec viene sommato il valore risultato dalla conversione del byte corrente secondo la formula: sommatoria di N * 256 ^ peso
                ipDec += dp[i] * (int)Math.Pow(256, dp.Length - 1 - i);
            }

            return ipDec;
        }

        //METODO CONVERSIONE DA BIN A DEC
        static int Convert_binario_to_intero(bool[] bn)
        {
            int ipDecFromBin = 0;

            //CICLO FOR PER SCORRERE L'ARRAY bn PER LA CONVERSIONE

            for (int i = bn.Length - 1; i >= 0; i--)
            {
                //Alla variabile ipDec viene sommato il valore risultato dalla conversione del byte corrente secondo la formula: sommatoria di N * 2 ^ peso
                ipDecFromBin += Convert.ToInt32(bn[i]) * (int)Math.Pow(2, bn.Length - 1 - i);
            }

            return ipDecFromBin;
        }

        //FINE
    }
}


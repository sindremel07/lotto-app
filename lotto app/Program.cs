using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LottoConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int forsok = 0;

            int[] lottoLapp = new int[7];
            int[] lottoLappUnsorted = new int[7];

            Console.WriteLine("Generer Random Lotto Lapp: 1 | Skriv Din Egen (7 Tall): 2");
            string answer = Console.ReadLine();

            if (answer == "1")
            {
                for (int i = 0; i < lottoLapp.Length; i++)
                {
                    int nyttTall = random.Next(1, 35);

                    if (FinnNummer(nyttTall, lottoLapp))
                    {
                        i--;
                    }
                    else
                    {
                        lottoLapp[i] = nyttTall;
                        lottoLappUnsorted[i] = nyttTall;
                    }
                }
            }
            else if (answer == "2")
            {
                Console.WriteLine("Skriv Din Egen Lotto Lapp, Det MÅ være 7 UNIKE tall og være mellom 1-35");
                HashSet<int> tallSett = new HashSet<int>();

                for (int i = 0; i < 7; i++)
                {
                    bool gyldigTall = false;

                    while (!gyldigTall)
                    {
                        Console.WriteLine($"Skriv tall nummer {i + 1}:");
                        string input = Console.ReadLine();

                        if (int.TryParse(input, out int tall))
                        {
                            if (tall < 1 || tall > 35)
                            {
                                Console.WriteLine("Tallet må være mellom 1 og 35.");
                            }
                            else if (!tallSett.Add(tall))
                            {
                                Console.WriteLine("Tallet er allerede skrevet. Du må skrive et unikt tall.");
                            }
                            else
                            {
                                lottoLapp[i] = tall;
                                lottoLappUnsorted[i] = tall;
                                gyldigTall = true;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Du må skrive et gyldig tall.");
                        }
                    }
                }
            }
            else if (answer != "1" && answer != "2")
            {
                Console.WriteLine("Ikke et gyldig valg.");
                return;
            }

            for (; ; )
            {
                forsok++;

                int[] lottoTall = new int[7];
                for (int i = 0; i < lottoTall.Length; i++)
                {
                    int nyttTall = random.Next(1, 35);

                    if (FinnNummer(nyttTall, lottoTall))
                    {
                        i--;
                    }
                    else
                    {
                        lottoTall[i] = nyttTall;
                    }
                }

                if (ArraySjekk(lottoLapp, lottoTall))
                {
                    Console.WriteLine("\n\n----------------------------------------------------------");
                    Console.WriteLine("\n\nDet tok " + forsok + " forsøk for å få riktig lottolapp!");
                    Console.WriteLine("\n\nDin lottolapp: " + string.Join(", ", lottoLappUnsorted));
                    Console.WriteLine("Systemets lottotall: " + string.Join(", ", lottoTall));
                    break;
                }
            }
        }

        public static bool FinnNummer(int userNum, int[] lottoNum)
        {
            foreach (int num in lottoNum)
            {
                if (num == userNum)
                    return true;
            }
            return false;
        }

        public static bool ArraySjekk(int[] array1, int[] array2)
        {
            if (array1.Length != array2.Length)
                return false;

            Array.Sort(array1);
            Array.Sort(array2);

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                    return false;
            }
            return true;
        }
    }
}
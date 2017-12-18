using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tor_selden_csharp
{
    class Day18
    {
        static string[] input = File.ReadAllLines(Path.Combine(Program.BasePath, "input18.txt"));
        static Dictionary<string, long> registers = new Dictionary<string, long>();
        static long lastPlayedSound = 0;
        static long currentCommand = 0;

        internal static void A()
        {
            InitializerRegisters();

            bool done = false;

            while (!done)
            {
                //for (long i = 0; i < input.Length; i++)
                //foreach (var item in input)
                var command = input[currentCommand].Split(new[] { ' ' });

                switch (command[0])
                {
                    case "snd":
                        Snd(command[1]);
                        break;
                    case "set":
                        Set(command[1], command[2]);
                        break;
                    case "add":
                        Add(command[1], command[2]);
                        break;
                    case "mul":
                        Mul(command[1], command[2]);
                        break;
                    case "mod":
                        Mod(command[1], command[2]);
                        break;
                    case "rcv":
                        done = Rcv(command[1]);
                        break;
                    case "jgz":
                        Jgz(command[1], command[2]);
                        break;
                    default:
                        throw new ExecutionEngineException();
                }
            }
            Console.WriteLine(lastPlayedSound);
        }

        private static void Jgz(string x, string y)
        {
            long valueX = 0;
            long valueY = 0;

            if (!long.TryParse(x, out valueX))
                valueX = registers[x];

            if (!(valueX > 0))
            {
                currentCommand++;
                return;
            }

            if (!long.TryParse(y, out valueY))
                valueY = registers[y];

            currentCommand += valueY;
        }

        private static bool Rcv(string x)
        {
            currentCommand++;
            if (!(long.TryParse(x, out long value)))
                value = registers[x];

            if (value > 0)
            {
                //Console.WriteLine(lastPlayedSound);
                return true;
            }
            return false;
        }

        private static void Mod(string x, string y)
        {
            currentCommand++;
            long value = 0;

            if (long.TryParse(y, out value))
            {
                registers[x] = registers[x] % value;
                return;
            }
            registers[x] = registers[x] % registers[y];
        }

        private static void Mul(string x, string y)
        {
            currentCommand++;
            long value = 0;

            if (long.TryParse(y, out value))
            {
                registers[x] *= value;
                return;
            }
            registers[x] *= registers[y];
        }

        private static void Add(string x, string y)
        {
            currentCommand++;
            long value = 0;

            if (long.TryParse(y, out value))
            {
                registers[x] += value;
                return;
            }
            registers[x] += registers[y];
        }

        private static void Set(string x, string y)
        {
            currentCommand++;
            long value = 0;

            if (long.TryParse(y, out value))
            {
                registers[x] = value;
                return;
            }
            registers[x] = registers[y];
        }

        private static void Snd(string v)
        {
            currentCommand++;
            lastPlayedSound = registers[v];
        }

        private static void InitializerRegisters()
        {
            //a,i,p,b,f
            registers.Add("a", 0);
            registers.Add("b", 0);
            registers.Add("f", 0);
            registers.Add("i", 0);
            registers.Add("p", 0);
        }
    }
}

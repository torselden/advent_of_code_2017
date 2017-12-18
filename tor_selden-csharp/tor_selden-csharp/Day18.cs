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
        static Dictionary<string, int> registers = new Dictionary<string, int>();
        static int lastPlayedSound = 0;
        static int globalLastPlayedSound = 0;
        static int currentCommand = 0;

        internal static void A()
        {
            InitializerRegisters();

            while (true)
            {
            //for (int i = 0; i < input.Length; i++)
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
                        Rcv(command[1], command[2]);
                        break;
                    case "jgz":
                        Jgz(command[1], command[2]);
                        break;
                    default:
                        throw new ExecutionEngineException();
                }
            }
        }

        private static void Jgz(string x, string y)
        {
            int valueX = 0;
            int valueY = 0;

            if (!int.TryParse(x, out valueX))
                valueX = registers[x];

            if (!(valueX > 0))
                return;
            
            if (!int.TryParse(y, out valueY))
                valueY = registers[y];

            currentCommand += valueY;

        }

        private static void Rcv(string v1, string v2)
        {
            currentCommand++;
            if (lastPlayedSound != 0)
                globalLastPlayedSound = lastPlayedSound;

        }

        private static void Mod(string x, string y)
        {
            currentCommand++;
            int value = 0;

            if (int.TryParse(y, out value))
            {
                registers[x] = registers[x] % value;
                return;
            }
            registers[x] = registers[x] % registers[y];
        }

        private static void Mul(string x, string y)
        {
            currentCommand++;
            int value = 0;

            if (int.TryParse(y, out value))
            {
                registers[x] *= value;
                return;
            }
            registers[x] *= registers[y];
        }

        private static void Add(string x, string y)
        {
            currentCommand++;
            int value = 0;

            if (int.TryParse(y, out value))
            {
                registers[x] += value;
                return;
            }
            registers[x] += registers[y];
        }

        private static void Set(string x, string y)
        {
            currentCommand++;
            int value = 0;

            if (int.TryParse(y, out value))
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace tor_selden_csharp
{
    class Prog
    {
        public Dictionary<string,long> Registers { get; set; }
        public Queue<string> MsgQueue { get; set; }
        public int SentMessages { get; set; }
        public int RecievedMessages { get; set; }
        public int CurrentCommand { get; set; }
    }

    class Day18
    {
        static string[] input = File.ReadAllLines(Path.Combine(Program.BasePath, "input18.txt"));
        static Dictionary<string, long> registers = new Dictionary<string, long>();
        static long lastPlayedSound;
        static long currentCommand;

        public static void B()
        {
            var prog0 = new Prog() {Registers = InitializerRegisters(0)};
            var prog1 = new Prog() {Registers = InitializerRegisters(1)};

            do
            {
                
            } while (prog0.MsgQueue.Count>0 && prog1.MsgQueue.Count > 0);
            
        }

        internal static void A()
        {
            registers = InitializerRegisters(0);

            bool done = false;
            while (!done)
            {
                done = ParseCommand();
            }
            Console.WriteLine(lastPlayedSound);
        }

        public static bool ParseCommand()
        {
            var command = input[currentCommand].Split(new[] {' '});

            bool done = false;
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
                    throw new Exception();
            }
            return done;
        }

        private static void Jgz(string x, string y)
        {
            if (!long.TryParse(x, out var valueX))
                valueX = registers[x];

            if (!(valueX > 0))
            {
                currentCommand++;
                return;
            }

            if (!long.TryParse(y, out var valueY))
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
                return true;
            }
            return false;
        }

        private static void Mod(string x, string y)
        {
            currentCommand++;

            if (long.TryParse(y, out var value))
            {
                registers[x] = registers[x] % value;
                return;
            }
            registers[x] = registers[x] % registers[y];
        }

        private static void Mul(string x, string y)
        {
            currentCommand++;

            if (long.TryParse(y, out var value))
            {
                registers[x] *= value;
                return;
            }
            registers[x] *= registers[y];
        }

        private static void Add(string x, string y)
        {
            currentCommand++;

            if (long.TryParse(y, out var value))
            {
                registers[x] += value;
                return;
            }
            registers[x] += registers[y];
        }

        private static void Set(string x, string y)
        {
            currentCommand++;

            if (long.TryParse(y, out var value))
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

        public static Dictionary<string, long> InitializerRegisters(int startValue)
        {
            Dictionary<string, long> registers = new Dictionary<string, long>();

            registers.Add("a", 0);
            registers.Add("b", 0);
            registers.Add("f", 0);
            registers.Add("i", 0);
            registers.Add("p", startValue);

            return registers;
        }

    }
}

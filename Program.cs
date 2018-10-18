using System;
using Newtonsoft.Json;

namespace pafenet
{
    class Program
    {
        static void Main(string[] args)
        {
            var dump = SFDumpRaw.DumpISFBytes(0x0003, 0x090f);
            if (dump == null)
            {
                Console.WriteLine($"Error ocurred.");
                return;
            }

            Console.WriteLine($"IDm: {dump.IDm}");
            foreach (var block in dump.Blocks)
            {
                for (int i = 0; i < block.Length; i++) {
                    Console.Write($"{block[i]:X02}");
                }
                Console.Write("\n");
            }
            Console.WriteLine(JsonConvert.SerializeObject(dump));
        }
    }
}

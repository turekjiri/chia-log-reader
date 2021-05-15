using System;

namespace ChiaLogReader.App
{
    class Program
    {
        static void Main(string[] args)
        {
            new App.App(@"D:\Desktop\Nouveau dossier (5)\debug.log.1.txt").Run();

            Console.WriteLine("Press any key to finish!");
            Console.ReadLine();
        }
    }
}

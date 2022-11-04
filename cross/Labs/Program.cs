using System.IO;

namespace Works
{
    class Program
    {
        static void Main(string[] args)
        {
            string folder = "Lab1";
            string input_path = "INPUT.txt";
            string output_path = "OUTPUT.txt";
            Directory.CreateDirectory(folder);
            
            Lab1.main(folder, input_path, output_path);
        }
    }
}

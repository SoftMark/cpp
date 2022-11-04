using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Works
{
    public class Lab1
    {
        static string read_file(string file_path)
        {
            string text = "";
            if (File.Exists(file_path)){
                text = File.ReadAllText(file_path);
                text = text.Replace("\r", string.Empty);
            }
            else
            {
                throw new Exception("File " + file_path + " doesn't exists!");
            }

            return text;
        }
        static void save_file(string file_path, string text)
        {
            File.WriteAllText(file_path, text);
            Console.Write("Text successfully saved to ");
            Console.WriteLine(file_path);
        }
        static string[] get_pads(string text)
        {
            string[] pads = text.Split('\n');
            string cleared_text = text.Replace('\n', '1');
            if (pads.Length != 2)
                throw new Exception("File must contain exactly 2 lines!");
            
            for (int i = 0; i < cleared_text.Length; i++)
                if (cleared_text[i] != '1' && cleared_text[i] != '2')
                    throw new Exception("File must contain only 1 or 2!");
            return pads;
        }
        static List<char> convert_top_pad(List<char> top_pad, int top_len, int bottom_len)
        {
            for (int i = 0; i < bottom_len - 1; i++)
            {
                top_pad.Insert(top_len + i, '0');
                top_pad.Insert(0, '0');
            }
            return top_pad;
        }
        static List<char> convert_bottom_pad(List<char> bottom_pad, int top_len, int bottom_len)
        {
            for (int i = 0; i < top_len + bottom_len - 2; i++)
            {
                bottom_pad.Insert(bottom_len + i, '0');
            }
            return bottom_pad;
        }
        static List<char> move_pad(List<char> bottom_pad)
        {
            bottom_pad.RemoveAt(bottom_pad.Count - 1);
            bottom_pad.Insert(0, '0');
            return bottom_pad;
        }
        static int get_connect_length(List<char> top_pad, List<char> bottom_pad)
        {
            List<char> connect_length = new List<char> ();
            for(int i = 0; i < top_pad.Count; i++)
            {
                if (top_pad[i] != '0' || bottom_pad[i] != '0')
                    connect_length.Add('0');
            }
            return connect_length.Count;
        }

        private static bool check_pads_not_compatible(char top_char, char bottom_char)
        {   
            return top_char == '2' && bottom_char == '2';
        }

        static int find_min_connected_length(string[] pads)
        {
            List<char> top_pad = new List<char> ( pads[0].ToCharArray() );
            List<char> bottom_pad = new List<char> ( pads[1].ToCharArray() );
            int top_len = top_pad.Count;
            int bottom_len = bottom_pad.Count;

            top_pad = convert_top_pad(top_pad, top_len, bottom_len);
            bottom_pad = convert_bottom_pad(bottom_pad, top_len, bottom_len);

            int min_connected_length = top_len + bottom_len - 1;
            int connect_length = 0;

            bool compatible = true;
            bool any_compatible = false;
            for (int i = 0; i < top_len+bottom_len-1; i++)
            {
                compatible = true;
                for (int j = 0; j < top_pad.Count; j++)
                {
                    char top_char = top_pad[j];
                    char bottom_char = bottom_pad[j];

                    if (top_char == '0' || bottom_char == '0'){
                        continue;
                    }

                    if (check_pads_not_compatible(top_char, bottom_char))
                    {
                        compatible = false;
                        break;
                    }

                }
                if (compatible && !any_compatible)
                {
                    any_compatible = true;
                }


                connect_length = get_connect_length(top_pad, bottom_pad);

                if (compatible && min_connected_length > connect_length)
                {
                    min_connected_length = connect_length;
                }

                Console.WriteLine("Pads position:");
                Console.WriteLine(top_pad.ToArray());
                Console.WriteLine(bottom_pad.ToArray());
                Console.Write("Connect length - ");
                Console.WriteLine(connect_length);
                Console.Write("Connect status - ");
                Console.WriteLine(compatible);
                Console.WriteLine("\n");

                bottom_pad = move_pad(bottom_pad);
            }
            if (any_compatible)
            {
                Console.Write("Minimal connection length - ");
                Console.WriteLine(min_connected_length);
                return min_connected_length;
            }
            else
            {
                Console.WriteLine("Not compatible");
                return top_len + bottom_len;
            }
        }

        public static void main(string folder, string input_path, string output_path)
        {
            string text = read_file(Path.Combine(folder, input_path));
            string[] pads = get_pads(text);
            int output = find_min_connected_length(pads);
            Console.WriteLine(output);
            save_file(Path.Combine(folder, output_path), Convert.ToString(output));
            Console.ReadKey();
        }
    }

    public class Lab2
    {

    }
}

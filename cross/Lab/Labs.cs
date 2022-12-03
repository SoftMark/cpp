using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Works
{
    public class Lab1
    {
        static string read_file(string file_path)
        {
            string text = "";
            if (File.Exists(file_path))
            {
                text = File.ReadAllText(file_path);
                text = text.Replace("\r", string.Empty);
            }
            else
            {
                throw new Exception("File doesn't exists!");
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
            List<char> connect_length = new List<char>();
            for (int i = 0; i < top_pad.Count; i++)
            {
                if (top_pad[i] != '0' || bottom_pad[i] != '0')
                    connect_length.Add('0');
            }
            return connect_length.Count;
        }
        static int find_min_connected_length(string[] pads)
        {
            List<char> top_pad = new List<char>(pads[0].ToCharArray());
            List<char> bottom_pad = new List<char>(pads[1].ToCharArray());
            int top_len = top_pad.Count;
            int bottom_len = bottom_pad.Count;

            top_pad = convert_top_pad(top_pad, top_len, bottom_len);
            bottom_pad = convert_bottom_pad(bottom_pad, top_len, bottom_len);

            int min_connected_length = top_len + bottom_len - 1;
            int connect_length = 0;

            bool compatible = true;
            for (int i = 0; i < top_len + bottom_len - 1; i++)
            {
                compatible = true;
                for (int j = 0; j < top_pad.Count; j++)
                {
                    char top_char = top_pad[j];
                    char bottom_char = bottom_pad[j];


                    if (top_char == bottom_char && top_char != '0' && bottom_char != '0')
                    {
                        compatible = false;
                    }

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
            Console.Write("Minimal connection length - ");
            Console.WriteLine(min_connected_length);


            return min_connected_length;
        }


        public static void run(string input_path, string output_path)
        {
            string text = read_file(input_path);
            string[] pads = get_pads(text);
            int output = find_min_connected_length(pads);
            save_file(output_path, Convert.ToString(output));
        }
    }

    public class Lab2
    {
        public class zn
        {
            public int[] x = new int[10];
            public int[] y = new int[10];
            public int[] p = new int[10];
        }
        public static zn[] a;
        public static long[,,,] b;
        public static int string_to_int(string str)
        {
            try
            {
                return Convert.ToInt32(str[str.Length - 1].ToString());
            }
            catch
            {
                throw new Exception("File must containe ONE positive integer number!");
            }
        }
        static string read_file(string file_path)
        {
            string text = "";
            if (File.Exists(file_path))
            {
                text = File.ReadAllText(file_path);
                text = text.Replace("\r", string.Empty);
            }
            else
            {
                throw new Exception("File doesn't exists!");
            }
            return text;
        }
        static void sumwozwar(int x, int y, int num, int per)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (x != i && y != j)
                    {
                        b[num, x, y, (x + y + per) / 10] += b[num + 1, i, j, per];
                    }
                }
            }
            b[num, x, y, (x + y + per) / 10] %= 1000000007;
        }
        static void vozwar(int num, int k)
        {
            for (int i = 0; i < 10; i++)
            {
                int x, y;
                x = a[k].x[i];
                y = a[k].y[i];
                sumwozwar(x, y, num, 0);
            }
            k--;
            if (k < 0) k = 9;
            for (int i = 0; i < 10; i++)
            {
                int x, y;
                x = a[k].x[i];
                y = a[k].y[i];
                sumwozwar(x, y, num, 1);
            }
        }
        static void w()
        {
            a = new zn[10];
            int[] z = new int[10];
            a = a.Select(e => new zn()).ToArray();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    int sum = i + j;
                    int s1 = sum % 10;
                    int p = sum / 10;
                    a[s1].x[z[s1]] = i;
                    a[s1].y[z[s1]] = j;
                    a[s1].p[z[s1]] = p;
                    z[s1]++;
                }
            }
        }
        
        public static void run(string input_path, string output_path)
        {
            w();
            string c = read_file(input_path);
            b = new long[c.Length+1, 10, 10, 2];
            int k = string_to_int(c);
            for (int i = 0; i < 10; i++)
            {
                int x, y, p;
                x = a[k].x[i];
                y = a[k].y[i];
                p = a[k].p[i];
                b[c.Length - 1, x, y, p] = 1;
            }
            for (int q = c.Length - 2; q > -1; q--)
            {
                vozwar(q, k);
            }
            long sum = 0;
            for (int i = 1; i < 10; i++)
            {
                for (int j = 1; j < 10; j++)
                {
                    sum += b[0, i, j, 0];
                }
            }
            sum %= 1000000007;
            File.WriteAllText(output_path, Convert.ToString(sum));
        }
    }
}

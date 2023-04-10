using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConsoleApp22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int Max(int[] a)
            {
                int max = a[0];
                for (int i = 1; i < a.Length; i++)
                {
                    if (max < a[i] && a[i] != -1)
                        max = a[i];
                }
                return max;
            }
            int Min(int[] a)
            {
                int min = 3000;//if array elements are -1
                for (int i = 0; i < a.Length; i++)
                {
                    if (min > a[i] && a[i] != -1)
                        min = a[i];
                }
                return min;
            }
            string Mult(string fs)
            {
                int indexOfMul = fs.IndexOf("*");
                int indexOfDiv = fs.IndexOf("/");
                int indexOfPlu = fs.IndexOf("+");
                int indexOfMin = fs.IndexOf("-");

                string newStr = "";
                string befMul = fs.Substring(0, indexOfMul);
                string aftMul = fs.Substring(indexOfMul + 1);
                int[] b = { befMul.LastIndexOf("+"), befMul.LastIndexOf('-') };
                string first = befMul.Substring(Max(b) + 1);
                decimal n1 = decimal.Parse(first);
                if (aftMul[0] == '-')
                {
                    n1 *= -1;
                    aftMul = aftMul.Substring(1);
                }

                int[] a = { aftMul.IndexOf("-"), aftMul.IndexOf("/"), aftMul.IndexOf("+") };
                string second = "";
                if (Min(a) != 3000)
                    second = aftMul.Substring(0, Min(a));
                else
                    second = aftMul;
                decimal n2 = decimal.Parse(second);


                decimal newNumber = n1 * n2;


                if (fs.Contains($"{first}*-{second}"))
                    newStr = fs.Replace($"{first}*-{second}", $"{newNumber}");
                else
                    newStr = fs.Replace($"{first}*{second}", $"{newNumber}");

                return newStr;


            }

            string Divi(string fs)
            {
                int indexOfMul = fs.IndexOf("*");
                int indexOfDiv = fs.IndexOf("/");
                int indexOfPlu = fs.IndexOf("+");
                int indexOfMin = fs.IndexOf("-");

                string newStr = "";
                string befDiv = fs.Substring(0, indexOfDiv);
                string aftDiv = fs.Substring(indexOfDiv + 1);
                int[] b = { befDiv.LastIndexOf("+"), befDiv.LastIndexOf('-') };
                string first = befDiv.Substring(Max(b) + 1);
                decimal n1 = decimal.Parse(first);
                if (aftDiv[0] == '-')
                {
                    n1 *= -1;
                    aftDiv = aftDiv.Substring(1);
                }

                int[] a = { aftDiv.IndexOf("-"), aftDiv.IndexOf("*"), aftDiv.IndexOf("+") };
                string second = "";
                if (Min(a) != 3000)
                    second = aftDiv.Substring(0, Min(a));
                else
                    second = aftDiv;
                decimal n2 = decimal.Parse(second);

                if (n2 == 0)
                    return "Tivy chi kareli bajanel 0i";
                decimal newNumber = n1 / n2;


                if (fs.Contains($"{first}/-{second}"))
                    newStr = fs.Replace($"{first}/-{second}", $"{newNumber}");
                else
                    newStr = fs.Replace($"{first}/{second}", $"{newNumber}");

                return newStr;


            }

            string PlusMin(string str)
            {
                string newStr = "";

                if (str.Contains("+-"))
                    return PlusMin(str.Replace("+-", "-"));
                if (str.StartsWith("--"))
                    return PlusMin(str.Replace("--", ""));
                else if (str.Contains("--"))
                    return PlusMin(str.Replace("--", "+"));
                if (!str.Contains("-") && !str.Contains("+"))
                    return str;

                if (str.IndexOf("+") == -1 && str.IndexOf("-") != 0)
                {

                    string befMin = str.Substring(0, str.IndexOf("-"));
                    string aftMin = str.Substring(str.IndexOf("-") + 1);
                    int[] a = { aftMin.IndexOf("-"), aftMin.IndexOf("+") };
                    decimal n1 = decimal.Parse(befMin);

                    decimal n2 = 0;

                    if (Min(a) == 3000)
                        n2 = decimal.Parse(aftMin);
                    else
                        n2 = decimal.Parse(aftMin.Substring(0, Min(a)));

                    decimal newNumber = n1 - n2;

                    newStr = str.Replace($"{n1}-{n2}", $"{newNumber}");

                }
                else if (str.IndexOf("-") == -1)
                {
                    string befMin = str.Substring(0, str.IndexOf("+"));
                    string aftMin = str.Substring(str.IndexOf("+") + 1);
                    int[] a = { aftMin.IndexOf("-"), aftMin.IndexOf("+") };

                    decimal n1 = decimal.Parse(befMin);
                    decimal n2 = 0;
                    if (Min(a) == 3000)
                        n2 = decimal.Parse(aftMin);
                    else
                        n2 = decimal.Parse(aftMin.Substring(0, Min(a)));


                    decimal newNumber = n1 + n2;

                    newStr = str.Replace($"{n1}+{n2}", $"{newNumber}");

                }
                else if (str.IndexOf("-") == 0 && !str.Contains("+") && str.LastIndexOf("-") == 0)
                {
                    return str;
                }
                else if (str.IndexOf("-") == 0 && (str.Contains("+") || str.LastIndexOf("-") != 0))
                {
                    string wom = str.Substring(1);
                    int[] b = { wom.IndexOf("-"), wom.IndexOf("+") };
                    string befMin = str.Substring(0, Min(b) + 1);
                    string aftMin = str.Substring(Min(b) + 2);
                    int[] a = { aftMin.IndexOf("-"), aftMin.IndexOf("+") };

                    decimal n1 = decimal.Parse(befMin);

                    decimal n2 = 0;


                    if (Min(a) == 3000)
                        n2 = decimal.Parse(aftMin);
                    else
                        n2 = decimal.Parse(aftMin.Substring(0, Min(a)));


                    decimal newNumber = 0;

                    if (Min(b) == wom.IndexOf("-"))
                    {
                        newNumber = n1 - n2;
                        newStr = str.Replace($"{n1}-{n2}", $"{newNumber}");
                    }

                    else
                    {
                        newNumber = n1 + n2;
                        newStr = str.Replace($"{n1}+{n2}", $"{newNumber}");
                    }



                }
                else if (str.IndexOf("+") > str.IndexOf("-"))
                {
                    string befMin = str.Substring(0, str.IndexOf("-"));
                    string aftMin = str.Substring(str.IndexOf("-") + 1);
                    int[] a = { aftMin.IndexOf("-"), aftMin.IndexOf("+") };
                    decimal n1 = decimal.Parse(befMin);

                    decimal n2 = 0;

                    if (Min(a) == 3000)
                        n2 = decimal.Parse(aftMin);
                    else
                        n2 = decimal.Parse(aftMin.Substring(0, Min(a)));


                    decimal newNumber = n1 - n2;

                    newStr = str.Replace($"{n1}-{n2}", $"{newNumber}");

                }
                else if (str.IndexOf("+") < str.IndexOf("-"))
                {
                    string befMin = str.Substring(0, str.IndexOf("+"));
                    string aftMin = str.Substring(str.IndexOf("+") + 1);
                    int[] a = { aftMin.IndexOf("-"), aftMin.IndexOf("+") };
                    decimal n1 = decimal.Parse(befMin);
                    decimal n2 = 0;

                    if (Min(a) == 3000)
                        n2 = decimal.Parse(aftMin);
                    else
                        n2 = decimal.Parse(aftMin.Substring(0, Min(a)));

                    decimal newNumber = n1 + n2;

                    newStr = str.Replace($"{n1}+{n2}", $"{newNumber}");

                }

                if ((newStr.Contains("+")) || (newStr.Contains("-") && newStr.IndexOf("-") != 0 && newStr.LastIndexOf("-") != 0))
                {
                    return PlusMin(newStr);
                }

                return newStr;
            }

            string Count(string fs)
            {
                while (fs.Substring(1).Contains("-") || fs.Substring(1).Contains("+") || fs.Substring(1).Contains("/") || fs.Substring(1).Contains("*"))
                {
                    if (fs.Contains("*") && fs.IndexOf("*") < fs.IndexOf("/"))
                    {
                        fs = Mult(fs);

                        continue;
                    }
                    else if (fs.Contains("*") && !fs.Contains("/"))
                    {
                        fs = Mult(fs);
                        continue;
                    }
                    else if (fs.Contains("/") && fs.IndexOf("*") > fs.IndexOf("/"))
                    {
                        fs = Divi(fs);
                        if (fs == "Tivy chi kareli bajanel 0i")
                            return "Tivy chi kareli bajanel 0i";
                        continue;
                    }
                    else if (fs.Contains("/") && !fs.Contains("*"))
                    {
                        fs = Divi(fs);
                        if (fs == "Tivy chi kareli bajanel 0i")
                            return "Tivy chi kareli bajanel 0i";
                        continue;
                    }
                    else if (!fs.Contains("/") && !fs.Contains("*") && (fs.Contains("+") || fs.Contains("-")))
                    {
                        fs = PlusMin(fs);
                        continue;
                    }

                }

                return fs;
            }

            int BracketCount(string str)
            {
                int count = 0;
                if (str == "")
                    return count;
                for (int i = 0; i < str.Length - 1; i++)
                {
                    if (str[i] == '(')
                        count++;
                }
                return count;
            }
            string HandleBracket(string str)
            {
                string s = str;

                if (s.Contains("("))
                {
                    s = s.Substring(s.IndexOf("(") + 1, s.IndexOf(")") - 1 - s.IndexOf("("));
                    if (s.Contains("("))
                    {
                        for (int i = 0; i <= BracketCount(s); i++)
                        {

                            if (s.Contains("("))
                                s = s.Substring(s.IndexOf("(") + 1);
                            else
                                s = Calc(s).ToString();
                        }
                    }
                }
                else
                {
                    return s;
                }
                return HandleBracket(str.Replace($"({s})", Calc(s).ToString()));

            }

            decimal Calc(string str)
            {
                string num = "";
                try
                {
                    num = HandleBracket(str);
                    num = Count(num);
                    decimal finalNumber = decimal.Parse(num);


                    return Math.Round(finalNumber, 4);
                }
                catch
                {
                    if (num == "Tivy chi kareli bajanel 0i")
                        Console.WriteLine("Tivy chi kareli bajanel 0i");
                    else
                        Console.WriteLine("Ardyunqy shat mec tiv e");
                    return 0;
                }

            }

            while (true)
            {
                Console.WriteLine("Nermucel artahaytutyun");
                string exp = Console.ReadLine();
                Console.WriteLine("Result = " + Calc(exp));
            }


            //string connect = "127.0.0.1 via TCP/IP";
            //SqlConnection con = new SqlConnection(connect);
            //con.Open();

            //string query = $"INSERT INTO calchistory (Expression,Result) VALUES ({exp},{Calc(exp)})";

            //SqlCommand cmd = new SqlCommand(query,con);
            //cmd.ExecuteNonQuery();

            //con.Close();

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace ConsoleApp22
{
    
    class Connect
    {
        Program p = new Program();
        static string conn = ConfigurationManager.ConnectionStrings["myDbConnection"].ConnectionString;

        public void Write(string exp)
        {
            try
            {
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();
                String sql = $"INSERT INTO calchistory (Expression,Result) VALUES ({exp},{p.Calc(exp)})";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.ExecuteNonQuery();
                using(var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Saved");
                    }
                    reader.Close();
                    connection.Close();
                }
                
                
            }
            catch (Exception err)
            {
                Console.WriteLine("Error: " + err);
            }

        }
    }
    class MultiplyDivide
    {
        protected string Service(string str, char mark)
        {
            char other = '*';
            if (mark == '*')
                other = '/';

            string newStr;
            var befMul = str.Substring(0, str.IndexOf(mark));
            var aftMul = str.Substring(str.IndexOf(mark) + 1);
            int[] b = { befMul.LastIndexOf("+"), befMul.LastIndexOf('-') };
            string first = befMul.Substring(b.Max() + 1);
            decimal n1 = decimal.Parse(first);
            if (aftMul[0] == '-')
            {
                n1 *= -1;
                aftMul = aftMul.Substring(1);
            }

            List<int> a = new List<int>() { };

            if (aftMul.IndexOf('-') != -1)
                a.Add(aftMul.IndexOf("-"));
            if (aftMul.IndexOf('+') != -1)
                a.Add(aftMul.IndexOf("+"));
            if (aftMul.IndexOf(other) != -1)
                a.Add(aftMul.IndexOf(other));

            string second;
            if (a.Count != 0)
                second = aftMul.Substring(0, a.Min());
            else
                second = aftMul;
            decimal n2 = decimal.Parse(second);
            decimal newNumber;
            if (mark == '*')
                newNumber = n1 * n2;
            else
                newNumber = n1 / n2;


            if (str.Contains($"{first}{mark}-{second}"))
                newStr = str.Replace($"{first}{mark}-{second}", $"{newNumber}");
            else
                newStr = str.Replace($"{first}{mark}{second}", $"{newNumber}");

            return newStr;
        }
    }
    class Program : MultiplyDivide
    {

        string Multiply(string str)
        {
            return Service(str,'*');
        }
        string Divide(string str)
        {
            return Service(str,'/');
        }
        string AddSubtract(string str)
        {
            string newStr;

            if (str.Contains("+-"))
                return AddSubtract(str.Replace("+-", "-"));
            else if (str.Contains("--"))
                return AddSubtract(str.Replace("--", "+"));
            if (!str.Contains("-") && !str.Contains("+"))
                return str;

            int[] a = { str.Substring(1).IndexOf("-"), str.Substring(1).IndexOf("+") };
            int[] b = { str.IndexOf("-"), str.IndexOf("+") };

            int aMin = (a.Min() == -1) ? a.Max() : a.Min();
            int bMin = (b.Min() == -1) ? b.Max() : b.Min();
            int nextMark = (str[0] == '-') ? aMin + 1 : bMin;

            decimal n1 = decimal.Parse(str.Substring(0, nextMark));
            string after = str.Substring(nextMark + 1);

            int[] c = { after.IndexOf("+"), after.IndexOf("-") };

            int afterNextMark = (c.Min() != -1) ? c.Min() : c.Max();
            decimal n2;
            if (afterNextMark == -1)
                n2 = decimal.Parse(str.Substring(nextMark + 1));
            else
                n2 = decimal.Parse(str.Substring(nextMark + 1, afterNextMark));

            if (str[nextMark] == '-')
            {
                decimal newNumber = n1 - n2;
                newStr = str.Replace($"{n1}-{n2}", $"{newNumber}");
            }
            else
            {
                decimal newNumber = n1 + n2;
                newStr = str.Replace($"{n1}+{n2}", $"{newNumber}");
            }

            if ((newStr.Contains("+") || (newStr.Contains("-")) && newStr.IndexOf("-") != 0 && newStr.LastIndexOf("-") != 0))
                return AddSubtract(newStr);

            return newStr;
        }
        string Count(string fs)
        {
            while (fs.Substring(1).Contains("-") || fs.Substring(1).Contains("+") || fs.Substring(1).Contains("/") || fs.Substring(1).Contains("*"))
            {
                if ((fs.Contains("*") && fs.IndexOf("*") < fs.IndexOf("/")) || (fs.Contains("*") && !fs.Contains("/")))
                {
                    fs = Multiply(fs);
                    continue;
                }
                else if ((fs.Contains("/") && fs.IndexOf("*") > fs.IndexOf("/")) || (fs.Contains("/") && !fs.Contains("*")))
                {
                    fs = Divide(fs);
                    if (fs == "Tivy chi kareli bajanel 0i")
                        return "Tivy chi kareli bajanel 0i";
                    continue;
                }
                else if (!fs.Contains("/") && !fs.Contains("*") && (fs.Contains("+") || fs.Contains("-")))
                {
                    fs = AddSubtract(fs);

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

            if (str.Contains("("))
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
                return s;
            return HandleBracket(str.Replace($"({s})", Calc(s).ToString()));
        }
        public decimal Calc(string str)
        {
            string num;
            str = str.Replace(" ", "");
            num = HandleBracket(str);
            num = Count(num);
            decimal finalNumber = decimal.Parse(num);
            return Math.Round(finalNumber, 4);
        }



        static void Main(string[] args)
        {
            Program p = new Program();
            Connect c = new Connect();




            //while (true)
            //{
                Console.WriteLine("Input the expression");
                string exp = Console.ReadLine();
                Console.WriteLine("Result = " + p.Calc(exp));
                c.Write(exp);
            //}





        }
    }
}

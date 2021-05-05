using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            localhost.WebService1 ws = new localhost.WebService1();
            Console.WriteLine("versão EN" + ws.HelloWorld());
        }
    }
}

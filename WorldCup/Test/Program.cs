using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test {
    class Program {
        static void Main(string[] args) {
            List<int> l1 = new List<int>();
            int[] arr1 = { 1, 3, 2, 6 };
            l1.AddRange(arr1);
            List<int> l2 = new List<int>();
            int[] arr2 = { 4, 9, 7, 3 };
            l2.AddRange(arr2);
            l1.AddRange(l2);
            l1 = l1 + l2;

            Console.ReadKey();
        }
    }
}

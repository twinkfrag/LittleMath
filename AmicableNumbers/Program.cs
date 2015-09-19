using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using static System.Math;

namespace AmicableNumbers
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			try
			{
				WriteLine(IsAmicableNumbers(int.Parse(args[0]), int.Parse(args[1])));
			}
			catch (Exception e) when (e is IndexOutOfRangeException || e is ArgumentNullException || e is FormatException)
			{
				int a, b;
				if (!int.TryParse(ReadLine(), out a)) return;
				if (!int.TryParse(ReadLine(), out b)) return;
				WriteLine(IsAmicableNumbers(a, b));
			}

			ReadKey();
		}

		private static bool IsAmicableNumbers(int a, int b)
		{
			var factorA = GetFactor(a);
			var factorB = GetFactor(b);
			var a2 = factorA.Sum();
			var b2 = factorB.Sum();

			WriteLine($"{a}: {factorA.JoinToString()} => {a2}");
			WriteLine($"{b}: {factorB.JoinToString()} => {b2}");
			return a == b2 && b == a2;
		}

		private static int[] GetPrimeFactor(int n)
		{
			//var factors = Primes.ToDictionary(x => x, x => 0);
			var isEven = (n & 0x1) == 0;
			var factorList = new List<int>((int)Log(n, isEven ? 2 : 3));

			for (var i = isEven ? 0 : 1; i < Primes.Length;)
			{
				if (n % Primes[i] == 0)
				{
					factorList.Add(Primes[i]);
					n /= Primes[i];
				}
				else ++i;
			}
			return factorList.ToArray();
		}

		private static int[] GetFactor(int n)
		{
			var isEven = (n & 0x1) == 0;
			var root = (int)Sqrt(n);
			var log = (int)Log(n, isEven ? 2 : 3);

			var factorMinList = new List<int>(log) { 1 };
			var factorMaxList = new List<int>(log);
			int k = 0;

			for (var i = isEven ? 0 : 1; Primes[i] <= root; ++i)
			{
				for (int f = Primes[i], j = 0; j < factorMinList.Count && f <= root; f = Primes[i] * factorMinList[++j])
				{
					++k;
					if (n % f == 0)
					{
						factorMinList.Add(f);
						factorMaxList.Add(n / f);
					}
					else break;
				}
			}

			return factorMinList.Concat(factorMaxList).ToArray();
		}

		private static readonly int[] Primes = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 };

	}

	static class Extension
	{
		public static string JoinToString<T>(this IEnumerable<T> enumerable)
		{
			return string.Join(", ", enumerable.Select(x => x.ToString()));
		}
	}
}

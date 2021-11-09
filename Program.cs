using System;
using number;
using numerics;

namespace CheatCaculator {
	class Program {
		static void Main(string[] args) {
			int[,] data = new int[3,3];
			data[0,0] = 1;
			data[1,1] = 2;
			data[2,2] = 3;
			Matrix<int> mat = new Matrix<int>(data);
			Console.WriteLine($"check = {mat.getRow(1)[1]}");
		}
	}
}

using System;
using System.Diagnostics;
using System.Collections.Generic;

// Solution code goes here
public class Solution
{
	public static Dictionary<string, double>[] DoAggregations(double[,] input)
	{

		// Getting the width and height of the input array

		long width = input.GetLength(1);
		long height = input.GetLength(0);
		Dictionary<string, double>[] result = new Dictionary<string, double>[height];


		int i, j;

		for (i = 0; i < width; i++)
		{
			Dictionary<string, double> colResult = new Dictionary<string, double>();
			HashSet<double> listofNos = new HashSet<double>();

			double sum = 0;

			for (j = 0; j < height; j++)
			{
				sum += input[j, i];
				listofNos.Add(input[j, i]);
			}

			colResult.Add("SUM", sum);
			colResult.Add("AVERAGE", sum / height);
			colResult.Add("COUNT DISTINCT", listofNos.Count);

			result[i] = colResult;
		}
        return result;
	}
}

public class Mock
{
	public static Dictionary<string, double>[] DoAggregations(double[,] input)
	{
		return new Dictionary<string, double>[]{
			new Dictionary<string, double>{{"SUM", 9d}, {"AVERAGE", 2.25d}, {"COUNT DISTINCT", 3d}},
			new Dictionary<string, double>{{"SUM", 14d}, {"AVERAGE", 3.5d}, {"COUNT DISTINCT", 2d}},
			new Dictionary<string, double>{{"SUM", 16d}, {"AVERAGE", 4d}, {"COUNT DISTINCT", 4d}}};
	}
}

public class Program
{
	// Don't change Main
	public static void Main()
	{
		//*** SMALL INPUT ***//
		double[,] smallInput = new double[,] { { 1, 4, 3 }, { 2, 3, 4 }, { 1, 3, 7 }, { 5, 4, 2 } };

		// Mocked example of execution
		Console.WriteLine("=== Small Input - Mocked Execution ===");
		var mockResult = Mock.DoAggregations(smallInput);
		IsResultCorrect(mockResult);

		// Your code execution
		Console.WriteLine("\n=== Small Input - Solution Execution ===");
		var result = Solution.DoAggregations(smallInput);
		IsResultCorrect(result);

		//*** LARGE INPUT ***//
		Console.WriteLine("\n=== Large Input - Solution Execution ===");
		var largeInput = GenerateLargeInput();
		var sw = Stopwatch.StartNew();
		for (int i = 0; i < 5; i++)
			Solution.DoAggregations(largeInput);
		sw.Stop();
		IsExecutionFastEnough(sw.ElapsedMilliseconds / 5);
	}

	public static double[,] GenerateLargeInput()
	{
		int numRows = 15000;
		int numCols = 100;

		var input = new double[numRows, numCols];
		Random rnd = new Random(123);
		for (int col = 0; col < numCols; col++)
			for (int row = 0; row < numRows; row++)
				input[row, col] = rnd.Next(100000);

		return input;
	}

	public static void IsResultCorrect(Dictionary<string, double>[] result)
	{
		if (
			result[0]["SUM"] == 9d &&
			result[1]["SUM"] == 14d &&
			result[2]["SUM"] == 16d &&
			result[0]["AVERAGE"] == 2.25d &&
			result[1]["AVERAGE"] == 3.5d &&
			result[2]["AVERAGE"] == 4d &&
			result[0]["COUNT DISTINCT"] == 3d &&
			result[1]["COUNT DISTINCT"] == 2d &&
			result[2]["COUNT DISTINCT"] == 4d
		)
		{
			Console.WriteLine("IsResultCorrect Passed");
			return;
		}

		Console.WriteLine("IsResultCorrect Failed");
	}

	public static void IsExecutionFastEnough(double milliSeconds)
	{

		if (milliSeconds < 90)
			Console.WriteLine($"IsExecutionFastEnough DIAMOND!!!: {milliSeconds}ms");
		else if (milliSeconds < 105)
			Console.WriteLine($"IsExecutionFastEnough GOLD!: {milliSeconds}ms");
		else if (milliSeconds < 150)
			Console.WriteLine($"IsExecutionFastEnough SILVER: {milliSeconds}ms");
		else
			Console.WriteLine($"IsExecutionFastEnough BRONZE: {milliSeconds}ms");
	}
}
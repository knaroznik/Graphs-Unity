using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities {

	public static int[][] MultiplyMatrix(int[][] A, int[][] B)
	{
		int rA = A.Length;
		int cA = A.Length;
		int cB = B.Length;
		int temp = 0;
		int[][] result = new int[rA][];
		for (int i = 0; i < result.Length; i++)
		{
			result[i] = new int[cB];
		}
		for (int i = 0; i < rA; i++)
		{
			for (int j = 0; j < cB; j++)
			{
				temp = 0;
				for (int k = 0; k < cA; k++)
				{
					temp += A[i][k] * B[k][j];
				}

				result[i][j] = temp;
			}
		}

		return result;
	}

	public static int MatrixTrace(int[][] matrix)
	{
		int result = 0;
		for (int i = 0; i < matrix.Length; i++)
		{
			result += matrix[i][i];
		}

		return result;
	}
}

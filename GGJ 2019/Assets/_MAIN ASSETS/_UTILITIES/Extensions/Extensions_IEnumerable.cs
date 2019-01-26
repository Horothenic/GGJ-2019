using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public static class Extensions_IEnumerable 
{
	public static void ShuffleList <T>(this IList<T> list)  
	{  
		int n = list.Count;  
		while (n > 1) 
		{  
			n--;  
			int k = new Random ().Next (n + 1);  
			T value = list[k];  
			list[k] = list[n];  
			list[n] = value;  
		}  
	}
}

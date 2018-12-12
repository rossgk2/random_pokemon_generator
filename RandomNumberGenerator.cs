using System;

public class RandomNumberGenerator
{
	static Random _RANDOMGEN = new Random();
	
	public static Random RANDOMGEN()
	{
		return _RANDOMGEN;
	}
}
using System;

/* Wrapper class for random number generation.
 * In Pokemon, a random number generator is only seeded once. For this reason, a static class 
 * that wraps a single random number generator (seeded only once) is used.
 */
public static class RandomNumberGenerator
{
	static Random _RANDOMGEN = new Random();
	
	public static Random RANDOMGEN()
	{
		return _RANDOMGEN;
	}
}
using System;

public abstract class AbstractStat
{
	public string name
	{
		get;
		protected set;
	}
	
	public int pokemonLevel
	{
		get;
		set;
	}
	
	public int statValue
	{
		get;
		protected set;
	}
	
	public int baseStat
	{
		get;
		protected set;
	}
	
	public int IV
	{
		get;
		protected set;
	}
	
	public int EV
	{
		get;
		protected set;
	}
	
	public AbstractStat()
	{  }
	
	//use this for most new Pokemon
	public AbstractStat(string name, int pokemonLevel, int baseStat)
	{
		this.name = name;
		this.pokemonLevel = pokemonLevel;
		this.baseStat = baseStat;
		
		IV = RandomNumberGenerator.RANDOMGEN().Next(32); //UnityEngine.Random.Range(0, 32); // 0 - 31
		EV = 0;
		
		ComputeStatValue();
	}
	
	public abstract void ComputeStatValue();
}

public class Stat : AbstractStat
{
	public Nature nature
	{
		get;
		private set;
	}
	
	public Stat() : base()
	{
		
	}
	
	public Stat(string name, int pokemonLevel, int baseStat, Nature nature)
	{
		this.name = name;
		this.pokemonLevel = pokemonLevel;
		this.baseStat = baseStat;
		this.nature = nature;
		
		IV = RandomNumberGenerator.RANDOMGEN().Next(32); //UnityEngine.Random.Range(0, 32); // 0 - 31
		EV = 0;
		
		ComputeStatValue();
	}
	
	public override void ComputeStatValue()
	{
		statValue = (((IV + (2 * baseStat) + (EV / 4)) * pokemonLevel) / 100) + 5;
		if (name.ToLower() == nature.increasedStat.ToLower())
		{
			statValue = (int)(statValue * 1.1f);
		}
		else if (name.ToLower() == nature.decreasedStat.ToLower())
		{
			statValue = (int)(statValue * .9f);
		}
	}
	
	public override string ToString()
	{
		return name + ": \n\n\t" + 
			"Stat Value: " + statValue + "\n\t" +
				"Base Stat: " + baseStat + "\n\t" +
				"IV: " + IV + "\n\t" +
				"EV: " + EV;
	}   
}

public class HP : AbstractStat
{
	
	public HP(string name, int pokemonLevel, int baseStat) : base(name, pokemonLevel, baseStat)
	{ }
	
	public void SetHP(int HP)
	{
		statValue = HP;
	}
	
	public override void ComputeStatValue()
	{
		statValue = (((IV + (2 * baseStat) + (EV / 4) + 100) * pokemonLevel) / 100) + 10;
	}
	
	public override string ToString()
	{
		return name + ": \n\n\t" +
			"HP: " + statValue + "\n\t" +
				"Base HP: " + baseStat + "\n\t" +
				"IV: " + IV + "\n\t" +
				"EV: " + EV;
	}
}

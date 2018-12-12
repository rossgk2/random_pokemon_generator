﻿using System;

/* Different Pokemon have different "Stats": Attack, Defence, Special Attack, Special Defense, Speed, IV, and EV.
 * Each Stat also has an associated EV and IV value. IV is randomly assigned an integer value between 0 and 31, and EV is
 * initalized to 0. The actual value of a specific stat (represented as "statValue" in this code) that a Pokemon gets is determined 
 * by the Pokemon's level, the Pokemon's "baseStatValue" field, and by a little bit of randomness.
 * 
 * Each Stat also has a "modifier" field, because a Pokemon's "Nature" causes some "Stat"s to increase and some to decrease.
 */

public class Stat
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
	
	public int baseStatValue
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

    public float modifier
    {
        get;
        private set;
    }

    public Stat() { }
	
    public Stat(string name, int pokemonLevel, int baseStat, float modifier)
    {
        this.name = name;
        this.pokemonLevel = pokemonLevel;
        this.baseStatValue = baseStat;
        this.modifier = modifier;

        IV = RandomNumberGenerator.RANDOMGEN().Next(32); //UnityEngine.Random.Range(0, 32); // 0 - 31
        EV = 0;

        ComputeStatValue();
    }
    public virtual void ComputeStatValue()
    {
        statValue = (((IV + (2 * baseStatValue) + (EV / 4)) * pokemonLevel) / 100) + 5;
        statValue = (int) (statValue * modifier);
    }

    public override string ToString()
    {
        return name + ": \n\n\t" +
            "Stat Value: " + statValue + "\n\t" +
                "Base Stat: " + baseStatValue + "\n\t" +
                "IV: " + IV + "\n\t" +
                "EV: " + EV;
    }
}

/*
 * HP is a Stat that will never be affected by a Pokemon's Nature.
 */
public class HP : Stat
{
	public HP(string name, int pokemonLevel, int baseStat) : base(name, pokemonLevel, baseStat, 1)
	{ }
	
	public void SetHP(int HP)
	{
		statValue = HP;
	}
	
	public override void ComputeStatValue()
	{
		statValue = (((IV + (2 * baseStatValue) + (EV / 4) + 100) * pokemonLevel) / 100) + 10;
	}
	
	public override string ToString()
	{
		return name + ": \n\n\t" +
			"HP: " + statValue + "\n\t" +
				"Base HP: " + baseStatValue + "\n\t" +
				"IV: " + IV + "\n\t" +
				"EV: " + EV;
	}
}

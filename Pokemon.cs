using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class Pokemon
{
    public static int MAX_POKEMON_LEVEL = 100;
    public static int MAX_POKEDEX_NUMBER = 802;

    public PokedexID pokedexID
	{
		get;
		private set;
	}
	
	public List<string> types
	{
		get
        {
            return baseStats.types;
        }
        private set
        {
            types = value;
        }
	}
	
	public int level
	{
		get;
		private set;
	}
	
	private readonly uint personalityValue;
	
	public BaseStats baseStats
	{
		get;
		private set;
	}
	
	public Dictionary<string, Stat> stats
	{
		get
        {
            return baseStats.statDict;
        }
		private set
        {
            stats = value;
        }
	}
	
	public HP HP
	{
        get
        {
            Stat ret;
            bool success = stats.TryGetValue("HP", out ret);
            if (!success) { Console.WriteLine("HP \"get\" property failed."); } 
            return (HP) ret;
        }
		private set
        {
            HP = value;
        }
	}
	
	public string gender
	{
		get;
		private set;
	}

    public Ability ability;
	
	public Nature nature
	{
		get
        {
            return baseStats.nature;
        }
		private set
        {
            nature = value;
        }
	}
	
	public bool isShiny
	{
		get;
		private set;
	}
	
	public Pokemon(PokedexID pokedexID, int level, BaseStats baseStats)
	{
		this.pokedexID = pokedexID;
		this.level = level;
		this.baseStats = baseStats;

        this.personalityValue = baseStats.personalityValue; //this must occur in constructor
        foreach (KeyValuePair<string, Stat> kvp in baseStats.statDict)
        {
            kvp.Value.ComputeStatValue(level);
        }

        gender = personalityValue % 256 >= baseStats.genderThreshold ? "Male" : "Female";

        List<Ability> abs = baseStats.abilities;
        if (abs.Count == 1)
        {
            ability = abs[0];
        }
        else if (abs.Count == 2)
        {
            ability = personalityValue % 2 == 0 ? abs[0] : abs[1];
        }
        else
        {
            ability = new Ability();
            Console.Error.WriteLine("(The Pokemon " + this.pokedexID.name + " was created with an empty Ability List.\n" +
                "Abilities have not been implemented yet.)");
        }

        isShiny = chance(.0122070312);
    }
	
	public void LevelUp()
	{
		
	}
	
	public void Evolve()
	{
		
	}
	
	public override string ToString()
	{
		string s = "Name: " + pokedexID.name + "\n" +
			"Pokedex Number: " + pokedexID.pokedexNumber + "\n" +
				"Level: " + level + "\n" +
				"Gender: " + gender + "\n";

        string typeInfo = "Types: [" + string.Join(", ", this.types) + "]";
		
		s += typeInfo + "\n" +
			"Nature: " + nature.name + "\n" +
				"Ability: " + ability.name + "\n" +
				"Shiny?: " + isShiny + "\n\n" +
				"Stats: " + "\n\t";

        string statInfo = "";
        Dictionary<string, Stat> stats = baseStats.statDict;
        Stat[] indexer = new Stat[stats.Count];
        stats.Values.CopyTo(indexer, 0);
        for (int i = 0; i < stats.Count; ++ i)
        {
            Stat st = stats[indexer[i].name];
            statInfo += st.name + ": " + st.baseStatValue + " (base value), " + st.statValue + " (actual value)";
            if (i != stats.Count - 1)
            {
                statInfo += "\n\t";
            }
        }
        s += statInfo;
		
		return s;
	}
	
	public static uint GeneratePersonalityValue()
	{
		uint thirtyBits = (uint)RandomNumberGenerator.RANDOMGEN().Next(1 << 30);
		uint twoBits = (uint)RandomNumberGenerator.RANDOMGEN().Next(1 << 2);
		uint fullRange = (thirtyBits << 2) | twoBits;
		return fullRange;
	}

    bool chance(double chance)
    {
        double random = new System.Random().NextDouble();
        return random < chance;
    }
}

public class PokedexID
{
	public string name
	{
		get;
		private set;
	}
	
	public int pokedexNumber
	{
		get;
		private set;
	}
	
	public PokedexID(string name, int pokedexNumber)
	{
		this.name = name;
		this.pokedexNumber = pokedexNumber;
	}
}

//add ability, speciesDescription to baseStats
public class BaseStats
{
	public string name
    {
        get;
        private set;
    }

    public uint personalityValue
    {
        get;
        private set;
    }

    public List<string> types
    {
        get;
        private set;
    }

    public Nature nature
    {
        get;
        private set;
    }

    public List<Ability> abilities
    {
        get;
        private set;
    }

    public Dictionary<string, Stat> statDict
    {
        get;
        private set;
    }

    public int baseEXPYield
    {
        get;
        private set;
    }

    public int genderThreshold
    {
        get;
        private set;
    }

    public int eggCyclesTillHatch
    {
        get;
        private set;
    }

    public int baseFriendship
    {
        get;
        private set;
    }

    public string levelingRate
    {
        get;
        private set;
    }

    public List<string> eggGroups
    {
        get;
        private set;
    }

    public int safariZoneRate
    {
        get;
        private set;
    }

    public string pokedexColor
    {
        get;
        private set;
    }

    public float height
    {
        get;
        private set;
    }

    public float weight
    {
        get;
        private set;
    }

    public string bodyStyle
    {
        get;
        private set;
    }

    public string footprint
    {
        get;
        private set;
    }

    public BaseStats(string name, uint personalityValue, List<string> types, Nature nature, List<Ability> abilities, Dictionary<string, Stat> statDict,
        int baseEXPYield, int genderThreshold, int eggCyclesTillHatch, int baseFriendship, string levelingRate, List<string> eggGroups,
        string pokedexColor, float height, float weight)
	{
        this.name = name;
        this.personalityValue = personalityValue;
        this.types = types;
        this.nature = nature;
        this.abilities = abilities;
        this.statDict = statDict;
        this.baseEXPYield = baseEXPYield;
        this.genderThreshold = genderThreshold;
        this.eggCyclesTillHatch = eggCyclesTillHatch;
        this.baseFriendship = baseFriendship;
        this.levelingRate = levelingRate;
        this.eggGroups = eggGroups;
        this.safariZoneRate = 0; //not implemented yet
        this.pokedexColor = pokedexColor;
        this.height = height;
        this.weight = weight;
        this.bodyStyle = ""; //not implemented yet
        this.footprint = ""; //not implemented yet 
    }
}
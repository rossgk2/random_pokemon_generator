using System.Collections;

public class Pokemon
{
	public PokedexID pokedexID
	{
		get;
		private set;
	}
	
	public string[] type
	{
		get;
		private set;
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
	
	public Stat[] stats
	{
		get;
		private set;
	}
	
	public HP HP
	{
		get;
		private set;
	}
	
	public string gender
	{
		get;
		private set;
	}
	
	public Ability ability
	{
		get;
		private set;
	}
	
	public Nature nature
	{
		get;
		private set;
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
		
		personalityValue = GeneratePersonalityValue();
		
		type = (string) baseStats.GetStat("Type 2") == "" || baseStats.GetStat("Type 2") == null ? new string[] { (string)baseStats.GetStat("Type 1") } :
		new string[] { (string)baseStats.GetStat("Type 1"), (string)baseStats.GetStat("Type 2") };

        nature = Nature.natures[personalityValue % 27];

        HP = new HP("HP", level, (int)baseStats.GetStat("Base HP"), 0);
		
		string[] statObjectLabels = new string[] { "Attack", "Defense", "Special Attack", "Special Defense", "Speed" };
		stats = new Stat[5];
		for (int i = 0; i < stats.Length; i++)
		{
			int baseStat = ((int)baseStats.GetStat("Base " + statObjectLabels[i]));

            //decide whether the Pokemon's nature increases or decreases each stat
            string statName = statObjectLabels[i];
            float modifier = nature.ComputeModifier(statName);
            int evYield = baseStats.evYields.getEVForStat(statObjectLabels[i]);
            stats[i] = new Stat(statName, level, baseStat, evYield, modifier);
		}
		
		gender = personalityValue % 256 >= (int)baseStats.GetStat("Gender Threshold") ? "Male" : "Female";
		if ((Ability) baseStats.GetStat("Ability 2") == null)
		{
			ability = (Ability) baseStats.GetStat("Ability 1");
		}
		else
		{
			ability = personalityValue % 2 == 0 ? (Ability)baseStats.GetStat("Ability 1") : (Ability)baseStats.GetStat("Ability 2");
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
		
		string typeInfo = type.Length == 2 ? "Type: " + type[0] + ", " + type[1] : "Type: " + type[0];
		
		s += typeInfo + "\n" +
			"Nature: " + nature.name + "\n" +
				"Ability: " + ability.name + "\n" +
				"Shiny?: " + isShiny + "\n\n" +
				"Stats: " + "\n\n";
		
		foreach (Stat stat in stats)
		{
			s += "\t" + stat.ToString() + "\n\n\n";
		}
		
		return s;
	}
	
	uint GeneratePersonalityValue()
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
	public ArrayList baseStats
	{
		get;
		private set;
	}
	
	static readonly string[] labels = {"base hp", "base attack", "base defense", "base special attack",
		"base special defense", "base speed", "type 1", "type 2", "catch rate", "base exp yield", "item 1",
		"item 2", "gender threshold", "egg cycles to hatch", "base friendship", "leveling rate", "egg group 1",
		"egg group 2", "ability 1", "ability 2", "safari zone rate", "pokedex color", "height", "weight",
		"body style", "footprint"};
	
    public EVYield evYields
    {
        get;
        private set;
    }

	public BaseStats(ArrayList baseStats, EVYield evYields)
	{
		this.baseStats = baseStats;
        this.evYields = evYields;
	}
	
	public object GetStat(string query)
	{
		return baseStats[IndexOf(query)];
	}
	
	static int IndexOf(string query)
	{
		query = query.ToLower();
		
		int i = 0;
		foreach (string s in labels)
		{
			if (query == s)
			{
				return i;
			}
			i++;
		}
		return -1;
	}
}

public class EVYield
{
    static string[] statLabels = new string[] { "HP", "Attack", "Defense", "Special Attack", "Special Defense", "Speed" };
    int[] evYieldArray;

    public EVYield(int[] evYieldArray)
    {
        this.evYieldArray = evYieldArray;
    }

    public int getEVForStat(string stat)
    {
        int index = -1;
        
        for (int i = 0; i < statLabels.Length; ++i)
        {
            if (stat == statLabels[i])
            {
                index = i;
                break;
            }
        }
        return evYieldArray[index];
    }

    public override string ToString()
    {
        string ret = "[";
        for (int i = 0; i < statLabels.Length; ++ i)
        {
            ret += statLabels[i] + " EV: " + evYieldArray[i];
            if (i != statLabels.Length - 1)
            {
                ret += ", ";
            }
        }
        ret += "]";
        return ret;
    }
}
public class Nature
{
	public static readonly Nature[] natures = new Nature[]
	{
		new Nature("Hardy", "", "", "", ""),
		new Nature("Lonely", "Attack", "Defense", "Spicy", "Sour"),
		new Nature("Brave", "Attack", "Speed", "Spicy", "Sweet"),
		new Nature("Adamant", "Attack", "Special Attack", "Spicy", "Dry"),
		new Nature("Naughty", "Attack", "Special Defense", "Spicy", "Bitter"),
		new Nature("Bold", "Defense", "Attack", "Sour", "Spicy"),
		new Nature("Docile", "", "", "", ""),
		new Nature("Relaxed", "Defense", "Speed", "Sour", "Sweet"),
		new Nature("Impish", "Defense", "Special Attack", "Sour", "Dry"),
		new Nature("Lax", "Defense", "Special Defense", "Sour", "Bitter"),
		new Nature("Timid", "Speed", "Attack", "Sweet", "Spicy"),
		new Nature("Hasty", "Speed", "Defense", "Sweet", "Sour"),
		new Nature("Serious", "", "", "", ""),
		new Nature("Jolly", "Speed", "Special Attack", "Sweet", "Dry"),
		new Nature("Naive", "Speed", "Special Defense", "Sweet", "Bitter"),
		new Nature("Modest", "Special Attack", "Attack", "Dry", "Spicy"),
		new Nature("Mild", "Special Attack", "Defense", "Dry", "Sour"),
		new Nature("Quiet", "Special Attack", "Speed", "Dry", "Sweet"),
		new Nature("Bashful", "", "", "", ""),
		new Nature("Rash", "Special Attack", "Special Defense", "Dry", "Bitter"),
		new Nature("Calm", "Special Defense", "Attack", "Bitter", "Spicy"),
		new Nature("Gentle", "Special Defense", "Defense", "Bitter", "Sour"),
		new Nature("Sassy", "Special Defense", "Speed", "Bitter", "Sweet"),
		new Nature("Careful", "Special Defense", "Special Attack", "Bitter", "Dry"),
		new Nature("Quirky", "", "", "", "")
	};
	
	public string name
	{
		get;
		private set;
	}
	
	public string increasedStat
	{
		get;
		private set;
	}
	
	public string decreasedStat
	{
		get;
		private set;
	}
	
	public string favoriteFlavor
	{
		get;
		private set;
	}
	
	public string dislikedFlavor
	{
		get;
		private set;
	}
	
	public Nature(string name, string increasedStat, string decreasedStat, string favoriteFlavor, string dislikedFlavor)
	{
		this.name = name;
		this.increasedStat = increasedStat;
		this.decreasedStat = decreasedStat;
		this.favoriteFlavor = favoriteFlavor;
		this.dislikedFlavor = dislikedFlavor;
	}
	
	public static Nature GetNature(string name)
	{
		foreach (Nature n in natures)
		{
			if (n.name.ToLower() == name.ToLower())
			{
				return n;
			}
		}
		return null;
	}
	
	public override string ToString()
	{
		return name;
	}

    /*
     * Determines the modifier that this Nature object applies to the Stat stat.
     */
    public float ComputeModifier(string statName)
    {
        float modifier = 1f;
        if (statName == this.increasedStat.ToLower())
        {
            modifier = 1.1f;
        }
        else if (statName == this.decreasedStat.ToLower())
        {
            modifier = .9f;
        }
        return modifier;
    }


    /*
     *  Overload for above method in case I want to pass in a "Stat" object instead of its name.
     */ 
    public float ComputeModifier(Stat stat)
    {
        return ComputeModifier(stat.name);
    }
}
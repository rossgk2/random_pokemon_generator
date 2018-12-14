public class Ability
{
	public string name
	{
		get;
		private set;
	}

    public Ability(string name)
    {
        this.name = name;
    }

    public Ability()
    {
        name = "";
    }
}

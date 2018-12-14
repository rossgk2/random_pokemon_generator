using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

public class PokemonGeneratorMain
{
    public static Pokemon GenerateRandomPokemon(int pokedexNum)
    {
        //Get JSON file from PokeAPI that cooresponds to this pokedex number
        string json = GetPokemonJSON(pokedexNum);
        JObject obj = JObject.Parse(json);

        //parse data from the JSON file
        int pokemonLevel = new Random().Next(Pokemon.MAX_POKEMON_LEVEL + 1); //so the level is in [0, MAX_POKEMON_LEVEL] 
        string name = FirstLetterToUpper(obj.GetValue("name").ToString());
        uint personalityValue = Pokemon.GeneratePersonalityValue();
        List<string> types = GetTypes(obj);
        Nature nature = Nature.natures[personalityValue % 25];
        List<Ability> abilities = GetAbilities(obj);
        Dictionary<string, Stat> stats = GetStats(obj);
        int baseEXPYield = Int32.Parse(obj.GetValue("base_experience").ToString());
        int genderThreshold = 50; //placeholder
        int eggCyclesTillHatch = 10; //placeholder
        int baseFriendship = 70; //placeholder
        string levelingRate = "Medium Slow"; //placeholder
        List<string> eggGroups = GetEggGroups(obj); //not implemented yet
        string pokedexColor = ""; //placeholder
        float height = 5.5f; //placeholder
        float weight = (float)System.Convert.ToDouble(obj.GetValue("weight").ToString());

        //return the generated Pokemon
        BaseStats baseStats = new BaseStats(name, personalityValue, types, nature, abilities, stats, baseEXPYield, genderThreshold,
            eggCyclesTillHatch, baseFriendship, levelingRate, eggGroups, pokedexColor, height, weight);
        return new Pokemon(new PokedexID(name, pokedexNum), pokemonLevel, baseStats);
    }
    
    static string GetPokemonJSON(int pokedexNum)
    {
        return GetPokemonJSON(pokedexNum + "");
    }

    static string GetPokemonJSON(string pokemonName)
    {
        WebClient client = new WebClient();
        string url = "https://pokeapi.co/api/v2/pokemon/" + pokemonName;
        string response = client.DownloadString(url);
        return response;
    }

    static string FirstLetterToUpper(string str)
    {
        return str.Substring(0, 1).ToUpper() + str.Substring(1);
    }

    static List<string> GetEggGroups(JObject obj)
    {
        return new List<string>();
    }

    static Dictionary<string, Stat> GetStats(JObject obj)
    {
        int hp, attack, defense, specialAttack, specialDefense, speed;
        hp = attack = defense = specialAttack = specialDefense = speed = Int32.MaxValue;
        Dictionary<string, Stat> ret = new Dictionary<string, Stat>();

        //The spelling of the elements in this list MUST be this way, because this is the way PokeAPI
        //labels these stats.
        string[] pokeAPIStatLabels = { "hp", "attack", "defense", "special-attack", "special-defense", "speed" };
        string[] myStatLabels = { "HP", "Attack", "Defense", "Special Attack", "Special Defense", "Speed" };

        JArray stats = JArray.Parse(obj.GetValue("stats").ToString());
        foreach (JToken jt in stats)
        {
            string retrievedStatName = jt.Value<JToken>("stat").Value<string>("name");
            int retrievedStatValue = jt.Value<int>("base_stat");
            int retrievedEv = jt.Value<int>("effort");

            // Iterate through "statLabels". If "retrievedStatName" is one of the elements of "pokeAPIStatLabels", 
            // record the index.
            int index = -1;
            for (int i = 0; i < pokeAPIStatLabels.Length; ++ i)
            {
                if (retrievedStatName == pokeAPIStatLabels[i])
                {
                    index = i;
                    break;
                }
            }

            if (index == -1) { Console.WriteLine("Error in getStats()!");  }

            //Next, use this index to store the correct data for the stat that has been parsed.
            Stat st = new Stat(myStatLabels[index], retrievedStatValue, retrievedEv, 1);
            ret.Add(st.name, st);
        }

        return ret;
    }

    static List<string> GetTypes(JObject obj)
    {
        List<string> typesList = new List<string>();
        JArray types = JArray.Parse(obj.GetValue("types").ToString());
        foreach (JToken jt in types)
        {
            typesList.Add(FirstLetterToUpper(jt.Value<JObject>("type").GetValue("name").ToString()));
        }

        return typesList;
    }

    /*
     * A Pokemon has a list of possible abilities. In the Pokemon class, only one of these abilities is (randomly)
     * chosen for the pokemon.
     */
    static List<Ability> GetAbilities(JObject obj)
    {
        List<Ability> ret = new List<Ability>();
        JArray abilities = JArray.Parse(obj.GetValue("abilities").ToString());
        foreach (JToken ab in abilities)
        {
            ret.Add(new Ability(FirstLetterToUpper(ab.Value<JToken>("ability").Value<string>("name"))));
        }
        return ret;
    }

    static void Main()
    {
        Console.WriteLine("Enter how many Pokemon to randomly generate.");

        bool success = false;
        int numToGen = 0;
        while (!success)
        {
            string input = Console.ReadLine();
            success = Int32.TryParse(input, out numToGen);
            if (success)
            {
                break;
            }
            Console.WriteLine("Please enter an integer.");
        }

        //Generate the list of pokemon, then print the list, so that any debug output associated with
        //Pokemon generation is separated from desired output.
        List<Pokemon> pokemonList = new List<Pokemon>();
        for (int i = 0; i < numToGen; i ++)
        {
            int randomPokedexNum = new Random().Next(Pokemon.MAX_POKEDEX_NUMBER) + 1; //in the range [1, MAX_POKEDEX_NUM]
            pokemonList.Add(GenerateRandomPokemon(randomPokedexNum));
        }
        
        Console.WriteLine();
        Console.WriteLine(); //separate any debug output from actual program output
        string separator = "\n ====================================================================== \n";
        Console.WriteLine(separator);

        string output = string.Join(separator, pokemonList);
        Console.WriteLine(output);
    }
}
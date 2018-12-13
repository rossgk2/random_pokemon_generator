using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

public class PokemonTest
{
	Pokemon squirtle;

    // TO-DO:
    // Add a region field for the Pokemon, because catch rate depends on region.
    //
    // Move the heavy lifting that's done in the Pokemon constructor to the BaseStats class, and call
    // more user friendly getter methods in Pokemon constructor instead.

    static void Main()
    {
        int MAX_POKEDEX_NUM = 802;
        int num = new Random().Next(MAX_POKEDEX_NUM) + 1; //so num is in [1, MAX_POKEDEX_NUM] 
        string json = getPokemonJSON(num);
        JObject obj = JObject.Parse(json);

        string name = obj.GetValue("name").ToString();
        EVYield evYield;
        List <int> stats = getStats(obj, out evYield);
        Console.WriteLine(evYield);
        string[] statLabels = new string[] { "HP", "Attack", "Defense", "Special Attack", "Special Defense", "Speed" };Console.WriteLine(string.Join(", ", stats));
        List<string> types = getTypes(obj);
        int catchRate = -1; //depends on zone; implement later
        int baseEXPYield = Int32.Parse(obj.GetValue("base_experience").ToString());
        
        
       //Console.WriteLine("atk: {0}, def: {1}, satk: {2}, sdef: {3}, spd: {4}, hp: {5}", attack, defense, specialAttack, specialDefense, speed, hp);
        
    }

    static string FirstLetterToUpper(string str)
    {
        return str.Substring(0, 1).ToUpper() + str.Substring(1);
    }

    static List<int> getStats(JObject obj, out EVYield evYield)
    {
        int hp, attack, defense, specialAttack, specialDefense, speed;
        hp = attack = defense = specialAttack = specialDefense = speed = Int32.MaxValue;
        int[] statArray = new int[6];
        int[] evArray = new int[6];

        //The spelling of the elements in this list MUST be this way, because this is the way PokeAPI
        //labels these stats.
        string[] statLabels = { "hp", "attack", "defense", "special-attack", "special-defense", "speed" };

        JArray stats = JArray.Parse(obj.GetValue("stats").ToString());
        foreach (JToken jt in stats)
        {
            string retrievedStatName = jt.Value<JToken>("stat").Value<string>("name");
            int retrievedStatValue = jt.Value<int>("base_stat");
            int retrievedEv = jt.Value<int>("effort");

            // Iterate through "statLabels". If "retrievedStatName" is one of the elements of "statLabels", 
            // record the index.
            int index = -1;
            for (int i = 0; i < statLabels.Length; ++ i)
            {
                if (retrievedStatName == statLabels[i])
                {
                    index = i;
                    break;
                }
            }

            //Next, use this index to store the correct data for the stat that has been parsed.

            statArray[index] = retrievedStatValue;
            evArray[index] = retrievedEv;
        }

        evYield = new EVYield(evArray);
        List<int> statsList = new List<int>();
        statsList.AddRange(statArray);
        return statsList;
    }
     
    static List<string> getTypes(JObject obj)
    {
        List<string> typesList = new List<string>();
        JArray types = JArray.Parse(obj.GetValue("types").ToString());
        foreach (JToken jt in types)
        {
            typesList.Add(jt.Value<JObject>("type").GetValue("name").ToString());
        }

        return typesList;
    }


	static void Main2()
	{
        //this list will be used to create a BaseStats object, which specifies the following fields:
        //{"base hp", "base attack", "base defense", "base special attack",
        //"base special defense", "base speed", "type 1", "type 2", "catch rate", "base exp yield", "ev yield", "item 1",
		//"item 2", "gender threshold", "egg cycles to hatch", "base friendship", "leveling rate", "egg group 1",
		//"egg group 2", "ability 1", "ability 2", "safari zone rate", "pokedex color", "height", "weight",
		//"body style", "footprint"}
    ArrayList list = new ArrayList();
		list.AddRange(new int[] { 44, 48, 65, 50, 64, 43 });
		list.AddRange(new string[] { "Water", "" });
		list.AddRange(new int[] { 45, 66 });
		list.AddRange(new string[] { "1 Defense", "", "" });
		list.AddRange(new int[] { 31, 21, 70 });
		list.AddRange(new string[] { "Medium Slow", "Monster", "Water 1" });
		list.AddRange(new Ability[] { new Ability(), new Ability() }); //Abilities
		list.Add(-1);
		list.Add("Blue");
		list.Add(new float[] {.51f, 9f});
		list.AddRange(new string[] { "", "" });

        EVYield evy = new EVYield(new int[] { 0, 0, 0, 0, 0 });
		Pokemon squirtle = new Pokemon(new PokedexID("Squirtle", 7), 5, new BaseStats(list, evy));
		Console.WriteLine(squirtle.ToString());
	}

    static string getPokemonJSON(int pokedexNum)
    {
        WebClient client = new WebClient();
        string url = "https://pokeapi.co/api/v2/pokemon/" + pokedexNum;
        string response = client.DownloadString(url);
        return response;
    } 
     
}
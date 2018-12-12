using UnityEngine;
using System.Collections;

public class PokemonTest : MonoBehaviour {
	
	Pokemon squirtle;

	void Start()
	{
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

		Pokemon squirtle = new Pokemon(new PokedexID("Squirtle", 7), 5, new BaseStats(list));
		Debug.Log(squirtle.ToString());
	}
}
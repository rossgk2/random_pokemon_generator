## random_pokemon_generator
The Pokemon.exe file randomly generates Pokemon with stats, as would happen in a real Pokemon game. This program pulls data from the PokeAPI database (https://pokeapi.co/) and therefore requires an active internet connection.

# Note: to run the Pokemon.exe file, the Newtonsoft.Json.dll file must be in the same directory as the Pokemon.exe file.

Generated Pokemon have a PokedexID, which consists of the Pokemon's name and Pokedex number. They also have a level, which
is a number randomly generated between 1 and 100, a gender, a list of types (such as "Water", "Fire", "Bug", "Grass", or "Dragon"),
a nature attribute (such as "Rash" or "Gentle"), and an ability (such as "Torrent" or "Poison touch"). Pokemon also have a very, very 
small chance of being "shiny". All of these attributes could be said to be
'surface level' attributes, because, when these values are generated, there are a lot of other values behind the scenes that
determine what they are. Some of these 'behind the scenes' values are randomly generated, some of them aren't randomly generated 
and are specific to the Pokemon, and some of them are partially random and partially dependent on some qualities specific to the Pokemon.

In the code, the Pokemon class is the user interface between a programmer and a Pokemon that has been generated. Most of the
properties that someone who has played a Pokemon game would immediately see are immediately available to the Pokemon class. If more
in-detail, 'behind the scenes' types of properties need to be accessed, this can be done by accessing a Pokemon's BaseStat object.

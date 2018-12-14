# random_pokemon_generator
A C# program that randomly generates Pokemon with stats, as would happen in a real Pokemon game.

Generated Pokemon have a PokedexID, which consists of the Pokemon's name and Pokedex number. They also have a level, which
is a number randomly generated between 1 and 100, a gender, a list of types (such as "Water", "Fire", "Bug", "Grass", or "Dragon"),
a nature attribute (such as "Rash" or "Gentle"), and an ability (such as "Torrent" or "Poison touch"). Pokemon also have a very, very 
small chance of being "shiny" (this is a true/false field). All of these previously described attributes could be said to be
'surface level' attributes, because, when these values are generated, there are a lot of other values behind the scenes that
determine what they are. Some of these "behind the scenes" values are randomly generated, some of them aren't randomly generated 
and are specific to the Pokemon, and some of them are to extent, random but also dependent on some qualities specific to the Pokemon.

The Pokemon.exe file randomly generates as many Pokemon as the user specifies.

In the code, the Pokemon class is the user interface between a programmer and a Pokemon that has been generated. Most of the
properties that someone who has played a Pokemon game would immediately see are immediately available to the Pokemon class. If more
in-detail, "behind the scenes" types of properties need to be accessed, this can be done by accessing the Pokemon's BaseStat object.

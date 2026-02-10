using SuggestionMot.Core.Services;


ISuggestionService suggestionService = new SuggestionService();

List<string> mots = new List<string> { "gros", "gras", "graisse", "agressif", "go", "ros", "gro" };

Console.WriteLine("Test 1 : rechercher 'gros' avec N=2");
var result = suggestionService.GetSuggestions("gros", mots, 2);

result.ForEach(Console.WriteLine);


// Test supplémentaire
List<string> testListe = new List<string> { "chat", "chats", "chaton", "chien", "chin", "chatte" };
Console.WriteLine("\nTest 2 : rechercher 'chat' avec N=3");
var test2 = suggestionService.GetSuggestions("chat", testListe, 3);
test2.ForEach(Console.WriteLine);
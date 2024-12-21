using System.Text.RegularExpressions;

public static class RegexPromptGenerator
{
	public static string GeneratePrompt()
	{
		// Example: Generate a regex for Scryfall
		var patterns = new[] { "cmc>=5", "type:creature", "rarity:mythic" };
		var random = new Random();
		return patterns[random.Next(patterns.Length)];
	}
}

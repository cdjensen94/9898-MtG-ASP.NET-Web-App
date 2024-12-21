using ScryfallApi.Client.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
public class ScryfallService
{
	private readonly HttpClient _httpClient;

	public ScryfallService(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public async Task<string> SearchCardsAsync(string query)
	{
		string url = $"https://api.scryfall.com/cards/search?q={query}";
		var response = await _httpClient.GetAsync(url);
		response.EnsureSuccessStatusCode();

		return await response.Content.ReadAsStringAsync(); // Return raw JSON response
	}


	public async Task<List<string>> CreateBoosterPackAsync(ScryfallService service)
	{
		var prompt = RegexPromptGenerator.GeneratePrompt(); // Generate query
		var response = await service.SearchCardsAsync(prompt); // Fetch raw JSON

		var cards = ParseCardsFromResponse(response); // Get card names
		return cards.Take(15).ToList(); // Return up to 15 card names
	}



	// Dummy parser method
	private List<string> ParseCardsFromResponse(string response)
	{
		// Deserialize the JSON response into a ScryfallResponse object
		var scryfallResponse = JsonSerializer.Deserialize<ScryfallResponse>(response);

		// Return a list of card names, or an empty list if no cards are found
		return scryfallResponse?.Data?.Select(card => card.Name).ToList() ?? new List<string>();
	}


}
public class ScryfallResponse
{
	public List<Card> Data { get; set; }
}

public class Card
{
	public string Name { get; set; }
	public ImageUris Image_Uris { get; set; }
	public string Type_Line { get; set; }
}

public class ImageUris
{
	public string Normal { get; set; }
}



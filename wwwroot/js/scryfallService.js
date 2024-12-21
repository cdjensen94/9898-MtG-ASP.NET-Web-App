export async function fetchScryfallCards(query) {
    try {
        const response = await fetch(`https://api.scryfall.com/cards/search?q=${encodeURIComponent(query)}`);
        if (!response.ok) throw new Error('Failed to fetch cards.');
        const data = await response.json();
        return data.data;
    } catch (error) {
        console.error('Error fetching cards:', error);
        return [];
    }
}

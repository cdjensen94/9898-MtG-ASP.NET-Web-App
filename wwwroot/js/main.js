import { generateRandomQuery } from './queryGenerators.js';
import { fetchScryfallCards } from './scryfallService.js';
import { displayResults, logCardNames } from './uiHandler.js';

document.addEventListener('DOMContentLoaded', () => {
    const searchForm = document.getElementById('search-form');
    const resultsContainer = 'results';
    const logId = 'log';

    searchForm.addEventListener('submit', async (event) => {
        event.preventDefault();
        const query = generateRandomQuery();
        const cards = await fetchScryfallCards(query);
        displayResults(cards, resultsContainer);
        logCardNames(cards, logId);
    });
});

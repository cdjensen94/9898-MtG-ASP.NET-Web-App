import { generateScryfallRegEx } from './queryGenerators.js';
import { displayResults } from './results.js';

export async function generateChaosBoosterPack() {
    const query = generateScryfallRegEx('chaos');
    try {
        const response = await fetch(`/api/booster?query=${encodeURIComponent(query)}`);
        const cards = await response.json();
        displayResults(cards);
    } catch (error) {
        console.error('Error generating chaos booster pack:', error);
    }
}

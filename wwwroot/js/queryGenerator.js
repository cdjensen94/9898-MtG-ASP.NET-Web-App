async function loadQueryData() {
    try {
        const response = await fetch('../data/queryData.json');
        if (!response.ok) throw new Error('Failed to load query data');
        return await response.json();
    } catch (error) {
        console.error('Error loading query data:', error);
        return null;
    }
}

// Use the loaded JSON
async function initializeQueryGenerator() {
    const queryData = await loadQueryData();
    if (!queryData) return;

    const keys = queryData.keys;
    const values = queryData.values;

    console.log('Keys:', keys);
    console.log('Values:', values);
}

initializeQueryGenerator();

import fs from 'fs';

// Load the JSON file
const queryData = JSON.parse(fs.readFileSync('../data/queryData.json', 'utf-8'));

export function generateRandomQuery() {
    const combos = queryData.combos;

    // Ensure the combos array is valid and non-empty
    if (!combos || combos.length === 0) {
        throw new Error("The combos array is missing or empty in the JSON data.");
    }

    // Select a random query from the combos array
    const randomQuery = combos[Math.floor(Math.random() * combos.length)];

    return randomQuery;
}

// Example usage
console.log(generateRandomQuery());


export function generateScryfallRegEx(query) {
    return `t:${query}`;
}

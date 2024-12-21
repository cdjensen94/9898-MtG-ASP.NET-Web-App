// generateChaosBoosterPack.js

// Function to generate three chaos-themed Scryfall queries
function generateChaosQueries() {
    return [
        generateScryfallRegEx(),
        generateScryfallRegEx(),
        generateScryfallRegEx()
    ];
}

// Function to fetch and process results from Scryfall API
function fetchChaosBoosterPacks() {
    var queries = generateChaosQueries();
    const promises = queries.map(query => searchScryfall(query));

    return Promise.all(promises).then(results => {
        return results.flat();
    });
}

// Function to generate a random Scryfall regex query
function generateScryfallRegEx() {
    const regexPatterns = [
        '.*dragon.*',
        '.*elf.*',
        '.*goblin.*',
        '.*wizard.*',
        '.*zombie.*'
    ];

    // Select a random pattern
    const randomPattern = regexPatterns[Math.floor(Math.random() * regexPatterns.length)];

    // Update regex display on the page
    const regexDisplay = document.getElementById('regex-display');
    if (regexDisplay) {
        regexDisplay.value = randomPattern;
    }

    return randomPattern;
}

// Function to fetch card names based on the Scryfall regex query
function fetchCardNames(query) {
    // Simulated card names (replace with actual API fetch logic)
    const simulatedCardNames = ['Card 1', 'Card 2', 'Card 3']; // Example placeholder
    console.log(`Fetching cards for query: ${query}`); // Debug log
    return simulatedCardNames;
}

// Function to display card names in the results container
function displayResults(cardNames) {
    const resultsContainer = document.getElementById('results');
    if (!resultsContainer) {
        console.error('Results container not found.');
        return;
    }

    // Clear previous results
    resultsContainer.innerHTML = '';

    // Add header
    const header = document.createElement('h2');
    header.textContent = 'Results:';
    resultsContainer.appendChild(header);

    // Add card names
    cardNames.forEach((cardName) => {
        const resultItem = document.createElement('li');
        resultItem.textContent = cardName;
        resultsContainer.appendChild(resultItem);
    });

    // Add footer
    const footer = document.createElement('p');
    footer.textContent = 'End of results.';
    resultsContainer.appendChild(footer);
}

// Function to generate a new Scryfall regex query and display results
function generateNewQuery() {
    const newQuery = generateScryfallRegEx();
    console.log('Generated Scryfall regex query:', newQuery);

    const cardNames = fetchCardNames(newQuery);

    console.log('Fetched Card Names:', cardNames);
    displayResults(cardNames);
}

// Initialize the page with a default query
window.onload = () => {
    console.log('Page loaded. Generating initial query...');
    generateNewQuery();
};

// Generate a new Scryfall regex query every 5 minutes
setInterval(() => {
    console.log('Generating a new query...');
    generateNewQuery();
}, 5 * 60 * 1000);

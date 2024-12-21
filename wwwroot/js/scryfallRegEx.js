// scryfallRegEx.js


/*


*/



// Ensure DOM is loaded before running scripts that manipulate the DOM
document.addEventListener('DOMContentLoaded', () => {
    // DOM References
    const resultsSection = document.getElementById('results');
    const resultsList = document.getElementById('results-list');
    const logArea = document.getElementById('log');
    const summary = document.getElementById('summary');
    const feedbackInput = document.getElementById('feedback');
    const suggestInput = document.getElementById('suggest');
    const searchForm = document.getElementById('search-form');
    const searchInput = document.getElementById('search-input');
    const generateButton = document.getElementById('generate-button');
    const regexDisplay = document.getElementById('regex-display');
    const clearButton = document.getElementById('clear-button');
    const chaosButton = document.getElementById('chaos-button');
    const timerDisplay = document.getElementById('timer');
    const feedbackForm = document.getElementById('feedback-form');
    const suggestForm = document.getElementById('suggest-form');
    const suggestContainer = document.getElementById('suggest-container');

    // Check if all critical elements exist
    if (!resultsSection || !resultsList || !logArea) {
        console.error('Some core elements (results, results-list, log) are missing from the DOM.');
    }

    // =====================================
    // Helper Functions
    // =====================================

    // Fetch and search Scryfall
    function searchScryfall(query) {
        const apiUrl = `https://api.scryfall.com/cards/search?q=${encodeURIComponent(query)}`;
        fetch(apiUrl)
            .then(response => {
                if (!response.ok) {
                    throw new Error(`API request failed with status ${response.status}`);
                }
                return response.json();
            })
            .then(data => {
                if (!data.data || data.data.length === 0) {
                    console.warn('No cards found for the query:', query);
                    return;
                }
                const selectedResults = selectRandomResults(data.data);
                displayResults(selectedResults);
                logCardNames(selectedResults, 'Search Results');
            })
            .catch(error => {
                console.error('Error fetching cards:', error.message);
            });
    }


    // Randomly select 15 or 20 results
    function selectRandomResults(results) {
        const count = Math.random() < 0.5 ? 15 : 20;
        const shuffled = results.sort(() => 0.5 - Math.random());
        return shuffled.slice(0, Math.min(count, results.length));
    }

    // Display results in the DOM
    function displayResults(results) {
        const resultsSection = document.getElementById('results');
        const resultsList = document.getElementById('results-list');
        if (!resultsSection || !resultsList) return;

        resultsSection.innerHTML = ''; // Clear previous results
        resultsList.value = `Number of results: ${results.length}\n\n`;

        results.forEach(card => {
            const cardDiv = document.createElement('div');
            cardDiv.classList.add('card-container');

            const cardHeader = document.createElement('h2');
            cardHeader.textContent = card.name;
            cardDiv.appendChild(cardHeader);

            const cardImg = document.createElement('img');
            if (card.image_uris && card.image_uris.normal) {
                cardImg.src = card.image_uris.normal;
                cardImg.alt = card.name;
            } else {
                cardImg.alt = 'Image not available';
            }
            cardDiv.appendChild(cardImg);

            const cardFooter = document.createElement('p');
            cardFooter.textContent = `${card.type_line || ''} - ${card.oracle_text || ''}`;
            cardDiv.appendChild(cardFooter);

            resultsSection.appendChild(cardDiv);

            // Append card name to results list
            resultsList.value += `${card.name}\n`;
        });
    }


    // Log card names to the textarea and console
    function logCardNames(results, label) {
        const logArea = document.getElementById('log');
        if (!logArea) return;

        logArea.value += `${label}:\n`;
        results.forEach(card => {
            logArea.value += `${card.name}\n`;
            console.log(card.name);
        });
        logArea.value += '\n---\n';
    }


    // Generate a random Scryfall regex query
    async function generateScryfallRegEx() {
        try {
            // Load data from queryData.json
            const response = await fetch('../data/queryData.json');
            if (!response.ok) throw new Error('Failed to load query data.');
            const queryData = await response.json();

            const keys = queryData.keys; // Extract available keys
            const values = queryData.values; // Extract value arrays

            // Step 1: Randomly pick a subset of keys
            const numKeys = Math.floor(Math.random() * keys.length) + 1; // Pick between 1 and all keys
            const selectedKeys = keys
                .sort(() => Math.random() - 0.5) // Shuffle keys
                .slice(0, numKeys); // Take a random subset

            // Step 2: Assign random values for the selected keys
            const queryParts = selectedKeys.map((key, index) => {
                const valueArray = values[index]; // Get values for the key
                const randomValue = valueArray[Math.floor(Math.random() * valueArray.length)];
                return `${key}:${randomValue}`;
            });

            // Step 3: Build the query string
            return queryParts.join(' ');
        } catch (error) {
            console.error('Error generating Scryfall RegEx query:', error);
            return '';
        }
    }


    // Clear results and forms
    function clearResults() {
        if (resultsSection) resultsSection.innerHTML = '';
        if (resultsList) resultsList.value = '';
        if (logArea) logArea.value = '';
        if (summary) summary.value = '';
        if (feedbackInput) feedbackInput.value = '';
        if (suggestInput) suggestInput.value = '';
    }

    // Generate Chaos Booster Pack
    function generateChaosBoosterPack(retries = 3) {
        if (retries <= 0) {
            console.error('Max retries reached for generating Chaos Booster Pack.');
            return;
        }

        const query = generateScryfallRegEx();
        fetch(`https://api.scryfall.com/cards/search?q=${encodeURIComponent(query)}`)
            .then(response => response.json())
            .then(data => {
                if (!data.data || data.data.length < 15) {
                    console.warn('Insufficient results. Retrying...');
                    generateChaosBoosterPack(retries - 1);
                    return;
                }
                const selectedResults = selectRandomResults(data.data);
                logCardNames(selectedResults, 'Chaos Booster Results');
                displayResults(selectedResults);
            })
            .catch(error => {
                console.error('Error generating Chaos Booster Pack:', error.message);
            });
    }


    // Display suggestions
    function displaySuggestions(results) {
        if (!suggestContainer) return;
        suggestContainer.innerHTML = '';
        results.forEach(card => {
            const suggestDiv = document.createElement('div');
            suggestDiv.classList.add('suggestion');

            const suggestHeader = document.createElement('h3');
            suggestHeader.textContent = `Suggestion for ${card.name}`;
            suggestDiv.appendChild(suggestHeader);

            const suggestText = document.createElement('p');
            suggestText.textContent = `Consider using ${card.name} in a deck with similar cards.`;
            suggestDiv.appendChild(suggestText);

            suggestContainer.appendChild(suggestDiv);
        });
    }

    // Countdown Timer
    function startCountdown(duration, display) {
        let timer = duration, minutes, seconds;
        setInterval(() => {
            minutes = parseInt(timer / 60, 10);
            seconds = parseInt(timer % 60, 10);

            minutes = minutes < 10 ? '0' + minutes : minutes;
            seconds = seconds < 10 ? '0' + seconds : seconds;

            display.textContent = minutes + ':' + seconds;

            if (--timer < 0) {
                timer = duration;
            }
        }, 1000);
    }

    // Theme changing every 15 minutes
    function changeTheme() {
        const themes = [
            { background: '#000000', color: '#CC0000' },
            { background: '#333333', color: '#FFFFFF' },
            { background: '#FFFFFF', color: '#000000' },
            { background: '#610000', color: '#FF3131' }
        ];
        let currentThemeIndex = 0;

        setInterval(() => {
            currentThemeIndex = (currentThemeIndex + 1) % themes.length;
            document.body.style.backgroundColor = themes[currentThemeIndex].background;
            document.body.style.color = themes[currentThemeIndex].color;
        }, 900000); // 15 minutes
    }

    // Event Listeners
    if (searchForm && searchInput) {
        searchForm.addEventListener('submit', function (event) {
            event.preventDefault();
            searchScryfall(searchInput.value);
        });
    }

    if (generateButton && regexDisplay) {
        generateButton.addEventListener('click', function () {
            const query = generateScryfallRegEx();
            regexDisplay.value = query;
            searchScryfall(query);
        });
    }

    if (clearButton) {
        clearButton.addEventListener('click', clearResults);
    }

    if (chaosButton) {
        chaosButton.addEventListener('click', generateChaosBoosterPack);
    }

    // Start timer and theme
    if (timerDisplay) {
        const duration = 60 * 30; // 30 minutes
        startCountdown(duration, timerDisplay);
    }
    changeTheme();

    // Generate initial Scryfall regex query
    if (generateButton) {
        generateButton.click();
    }

    // Auto-focus search input on page load
    if (searchInput) {
        searchInput.focus();
    }

    // Feedback Form Submission
    if (feedbackForm && feedbackInput) {
        feedbackForm.addEventListener('submit', function (event) {
            event.preventDefault();
            const feedback = feedbackInput.value.trim();
            if (!feedback) {
                alert('Please provide your feedback before submitting.');
                return;
            }
            console.log('Feedback submitted:', feedback);
            feedbackInput.value = '';
            alert('Thank you for your feedback!');
        });
    }

    // Suggestion Form Submission
    if (suggestForm && suggestInput) {
        suggestForm.addEventListener('submit', function (event) {
            event.preventDefault();
            const suggestion = suggestInput.value.trim();
            if (!suggestion) {
                alert('Please provide a suggestion before submitting.');
                return;
            }
            console.log('Suggestion submitted:', suggestion);
            suggestInput.value = '';
            alert('Thank you for your suggestion!');
        });
    }

    // Display current date and time
    const currentDateEl = document.getElementById('current-date');
    const currentTimeEl = document.getElementById('current-time');
    if (currentDateEl && currentTimeEl) {
        const currentDate = new Date();
        currentDateEl.textContent = currentDate.toLocaleDateString();
        currentTimeEl.textContent = currentDate.toLocaleTimeString();
    }

    // Toggling various elements
    function setupToggle(buttonId, targetId) {
        const button = document.getElementById(buttonId);
        const target = document.getElementById(targetId);
        if (button && target) {
            button.addEventListener('click', () => {
                target.classList.toggle('hidden');
            });
        }
    }

    // Basic toggles
    setupToggle('dark-mode-toggle', null);
    document.getElementById('dark-mode-toggle')?.addEventListener('click', function () {
        document.body.classList.toggle('dark-mode');
    });

    setupToggle('advanced-toggle', 'advanced-options');
    setupToggle('suggest-toggle', 'suggest-container');
    setupToggle('chaos-toggle', 'results');
    setupToggle('log-toggle', 'log');
    setupToggle('summary-toggle', 'summary');
    setupToggle('results-list-toggle', 'results-list');
    setupToggle('feedback-toggle', 'feedback-form');
    setupToggle('suggest-toggle', 'suggest-form');
    setupToggle('current-date-toggle', 'current-date');
    setupToggle('current-date-toggle', 'current-time');
    setupToggle('dark-mode-toggle-toggle', 'dark-mode-toggle');
    setupToggle('advanced-toggle-toggle', 'advanced-toggle');
    setupToggle('suggest-toggle-toggle', 'suggest-toggle');
    setupToggle('chaos-toggle-toggle', 'chaos-toggle');

    // Fetch and display chaos booster packs
    function generateChaosQueries() {
        return [
            'type:creature+cmc<=3+power>=3',
            'type:instant+cmc>=4+color:red',
            'type:sorcery+cmc<=2+color:black'
        ];
    }

    function fetchChaosBoosterPacks() {
        const queries = generateChaosQueries();
        queries.forEach(query => {
            fetch('https://api.scryfall.com/cards/search?q=' + encodeURIComponent(query))
                .then(response => response.json())
                .then(data => {
                    const results = data.data.slice(0, 20); // Limit to 20 results
                    logCardNames(results);
                    displayResults(results);
                })
                .catch(error => console.error('Error:', error));
        });
    }

    // Add card styling
    const style = document.createElement('style');
    style.textContent = 
    .card-container {
        font-family: 'AR CENA';
        font-size: 24px;
        border: 2px solid black;
        padding: 10px;
        margin: 10px;
        transition: transform 0.3s, background-color 0.3s;
    }
    .card-header, .card-footer {
        transition: transform 0.3s, background-color 0.3s;
    }
    .card-container:hover {
        transform: scale(1.05);
    }
    .card-header:hover, .card-footer:hover {
        transform: scale(1.1);
        background-color: yellow;
    }
    ;
    document.head.appendChild(style);
    function generateQueryCombinations(keys, values) {
        const allCombinations = [];

        // Helper function to recursively generate combinations
        function combine(current, remainingKeys, remainingValues) {
            if (remainingKeys.length === 0) {
                if (current.length > 0) {
                    allCombinations.push(current.join(' '));
                }
                return;
            }

            const nextKey = remainingKeys[0];
            const nextValues = remainingValues[0];

            // For each value of the next key, create a new combination
            nextValues.forEach(value => {
                combine([...current, `${nextKey}:${value}`], remainingKeys.slice(1), remainingValues.slice(1));
            });

            // Optionally skip the current key
            combine(current, remainingKeys.slice(1), remainingValues.slice(1));
        }

        // Start recursion
        combine([], keys, values);
        return allCombinations;
    }
    const allQueries = generateQueryCombinations(keys, values);

    // Convert combinations to a random selection mechanism
    const randomQuery = allQueries[Math.floor(Math.random() * allQueries.length)];

    // Write the results to a .txt file
    const fs = require('fs');
    fs.writeFileSync('all_combinations.txt', allQueries.join('\n'), 'utf8');

    console.log(`Generated ${allQueries.length} combinations.`);
    console.log(`Random query: ${randomQuery}`);

    // Call to fetch chaos booster packs initially
    fetchChaosBoosterPacks();
    generateQueryCombinations();
});

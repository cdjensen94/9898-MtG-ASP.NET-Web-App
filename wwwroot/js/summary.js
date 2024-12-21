// summary.js

// Function to display a summary of the results
function displaySummary(results) {
    const summaryContainer = document.getElementById('summary');
    summaryContainer.value = `Total cards: ${results.length}\n`;

    const types = results.reduce((acc, result) => {
        const type = result.type_line.split(' — ')[0];
        acc[type] = (acc[type] || 0) + 1;
        return acc;
    }, {});

    for (const [type, count] of Object.entries(types)) {
        summaryContainer.value += `${type}: ${count}\n`;
    }
}

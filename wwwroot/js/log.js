// log.js

// Function to log card names to the console with a custom label
function logCardNames(results, label) {
    if (label === undefined) {
        label = 'Card Names';
    }
    console.log(`${label}:`);
    results.forEach(result => {
        console.log(result.name);
    });
}

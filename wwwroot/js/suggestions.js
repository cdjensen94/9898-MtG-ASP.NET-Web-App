// suggestions.js

// Function to handle suggestion form submission
document.getElementById('suggest-form').addEventListener('submit', function(event) {
    event.preventDefault();
    const suggestion = document.getElementById('suggest').value;
    console.log('Suggestion submitted:', suggestion);
    alert('Thank you for your suggestion!');
});

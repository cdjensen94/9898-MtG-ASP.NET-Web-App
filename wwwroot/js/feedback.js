document.getElementById('feedback-form')?.addEventListener('submit', (event) => {
    event.preventDefault();
    const feedback = document.getElementById('feedback')?.value.trim();
    if (!feedback) {
        alert('Please provide your feedback before submitting.');
        return;
    }
    alert('Thank you for your feedback!');
});

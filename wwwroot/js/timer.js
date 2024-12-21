// Function to start the countdown timer
function startCountdown(duration, display) {
    let timer = duration, minutes, seconds;

    const interval = setInterval(() => {
        minutes = parseInt(timer / 60, 10);
        seconds = parseInt(timer % 60, 10);

        minutes = minutes < 10 ? "0" + minutes : minutes;
        seconds = seconds < 10 ? "0" + seconds : seconds;

        // Update the display
        if (display) {
            display.textContent = `${minutes}:${seconds}`;
        }

        // Decrement the timer
        if (--timer < 0) {
            clearInterval(interval); // Stop the timer
            if (display) {
                display.textContent = "00:00"; // Ensure it shows 00:00 when finished
            }
        }
    }, 1000);
}

// Start the countdown timer on page load
window.onload = function () {
    const thirtyMinutes = 60 * 30; // 30 minutes in seconds
    const display = document.querySelector('#timer');

    if (display) {
        startCountdown(thirtyMinutes, display);
    } else {
        console.error('Timer display element not found.');
    }
};

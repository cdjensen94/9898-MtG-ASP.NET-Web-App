export function autoRefresh(intervalMinutes) {
    setInterval(() => {
        location.reload();
    }, intervalMinutes * 60 * 1000);
}

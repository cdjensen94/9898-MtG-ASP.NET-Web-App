export function displayResults(cards, containerId) {
    const container = document.getElementById(containerId);
    if (!container) return;

    container.innerHTML = cards.map(card => `
        <div class="card-container">
            <h2>${card.name}</h2>
            <img src="${card.image_uris?.normal}" alt="${card.name}" />
            <p>${card.type_line || ''} - ${card.oracle_text || ''}</p>
        </div>
    `).join('');
}

export function logCardNames(cards, logId) {
    const log = document.getElementById(logId);
    if (!log) return;

    log.value = cards.map(card => card.name).join('\n');
}

export function displayResults(cards) {
    const container = document.getElementById('results');
    container.innerHTML = cards.map((card) => `
        <div class="card">
            <img src="${card.image_uris?.normal}" alt="${card.name}" />
            <h2>${card.name}</h2>
            <p>${card.type_line}</p>
        </div>
    `).join('');
}

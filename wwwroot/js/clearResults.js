export function clearResults(...elementIds) {
    elementIds.forEach((id) => {
        const element = document.getElementById(id);
        if (element) element.innerHTML = '';
    });
}

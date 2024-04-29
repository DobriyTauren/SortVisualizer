// wwwroot/js/svgInterop.js
window.svgInterop = {
    getSvgWidth: function () {
        var svg = document.getElementsByClassName('sort-block-container')[0];
        return svg.clientWidth; // возвращает ширину SVG
    }
};

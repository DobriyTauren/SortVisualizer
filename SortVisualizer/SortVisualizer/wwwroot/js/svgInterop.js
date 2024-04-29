// wwwroot/js/svgInterop.js
window.svgInterop = {
    getSvgWidth: function (elemNum) {
        var svg = document.getElementsByClassName('sort-block-container')[elemNum];
        return svg.clientWidth; // возвращает ширину SVG
    },
    getSvgHeight: function (elemNum) {
        var svg = document.getElementsByClassName('sort-block-container')[elemNum];
        return svg.clientHeight; // возвращает высоту SVG
    }
};

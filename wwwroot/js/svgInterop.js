// wwwroot/js/svgInterop.js
window.svgInterop = {
    getSvgWidth: function (elemNum) {
        var svg = document.getElementsByClassName('svg-container')[elemNum];
        return svg.clientWidth; // возвращает ширину SVG
    },
    getSvgHeight: function (elemNum) {
        var svg = document.getElementsByClassName('svg-container')[elemNum];
        return svg.clientHeight; // возвращает высоту SVG
    }
};

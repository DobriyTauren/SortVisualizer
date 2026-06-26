window.addClickOutsideEventListener = (dotNetObject) => {
    document.addEventListener('click', (event) => {
        if (!event.target.closest('.dropdown')) {
            dotNetObject.invokeMethodAsync('CloseDropdown');
        }
    });
};

window.removeClickOutsideEventListener = (dotNetObject) => {
    document.removeEventListener('click', (event) => {
        if (!event.target.closest('.dropdown')) {
            dotNetObject.invokeMethodAsync('CloseDropdown');
        }
    });
};

// Handle closing the dialog with escape key 
window.keypress = {
    init: dotnetHelper => {
        window.addEventListener('keydown',

            function (e) {

                if (e.key === 'Escape') {
                    dotnetHelper.invokeMethodAsync("CloseModal");

                }
            });
    }
};


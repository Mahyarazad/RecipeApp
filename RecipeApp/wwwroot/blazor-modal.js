
// Handle closing the dialog with escape key 
window.keypress = {
    init: dotnetHelper => {
        window.addEventListener('keydown',
            
            function (e) {
                $("textarea").each(function () {
                    this.setAttribute("style", "height:" + (this.scrollHeight) + "px;overflow-y:hidden;");
                }).on("input", function () {
                    this.style.height = "auto";
                    this.style.height = (this.scrollHeight) + "px";
                });
                if (e.key === 'Escape') {
                    dotnetHelper.invokeMethodAsync("CloseModal");

                }
            });
    }
};



function AutoHeight() {
    $("textarea").each(function () {
        this.setAttribute("style", "height:" + (this.scrollHeight) + "px;overflow-y:hidden;");
    }).on("input", function () {
        this.style.height = "auto";
        this.style.height = (this.scrollHeight) + "px";
    });
}



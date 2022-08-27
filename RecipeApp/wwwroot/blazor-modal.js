
// Handle closing the dialog with escape key 
window.keypress = {
    init: dotnetHelper => {
        window.addEventListener('keydown',
            
            function (e) {
                $("textarea").each(function () {
                    if (this.scrollHeight > 300) {
                        this.setAttribute("style", "height:" + 300 + "px;overflow-y:scroll;");
                    } else {
                        this.setAttribute("style", "height:" + (this.scrollHeight) + "px;overflow-y:hidden;");
                    }
                    
                }).on("input", function () {
                    if (this.scrollHeight > 300) {
                        this.style.height = "auto";
                        this.style.height = 300 + "px";
                    } else {
                        this.style.height = "auto";
                        this.style.height = (this.scrollHeight) + "px";
                    }
                });
                if (e.key === 'Escape') {
                    dotnetHelper.invokeMethodAsync("CloseModal");

                }
            });
    }
};



function AutoHeight() {
    $("textarea").each(function () {
        if (this.scrollHeight > 300) {
            this.setAttribute("style", "height:" + 300 + "px;overflow-y:scroll;");
        } else {
            this.setAttribute("style", "height:" + (this.scrollHeight) + "px;overflow-y:hidden;");
        }

    }).on("input", function () {
        if (this.scrollHeight > 300) {
            this.style.height = "auto";
            this.style.height = 300 + "px";
        } else {
            this.style.height = "auto";
            this.style.height = (this.scrollHeight) + "px";
        }
    });
}

function DeleteTagPrompt() {
    return Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        return result.value;
    });
}


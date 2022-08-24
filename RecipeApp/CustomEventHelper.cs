using Microsoft.JSInterop;

namespace RecipeApp;

public class MessageUpdateInvokeHelper
{
    private Action action;

    public MessageUpdateInvokeHelper(Action action)
    {
        this.action = action;
    }

    [JSInvokable("RecipeApp")]
    public void CloseModal()
    {
        action.Invoke();
    }
}
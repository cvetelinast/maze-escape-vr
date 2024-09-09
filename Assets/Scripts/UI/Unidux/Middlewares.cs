using Unidux;

public class Middlewares {

    public void ApplyMiddleware<TAction>(IStoreObject store, TAction action)
    {
        ApplyMiddleware(store, action);
    }

    private void ApplyMiddleware(UniduxState state, Action action)
    {
        switch (action)
        {
            case LoadMenuScene loadMenuScene:
                MiddlewareActions.MiddlewareActionsInstance.LoadMenuScene();
                break;
            case OnPlayButtonClicked onPlayButtonClicked:
                MiddlewareActions.MiddlewareActionsInstance.OnPlayButtonClicked();
                break;
            case LoadGameScene loadGameScene:
                MiddlewareActions.MiddlewareActionsInstance.LoadGameScene();
                break;
            default:
                break;
        };
    }
}

public class MiddlewareActions {

    private static MiddlewareActions _middlewareActionsInstance = null;

    public static MiddlewareActions MiddlewareActionsInstance {
        get {
            return _middlewareActionsInstance ??= new MiddlewareActions();
        }
    }

    public MiddlewareActions()
    {
    }

    public event System.Action LoadMenuSceneAction;
    public event System.Action OnPlayButtonClickedAction;
    public event System.Action LoadGameSceneAction;

    public void LoadMenuScene()
    {
        LoadMenuSceneAction?.Invoke();
    }

    public void OnPlayButtonClicked()
    {
        OnPlayButtonClickedAction?.Invoke();
    }
    public void LoadGameScene()
    {
        LoadGameSceneAction?.Invoke();
    }

}


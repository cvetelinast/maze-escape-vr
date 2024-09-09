using System;
using Unidux;

public class Reducer : ReducerBase<UniduxState, Action> {
    public override UniduxState Reduce(UniduxState state, Action action)
    {
        return action switch
        {
            LoadMenuScene loadMenuScene => TransitionOnLoadMenuScene(state, loadMenuScene),
            OnPlayButtonClicked onPlayButtonClicked => TransitionOnPlayButtonClicked(state, onPlayButtonClicked),
            LoadGameScene loadGameScene => TransitionOnLoadGameScene(state, loadGameScene),
            _ => throw ComposeException(state, action),

        };
    }

    private UniduxState TransitionOnLoadMenuScene(UniduxState state, LoadMenuScene loadMenuScene)
    {
        switch (state)
        {
            case InitialState _:
                {
                    return new MenuSceneState(state.payload);
                }
            default: throw ComposeException(state, loadMenuScene);
        }
    }

    private UniduxState TransitionOnPlayButtonClicked(UniduxState state, OnPlayButtonClicked onPlayButtonClicked)
    {
        switch (state)
        {
            case MenuSceneState _:
                {
                    return new LoadingState(state.payload);
                }
            default: throw ComposeException(state, onPlayButtonClicked);
        }
    }

    private UniduxState TransitionOnLoadGameScene(UniduxState state, LoadGameScene loadGameScene)
    {
        switch (state)
        {
            case LoadingState _:
                {
                    return new GameSceneState(state.payload);
                }
            default: throw ComposeException(state, loadGameScene);
        }
    }

    private SystemException ComposeException(UniduxState state, Action action) =>
                new InvalidOperationException("The state is " + state + ", but the action " + action + " is not allowed.");
}


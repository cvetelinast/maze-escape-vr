using UniRx;
using Unidux;
using UnityEngine;

public sealed class UniduxMaze : SingletonMonoBehaviour<UniduxMaze>, IStoreAccessor {
    public TextAsset InitialStateJson;

    private Store<UniduxState> _store;

    public IStoreObject StoreObject {
        get { return Store; }
    }

    public static UniduxState State {
        get { return Store.State; }
    }

    public static Subject<UniduxState> Subject {
        get { return Store.Subject; }
    }

    private static UniduxState InitialState {
        get {
            return Instance.InitialStateJson != null
                ? JsonUtility.FromJson<UniduxState>(Instance.InitialStateJson.text)
                : new UniduxState();
        }
    }

    public static Store<UniduxState> Store {
        get { return Instance._store = Instance._store ?? new Store<UniduxState>(InitialState); }
    }

    public static object Dispatch<TAction>(TAction action)
    {
        return Store.Dispatch(action);
    }

    void Update()
    {
        Store.Update();
    }
}
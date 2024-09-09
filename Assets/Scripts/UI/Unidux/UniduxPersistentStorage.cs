using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UniduxPersistentStorage {

    public static UniduxState state { get; private set; }

    public static void StoreState()
    {
        state = UniduxMaze.State;
    }

    public static void DestroyState()
    {
        state = null;
    }

}

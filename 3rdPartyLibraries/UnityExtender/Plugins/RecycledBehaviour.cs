using UnityEngine;
using System.Collections;

/// <summary>
/// Any objects that are used in the ObjectPool must inherit from this class.
/// </summary>
public class RecycledBehaviour : MonoBehaviour
{

    /// <summary>
    /// Saves the initial state of a prefab.
    /// </summary>
    virtual public void SaveInitialState ()
    {
    }

    /// <summary>
    /// Restores the initial state of a prefab when it is is added back to the pool.
    /// </summary>
    virtual public void RestoreInitialState ()
    {
    }

}


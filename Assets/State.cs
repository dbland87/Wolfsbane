using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{

    public ItemState currentState;
    public MeshRenderer errorMesh;
    public enum ItemState
    {
        Default,
        Error
    }
    public void Set(ItemState newState)
    {
        currentState = newState;
        switch (newState)
        {
            case ItemState.Error:
            {
                errorMesh.enabled = true;
                break;
            }
            case ItemState.Default:
            {
                errorMesh.enabled = false;
                break;
            }
        }
    }

    public void Reset()
    {
        currentState = ItemState.Default;
    }
}

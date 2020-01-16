using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{

    public ItemState currentState;
    public GameObject errorMeshObject;
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
                currentState = ItemState.Error;
                errorMeshObject.SetActive(true);
                break;
            }
            case ItemState.Default:
            {
                currentState = ItemState.Default;
                errorMeshObject.SetActive(false);
                break;
            }
            default:
                currentState = ItemState.Default;
                errorMeshObject.SetActive(false);
                break;
        }
    }

    public void Reset()
    {
        Set(ItemState.Default);
    }
}

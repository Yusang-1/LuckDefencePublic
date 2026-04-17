using UnityEngine;
using System;

public abstract class UIPresenter<T> : MonoBehaviour
{
    public abstract void OnUpdateUI(T item);
}

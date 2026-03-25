using UnityEngine;
using System;

public abstract class UIPresenter : MonoBehaviour
{
    public abstract void OnUpdateUI<T>(T item) where T : IConvertible;
}

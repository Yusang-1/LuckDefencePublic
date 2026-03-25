using System.Collections;
using UnityEngine;

public abstract class AbstractUI : MonoBehaviour
{
    public abstract IEnumerator Initialize();

    public abstract void PortraitSelected(int code);

    public abstract void RemovePortrait(int code);
}

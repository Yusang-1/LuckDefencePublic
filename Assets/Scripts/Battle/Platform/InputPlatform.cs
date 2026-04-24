using UnityEngine;
using System.Collections;

public class InputPlatform : SelectableController
{
    [SerializeField] private Platforms platforms;
    private InputManager inputManager;
    private ISelectableObject selectedPlatform;

    private readonly float holdJudgeTime = 0.25f;
    private bool isHold;
    private bool isHoldSuccess;
    private IEnumerator waitForSelectEndCoroutine;

    private void Start()
    {
        isHold = false;

        inputManager = FindAnyObjectByType<InputManager>();
        
        inputManager.selectStartCallbacks.Add(this, InputSelectContextStarted);
        inputManager.selectCanceledCallbacks.Add(this, InputSelectContextCanceled);
    }

    private void OnDestroy()
    {
        inputManager.selectStartCallbacks.Remove(this);
        inputManager.selectCanceledCallbacks.Remove(this);
    }

    private ISelectableObject startedObject;
    private void InputSelectContextStarted(ISelectableObject newSelectable)
    {
        isHold = false;
        isHoldSuccess = false;
        startedObject = newSelectable;
     
        if(newSelectable == null) return;

        waitForSelectEndCoroutine = WaitForSelectEnd();
        StartCoroutine(waitForSelectEndCoroutine);
    }

    private void InputSelectContextCanceled(ISelectableObject newSelectable)
    {
        if (waitForSelectEndCoroutine != null)
        {
            StopCoroutine(waitForSelectEndCoroutine);
        }
        waitForSelectEndCoroutine = null;

        if (isHold && newSelectable is IHoldableObject holdableObject)
        {
            selectedPlatform?.SelectedEnd();
            holdableObject.HoldReleased(isHoldSuccess);
        }
        else if(isHold && newSelectable == null)
        {
            (startedObject as IHoldableObject)?.HoldReleased(isHoldSuccess);
        }
        else
        {
            selectedPlatform?.SelectedEnd();
            newSelectable?.Selected();
            selectedPlatform = newSelectable;
        }

        startedObject = null;
    }

    /// <summary>
    /// SelectEnd가 호출되기 전에 holdJudgeTime이 지나면 hold판정
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitForSelectEnd()
    {
        float elapsedTime = 0f;
        while (elapsedTime < holdJudgeTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (startedObject is IHoldableObject)
        {
            isHold = true;
            isHoldSuccess = (startedObject as IHoldableObject).Holded();
        }
    }
}

public class SelectableController : MonoBehaviour
{
    
}
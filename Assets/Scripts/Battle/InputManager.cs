using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
    private Vector3 mousePosition;
    private bool isPointerOverGameObject;

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            isPointerOverGameObject = true;
        }
        else
        {
            isPointerOverGameObject = false;
        }
    }

    public Dictionary<SelectableController, Action<ISelectableObject>> selectStartCallbacks = new Dictionary<SelectableController, Action<ISelectableObject>>();
    public Dictionary<SelectableController, Action<ISelectableObject>> selectCanceledCallbacks = new Dictionary<SelectableController, Action<ISelectableObject>>();
    public void OnSelect(InputAction.CallbackContext context)
    {
        if (isPointerOverGameObject) return;

        if (context.started)
        {
            mousePosition = context.ReadValue<Vector2>();
            Vector3 vec = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));
            RaycastHit2D hit2D = Physics2D.Raycast(vec, Vector3.forward, float.MaxValue);

            if (hit2D && hit2D.collider.gameObject.TryGetComponent<ISelectableObject>(out ISelectableObject selectable))
            {
                if (selectStartCallbacks.TryGetValue(selectable.SelectableController, out Action<ISelectableObject> callback))
                {
                    callback?.Invoke(selectable);
                }
            }
            else
            {
                foreach (var callback in selectStartCallbacks.Values)
                {
                    callback?.Invoke(null);
                }
            }
        }

        // canceled에서는 mousePosition을 받아올수 없으므로 performed에서 최근 position을 받음
        if (context.performed)
        {
            mousePosition = context.ReadValue<Vector2>();
        }

        if (context.canceled)
        {
            Vector3 vec = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));
            RaycastHit2D hit2D = Physics2D.Raycast(vec, Vector3.forward, float.MaxValue);

            if (hit2D && hit2D.collider.gameObject.TryGetComponent<ISelectableObject>(out ISelectableObject selectable))
            {
                if (selectCanceledCallbacks.TryGetValue(selectable.SelectableController, out Action<ISelectableObject> callback))
                {
                    callback?.Invoke(selectable);
                }
            }
            else
            {
                foreach (var callback in selectCanceledCallbacks.Values)
                {
                    callback?.Invoke(null);
                }
            }
        }
    }

    public event Action<Vector2> OnMousePositionChanged;
    public void OnGetMousePosition(InputAction.CallbackContext context)
    {
        if (isPointerOverGameObject) return;
        if (OnMousePositionChanged == null) return;

        if (context.performed)
        {
            Vector2 position = context.ReadValue<Vector2>();
            position = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 10));
            OnMousePositionChanged?.Invoke(position);
        }
    }
}

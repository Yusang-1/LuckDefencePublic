using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] BattleManager manager;

    private ISelectableObject m_ISelectable;
    private Vector3 mousePosition;
    private bool isHold;
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
                if(m_ISelectable != selectable)
                {
                    m_ISelectable?.SelectedEnd();
                }

                m_ISelectable = selectable;
            }
            else
            {
                // 첫 selectedEnd : 다른 selectableObject 선택 대기, 두번째 selectedEnd : selectableObject 선택 안되었음을 알림
                m_ISelectable?.SelectedEnd();
                m_ISelectable?.SelectedEnd();
                m_ISelectable = null;
            }
        }

        if(context.performed)
        {
            mousePosition = context.ReadValue<Vector2>();
        }

        if (isHold) return;

        if (context.canceled)
        {
            ISelectableObject selectable;
            Vector3 vec = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));
            RaycastHit2D hit2D = Physics2D.Raycast(vec, Vector3.forward, float.MaxValue);

            if (hit2D && hit2D.collider.gameObject.TryGetComponent<ISelectableObject>(out selectable))
            {
                selectable.Selected();
            }
        }
    }

    public void OnHold(InputAction.CallbackContext context)
    {
        if (isPointerOverGameObject) return;

        if (context.performed)
        {
            isHold = true;

            Vector3 vec = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));
            RaycastHit2D hit2D = Physics2D.Raycast(vec, Vector3.forward, float.MaxValue);

            if (hit2D && hit2D.collider.gameObject.TryGetComponent<IHoldableObject>(out IHoldableObject holdable))
            {
                if (holdable == m_ISelectable)
                {
                    holdable.Holded();
                }
            }
        }

        if (context.canceled && isHold)
        {
            Vector3 vec = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));
            RaycastHit2D hit2D = Physics2D.Raycast(vec, Vector3.forward, float.MaxValue);

            if (hit2D && hit2D.collider.gameObject.TryGetComponent<IHoldableObject>(out IHoldableObject holdable))
            {
                holdable.HoldReleased();
            }
            isHold = false;
        }
    }
}

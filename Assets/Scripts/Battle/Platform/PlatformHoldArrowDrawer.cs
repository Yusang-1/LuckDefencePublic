using UnityEngine;

public class PlatformHoldArrowDrawer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer arrowHead;
    [SerializeField] private SpriteRenderer arrowBody;
    [SerializeField] private float arrowHeadOffset;
    [SerializeField] private float defaultBodyWidth;

    private bool isDrawing;
    private Vector2 standardPosition;
    private InputManager inputManager;

    private void Update()
    {
        if (isDrawing == false) return;
    }

    public void StartDraw(Platform platform)
    {
        if (inputManager == null)
        {
            inputManager = FindAnyObjectByType<InputManager>();
        }

        if (platform.EntityCount > 0)
        {
            isDrawing = true;
            arrowHead.gameObject.SetActive(true);
            arrowBody.gameObject.SetActive(true);
            arrowBody.size = new Vector2(defaultBodyWidth, 0);

            standardPosition = platform.transform.position;

            inputManager.OnMousePositionChanged += SetHeadPosition;
        }
    }

    public void EndDraw()
    {
        if (isDrawing)
        {
            isDrawing = false;
            arrowHead.gameObject.SetActive(false);
            arrowBody.gameObject.SetActive(false);

            inputManager.OnMousePositionChanged -= SetHeadPosition;
        }
    }

    private void SetHeadPosition(Vector2 mousePosition)
    {
        Vector2 direction = mousePosition - standardPosition;
        Vector2 position = new Vector2(mousePosition.x - direction.normalized.x * arrowHeadOffset, mousePosition.y - direction.normalized.y * arrowHeadOffset);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //float distance = direction.magnitude;
        arrowHead.transform.position = position;
        arrowHead.transform.rotation = Quaternion.Euler(0, 0, angle);

        DrawArrowBody();
    }

    private void DrawArrowBody()
    {

    }
}

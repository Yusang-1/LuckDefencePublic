using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectPlatformUIContainer : MonoBehaviour
{
    [SerializeField] private SelectPlatformUI m_UIRight;
    [SerializeField] private SelectPlatformUI m_UILeft;
    
    [SerializeField] private CanvasScaler canvasScaler;
    
    private Platforms platforms;

    public IEnumerator Initialize()
    {
        if(platforms == null)
        {
            platforms = FindFirstObjectByType<Platforms>();
        }

        platforms.PlatformSelected += OpenUI;
        platforms.NoPlatformSelected += CloseUI;
        platforms.PlatformDataChanged += UpdateUI;
        
        // float ratioX = canvasScaler.referenceResolution.x / Screen.width;
        // float ratioY = canvasScaler.referenceResolution.y / Screen.height;
        
        // Vector3 posRight = Camera.main.ViewportToScreenPoint(new Vector3(1,0,0));
        // posRight.x *= ratioX;
        // posRight.y *= ratioY;
        // Vector3 posLeft = Camera.main.ViewportToScreenPoint(new Vector3(0,0,0));
        // posLeft.x *= ratioX;
        // posLeft.y *= ratioY;
        
        m_UIRight.Initialize();
        yield return m_UILeft.Initialize();
    }

    private void OnDestroy()
    {
        platforms.PlatformSelected -= OpenUI;
        platforms.NoPlatformSelected -= CloseUI;
        platforms.PlatformDataChanged -= UpdateUI;
    }

    public void OpenUI(Platform platform)
    {
        if (Camera.main.WorldToScreenPoint(platform.transform.position).x >= Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0)).x)
        {
            OpenLeftUI(platform);
        }
        else
        {
            OpenRIghtUI(platform);
        }
    }

    public void OpenRIghtUI(Platform platform)
    {
        if(m_UILeft.IsOpen)
        {
            m_UILeft.OnCloseUI();
        }

        m_UIRight.OpenUI(platform);
    }

    public void OpenLeftUI(Platform platform)
    {
        if (m_UIRight.IsOpen)
        {
            m_UIRight.OnCloseUI();
        }

        m_UILeft.OpenUI(platform);
    }

    private void CloseUI()
    {
        if (m_UILeft.IsOpen)
        {
            m_UILeft.OnCloseUI();
        }

        if (m_UIRight.IsOpen)
        {
            m_UIRight.OnCloseUI();
        }
    }

    public void UpdateUI(Platform platform)
    {
        if(m_UIRight.IsOpen)
        {
            m_UIRight.SetData(platform);
        }

        if(m_UILeft.IsOpen)
        {
            m_UILeft.SetData(platform);
        }
    }
}

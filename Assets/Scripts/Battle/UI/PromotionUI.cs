using UnityEngine;
using UnityEngine.UI;

public class PromotionUI : MonoBehaviour
{
    //[SerializeField] private Button button;    
    [SerializeField] private Promotion promotion;

    private void Start()
    {
        //button.interactable = false;
        //promotion.PromotionableChanged += OnButtonAvailable;
    }

    private void OnDestroy()
    {
        //promotion.PromotionableChanged -= OnButtonAvailable;
    }

    public void OnPromotion()
    {
        promotion.OnPromotion();
    }

    public void OnButtonAvailable(bool value)
    {
        //button.interactable = value;
    }
}

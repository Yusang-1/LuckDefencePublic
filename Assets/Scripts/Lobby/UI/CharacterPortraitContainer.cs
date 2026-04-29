using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CharacterPortraitContainer : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image characterPortrait;
    [SerializeField] private Image protraitBackground;
    [SerializeField] private Image frameImage;
    [SerializeField] private TextMeshProUGUI characterName;
    
    private AbstractUI characterShopUI;
    private int characterCode;

    private bool isInSelectedArea;
    private bool isActivated;

    public int CharacterCode => characterCode;

    public void Initialize(Entity entity, AbstractUI characterShopUI, bool isInSelectedArea)
    {
        if(entity != null)
        {
            characterName.text = entity.Data.EntityName;
            characterCode = entity.Data.Code;
            isActivated = true;
        }
        this.characterShopUI = characterShopUI;
        this.isInSelectedArea = isInSelectedArea;
        
        SetPortrait(entity, null);
    }

    public void SetPortrait(Entity entity, string colorCode)
    {
        if(entity == null)
        {
            characterName.text = "";
            characterCode = 0;
            protraitBackground.color = Color.black;
            characterPortrait.gameObject.SetActive(false);
            isActivated = false;
            return;
        }

        characterName.text = entity.Data.EntityName;
        characterCode = entity.Data.Code;

        ColorUtility.TryParseHtmlString(colorCode, out Color setColor);
        protraitBackground.color = setColor;
        characterPortrait.sprite = (entity.Data as CharacterSO).Portrait;
        characterPortrait.gameObject.SetActive(true);
        isActivated = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(isInSelectedArea == true && isActivated)
        {
            characterShopUI.RemovePortrait(characterCode);
        }
        else if(isInSelectedArea == true && isActivated == false)
        {
            return;
        }
        else
        {
            characterShopUI.PortraitSelected(characterCode);
        }
    }
}

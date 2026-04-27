using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CharacterPortraitContainer : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image characterPortrait;
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
            characterPortrait.color = Color.white;
            isActivated = false;
            return;
        }

        characterName.text = entity.Data.EntityName;
        characterCode = entity.Data.Code;

        // Color setColor;
        // ColorUtility.TryParseHtmlString(colorCode, out setColor);
        characterPortrait.sprite = (entity.Data as CharacterSO).Portrait;
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

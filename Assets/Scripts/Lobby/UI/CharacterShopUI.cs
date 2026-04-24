using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class CharacterShopUI : AbstractUI, ILobbyUIState, IUIAnimation
{
    [SerializeField] private CharacterData characterData;
    [SerializeField] private PlayerResourcesSO playerResourcesData;

    [SerializeField] private AllCharListUI[] allCharListUIs;
    [SerializeField] private ManagedCharacterUI managedCharListUIs;
    [SerializeField] private CharacterInfoUI selectedCharacterInfoUI;

    [SerializeField] private RectTransform contentRect;
    [SerializeField] private TextMeshProUGUI charPrice;

    [SerializeField] private UIAnimation[] uiAnimations;
    [SerializeField] private float uiOpenTime;
    private IEnumerator deactiveUICoroutine;

    private int selectedCharCode;
    private Entity selectedEntity;

    public int SelectedCharCode
    {
        get => selectedCharCode;
        set
        {
            selectedCharCode = value;
            selectedEntity = characterData.CharacterListData.CharListAsRankDictionary[characterData.GetCharRankByCode(selectedCharCode)].EntityAsCodeDict[selectedCharCode];
            selectedCharacterInfoUI.SetInfoUI(selectedEntity);
            charPrice.text = (selectedEntity.Data as CharacterSO).Price.ToString();
        }
    }
    
    public IEnumerator Initialize(CharacterData characterData)
    {
        gameObject.SetActive(true);
        
        this.characterData = characterData;

        float height = 0;
        for (int i = 0; i < allCharListUIs.Length; i++)
        {
            allCharListUIs[i].Initialize(characterData.CharacterListData.CharListAsRankDictionary[(CharRank)i], this);

            height += allCharListUIs[i].GetUIHeight();
            yield return null;
        }
        height += allCharListUIs[allCharListUIs.Length - 1].GetUIHeight() / 2;

        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, height);
        
        gameObject.SetActive(false);
        foreach(var uiAnimation in uiAnimations)
        {
            yield return uiAnimation.Initizlize();
        }        
    }

    public override void PortraitSelected(int code)
    {
        SelectedCharCode = code;
    }
    
    public override void RemovePortrait(int code)
    {
        return;
    }

    public void OnBuyCharacter()
    {
        if (characterData.OwnedCharacterListData.CharListAsRankDictionary[characterData.GetCharRankByCode(selectedCharCode)].IsCodeExist(selectedCharCode))
        {
            Debug.LogWarning("이미 소유한 캐릭터를 구매하려고 함");
            return;
        }

        if(playerResourcesData.PlayerCoin < (selectedEntity.Data as CharacterSO).Price)
        {
            Debug.LogWarning("캐릭터를 구매할 금액이 부족함");
            return;
        }
        playerResourcesData.AddPlayerCoin(-(selectedEntity.Data as CharacterSO).Price);

        characterData.AddOwnedCharacter(selectedEntity);

        UpdateShopUI();
    }

    private void UpdateShopUI()
    {
        managedCharListUIs.UpdateShopUI();
    }

    public void ActiveUI()
    {
        gameObject.SetActive(true);
        if (deactiveUICoroutine != null)
        {
            StopCoroutine(deactiveUICoroutine);
        }

        ActiveUIAnimation();
    }

    public void ActiveUIAnimation()
    {
        foreach (var uiAnimation in uiAnimations)
        {
            uiAnimation.PlayEnableAnimation(uiOpenTime);
        }
    }

    public void DeactiveUI()
    {
        deactiveUICoroutine = DeactiveUIAnimationCoroutine();
        StartCoroutine(deactiveUICoroutine);
    }

    public IEnumerator DeactiveUIAnimationCoroutine()
    {
        bool isAnimationsComplete;

        foreach (var uiAnimation in uiAnimations)
        {
            uiAnimation.PlayDisableAnimation(uiOpenTime);
        }

        while(true)
        {
            isAnimationsComplete = true;

            foreach (var uiAnimation in uiAnimations)
            {
                isAnimationsComplete = isAnimationsComplete && uiAnimation.IsDisableAnimationFinished;

                if(isAnimationsComplete == false)
                {
                    break;
                }
            }

            if(isAnimationsComplete == true)
            {
                break;
            }

            yield return null;
        }

        gameObject.SetActive(false);
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagedCharacterUI : AbstractUI, ILobbyUIState, IUIAnimation
{
    [SerializeField] private CharacterData characterData;

    [Header("UIs")]
    [SerializeField] private OwnedCharListUI[] ownedCharListUIs;
    [SerializeField] private SelectedCharactersUI selectedCharactersUI;
    [SerializeField] private CharacterInfoUI selectedCharacterInfoUI;

    [SerializeField] private RectTransform contentRect;

    [Header("Animations")]
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
        }
    }

    public override IEnumerator Initialize()
    {
        gameObject.SetActive(true);

        yield return StartCoroutine(selectedCharactersUI.Initialize(characterData, this));

        float height = 0;
        for (int i = 0; i < ownedCharListUIs.Length; i++)
        {
            ownedCharListUIs[i].Initialize(characterData, characterData.OwnedCharacterListData.CharListAsRankDictionary[(CharRank)i], this);
            height += ownedCharListUIs[i].GetUIHeight();
            yield return null;
        }
        height += ownedCharListUIs[ownedCharListUIs.Length - 1].GetUIHeight() / 2;

        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, height);
        
        gameObject.SetActive(false);
        foreach(var uiAnimation in uiAnimations)
        {
            yield return uiAnimation.Initizlize();
        }        
    }

    public void OpenUI()
    {
        gameObject.SetActive(true);

        for (int i = 0; i < ownedCharListUIs.Length; i++)
        {
            ownedCharListUIs[i].OpenAllCharacterListUI();
        }
        UpdateShopUI();
    }

    public override void PortraitSelected(int code)
    {
        SelectedCharCode = code;
    }

    public override void RemovePortrait(int code)
    {
        characterData.RemoveSelectedCharacter(characterData.SelectedCharacterListData.CharListAsRankDictionary[characterData.GetCharRankByCode(code)].EntityAsCodeDict[code]);

        UpdateShopUI();
    }

    public void OnAddCharacterToBattleList()
    {
        characterData.AddSelectedCharacter(selectedEntity);

        UpdateShopUI();
    }

    public void UpdateShopUI()
    {
        for (int i = 0; i < ownedCharListUIs.Length; i++)
        {
            ownedCharListUIs[i].UpdateUI();
        }

        selectedCharactersUI.UpdateUI();
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

    public void DeactiveUI()
    {
        deactiveUICoroutine = DeactiveUIAnimationCoroutine();
        StartCoroutine(deactiveUICoroutine);
    }

    public void ActiveUIAnimation()
    {
        foreach (var uiAnimation in uiAnimations)
        {
            uiAnimation.PlayEnableAnimation(uiOpenTime);
        }
    }

    public IEnumerator DeactiveUIAnimationCoroutine()
    {
        bool isAnimationsComplete;

        foreach (var uiAnimation in uiAnimations)
        {
            uiAnimation.PlayDisableAnimation(uiOpenTime);
        }

        while (true)
        {
            isAnimationsComplete = true;

            foreach (var uiAnimation in uiAnimations)
            {
                isAnimationsComplete = isAnimationsComplete && uiAnimation.IsDisableAnimationFinished;

                if (isAnimationsComplete == false)
                {
                    break;
                }
            }

            if (isAnimationsComplete == true)
            {
                break;
            }

            yield return null;
        }

        gameObject.SetActive(false);
    }
}

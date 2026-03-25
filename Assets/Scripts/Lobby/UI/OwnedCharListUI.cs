using TMPro;
using UnityEngine;

public class OwnedCharListUI : MonoBehaviour
{
    [SerializeField] private RectTransform myRect;
    [SerializeField] private RectTransform upperUI;
    [SerializeField] private RectTransform lowerUI;
    [SerializeField] private RectTransform characterPortraitUI;
    [SerializeField] private TextMeshProUGUI rankText;

    [SerializeField] private float defaultSpacingBetweenUIs;
    [SerializeField] private float paddingHorizontal;
    [SerializeField] private float paddingVertical;
    [SerializeField] private Vector2 anchorTopLeft;
    
    private CharListAsRank charList;
    private CharacterPortraitContainer[] portraitContainers;

    private int activatedPortraitCount;

    public void Initialize(CharacterData characterData, CharListAsRank charList, AbstractUI managedCharacterUI)
    {
        activatedPortraitCount = 0;

        this.charList = charList;
        portraitContainers = new CharacterPortraitContainer[characterData.CharacterListData.CharListAsRankDictionary[charList.Rank].EntityList.Length];

        GameObject uiObject;
        for (int i = 0; i < characterData.CharacterListData.CharListAsRankDictionary[charList.Rank].EntityList.Length; i++)
        {
            uiObject = Instantiate(characterPortraitUI.gameObject, lowerUI);

            portraitContainers[i] = uiObject.GetComponent<CharacterPortraitContainer>();
            portraitContainers[i].Initialize(characterData.CharacterListData.CharListAsRankDictionary[charList.Rank].EntityList[i], managedCharacterUI, false);

            portraitContainers[i].gameObject.SetActive(false);
        }
        rankText.text = charList.Rank.ToString();

        OpenAllCharacterListUI();
    }

    public void OpenAllCharacterListUI()
    {
        gameObject.SetActive(true);

        myRect.sizeDelta = new Vector2(myRect.sizeDelta.x, 0);
        lowerUI.sizeDelta = new Vector2(lowerUI.sizeDelta.x, 0);

        int maxColumn = 1;
        float spacingBetweenUIs;
        while (true)
        {
            if(paddingHorizontal * 2 + characterPortraitUI.sizeDelta.x * maxColumn + defaultSpacingBetweenUIs * (maxColumn - 1) > myRect.sizeDelta.x)
            {
                spacingBetweenUIs = defaultSpacingBetweenUIs;
                maxColumn--;
                break;
            }
            else if(paddingHorizontal * 2 + characterPortraitUI.sizeDelta.x * maxColumn + defaultSpacingBetweenUIs * (maxColumn - 1) == myRect.sizeDelta.x)
            {
                spacingBetweenUIs = (myRect.sizeDelta.x - paddingHorizontal * 2 + characterPortraitUI.sizeDelta.x * maxColumn) / (maxColumn - 1);
                break;
            }

            maxColumn++;
        }

        activatedPortraitCount = 1;
        int row = 0, column = 0;
        Vector2 pos;
        RectTransform rect;

        int count = 0;
        for (int i = 0; i < portraitContainers.Length; i++)
        {
            if (count < charList.EntityList.Length && portraitContainers[i].CharacterCode == charList.CodeList[count])
            {
                count++;
            }
            else
            {
                continue;
            }

            portraitContainers[i].gameObject.SetActive(true);

            rect = portraitContainers[i].GetComponent<RectTransform>();
            rect.anchorMax = anchorTopLeft;
            rect.anchorMin = anchorTopLeft;

            pos.x = paddingHorizontal + column * (rect.sizeDelta.x + spacingBetweenUIs) + characterPortraitUI.sizeDelta.x / 2 - myRect.sizeDelta.x /2;
            pos.y = -paddingVertical - row * (rect.sizeDelta.y + spacingBetweenUIs) - characterPortraitUI.sizeDelta.y / 2;
            rect.localPosition = pos;

            if(activatedPortraitCount % maxColumn == 0)
            {
                column = 0;
                row++;
            }
            else
            {
                column++;
            }

            activatedPortraitCount++;
        }

        if (column == 0 && row != 0)
        {
            row--;
        }

        myRect.sizeDelta = new Vector2(myRect.sizeDelta.x, paddingVertical * 2 + (row + 1) * characterPortraitUI.sizeDelta.y + row * spacingBetweenUIs + upperUI.sizeDelta.y);
        lowerUI.sizeDelta = new Vector2(lowerUI.sizeDelta.x, paddingVertical * 2 + (row + 1) * characterPortraitUI.sizeDelta.y + row * spacingBetweenUIs);
    }

    public void UpdateUI()
    {
        if(charList.IsDirty == false)
        {
            return;
        }

        OpenAllCharacterListUI();
    }

    public float GetUIHeight()
    {
        return myRect.sizeDelta.y;
    }

    //public void OpenOwnedCharacterListUI()
    //{
    //    int count = 0;
    //    foreach(var item in charList.IsEntityOwnedDict)
    //    {
    //        if(item.Value == true)
    //        {
    //            portraitUIs[count].SetActive(true);
    //        }
    //        else
    //        {
    //            portraitUIs[count].SetActive(false);
    //        }

    //        count++;
    //    }
    //}
}

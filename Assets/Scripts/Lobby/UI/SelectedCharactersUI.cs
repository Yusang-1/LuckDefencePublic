using UnityEngine;
using System.Collections;

public class SelectedCharactersUI : MonoBehaviour
{
    [SerializeField] private GameObject portrait;
    [SerializeField] private RectTransform myRect;    

    [SerializeField] private float padding;
    [SerializeField] private float spacing;
    [SerializeField] private int columnCount;

    private CharacterData characterData;
    private CharacterPortraitContainer[] portraits;

    private int selectedCount;

    public IEnumerator Initialize(CharacterData characterData, ManagedCharacterUI managedCharacterUI)
    {
        this.characterData = characterData;
        portraits = new CharacterPortraitContainer[characterData.AllCount];

        // columnCount개의 portrait을 배치하려면 필요한 Portrait의 넓이
        float portraitWidth = (myRect.rect.width - ((padding * 2) + (spacing * (columnCount - 1)))) / columnCount;

        int column = 0, row = 0;
        Vector2 pos;

        GameObject go;
        RectTransform portraitRect;
        for (int i = 0; i < characterData.AllCount; i++)
        {
            go = Instantiate(portrait, myRect);
            portraits[i] = go.GetComponent<CharacterPortraitContainer>();
            portraits[i].Initialize(null, managedCharacterUI, true);

            // Portrait의 크기 설정
            portraitRect = portraits[i].GetComponent<RectTransform>();            
            portraitRect.sizeDelta = new Vector3(portraitWidth, portraitWidth, 1);

            // Portrait의 위치 지정
            pos.x = padding + column * (portraitRect.sizeDelta.x + spacing) + portraitRect.sizeDelta.x / 2 - myRect.rect.width / 2;
            pos.y = -padding - row * (portraitRect.sizeDelta.y + spacing) - portraitRect.sizeDelta.y / 2 + myRect.rect.height / 2;            
            portraitRect.localPosition = pos;

            // 열의 수가 충족되면 다음 행으로
            if (column != 0 && column % (columnCount-1) == 0)
            {
                column = 0;
                row++;
            }
            else
            {
                column++;
            }
            
            yield return null;
        }

        characterData.SelectedCharacterListData.IsDirty = true;
        foreach (var charListAsRank in characterData.SelectedCharacterListData.CharListAsRankDictionary)
        {
            charListAsRank.Value.SetDirty(true);
        }
        UpdateUI();
    }

    public void UpdateUI()
    {
        if(characterData.SelectedCharacterListData.IsDirty == false)
        {
            return;
        }

        int count = 0;
        int rankCount = 0;
        foreach (var charListAsRank in characterData.SelectedCharacterListData.CharListAsRankDictionary)
        {
            // 해당 랭크의 charList는 변경할게 없다면 해당 랭크 개수만큼의 portrait을 건너뛰기
            if(charListAsRank.Value.IsDirty == false)
            {
                rankCount += characterData.RankCountByRank[charListAsRank.Key];
                count = rankCount;
                continue;
            }

            foreach(var entity in charListAsRank.Value.EntityList)
            {
                if(entity == null)
                {
                    portraits[count].SetPortrait(entity, null);
                    count++;
                    continue;
                }

                portraits[count].SetPortrait(entity, characterData.ColorCodeByRank[(entity.Data as CharacterSO).Rank]);
                count++;
            }
            charListAsRank.Value.SetDirty(false);

            rankCount += characterData.RankCountByRank[charListAsRank.Key];
            count = rankCount;
        }
        characterData.SelectedCharacterListData.IsDirty = false;
    }
}

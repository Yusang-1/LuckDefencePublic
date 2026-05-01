using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    [SerializeField] private PlayerResourcesSO playerResources;
    [SerializeField] private CharacterData characterData;
    [SerializeField] private StagesSO stagesData;
    [SerializeField] private RankUnlockSO rankUnlockData;

    public void SaveGame()
    {
        Debug.Log("게임 저장");
        // playerResources 저장
        string json = JsonUtility.ToJson(playerResources.GetSaveData());
        PlayerPrefs.SetString("PlayerResources", json);

        // characterData 저장
        foreach (var charListAsRank in characterData.OwnedCharacterListData.CharListAsRankDictionary.Values)
        {
            json = JsonUtility.ToJson((charListAsRank as OwnedCharListAsRank).GetSaveData());
            PlayerPrefs.SetString($"OwnedCharacterList_{charListAsRank.Rank}", json);
        }

        foreach (var charListAsRank in characterData.SelectedCharacterListData.CharListAsRankDictionary.Values)
        {
            json = JsonUtility.ToJson((charListAsRank as SelectedCharListAsRank).GetSaveData());
            PlayerPrefs.SetString($"SelectedCharacterList_{charListAsRank.Rank}", json);
        }

        // stagesData 저장
        json = JsonUtility.ToJson(stagesData.GetSaveData());
        PlayerPrefs.SetString("StagesData", json);

        // rankUnlockData 저장
        json = JsonUtility.ToJson(rankUnlockData.GetSaveData());
        PlayerPrefs.SetString("RankUnlockData", json);
    }

    public void LoadGame()
    {
        Debug.Log("게임 불러오기");
        // playerResources 불러오기
        if (PlayerPrefs.HasKey("PlayerResources"))
        {
            string json = PlayerPrefs.GetString("PlayerResources");
            PlayerResourcesSO.PlayerResourcesSaveData saveData = JsonUtility.FromJson<PlayerResourcesSO.PlayerResourcesSaveData>(json);
            playerResources.SetLoadData(saveData);
        }
        else
        {
            playerResources.SetDefaultData();
            Debug.Log("저장된 플레이어 자원이 없습니다. 기본값으로 설정합니다.");
        }

        // characterData 불러오기
        characterData.Initialize();

        foreach (var charListAsRank in characterData.OwnedCharacterListData.CharListAsRankDictionary.Values)
        {
            OwnedCharListAsRank charList = (OwnedCharListAsRank)charListAsRank;
            
            if (PlayerPrefs.HasKey($"OwnedCharacterList_{charListAsRank.Rank}"))
            {
                string json = PlayerPrefs.GetString($"OwnedCharacterList_{charListAsRank.Rank}");

                OwnedCharListAsRank.CharListAsRankSaveData saveData = JsonUtility.FromJson<OwnedCharListAsRank.CharListAsRankSaveData>(json);
                charList.SetLoadData(saveData);
            }
            else
            {
                charList.SetDefaultData();
                Debug.Log($"저장된 {charListAsRank.Rank} 랭크의 소유 캐릭터 목록이 없습니다. 기본값으로 설정합니다.");
            }
        }

        foreach (var charListAsRank in characterData.SelectedCharacterListData.CharListAsRankDictionary.Values)
        {
            SelectedCharListAsRank charList = (SelectedCharListAsRank)charListAsRank;
            
            if (PlayerPrefs.HasKey($"SelectedCharacterList_{charListAsRank.Rank}"))
            {
                string json = PlayerPrefs.GetString($"SelectedCharacterList_{charListAsRank.Rank}");

                SelectedCharListAsRank.CharListAsRankSaveData saveData = JsonUtility.FromJson<SelectedCharListAsRank.CharListAsRankSaveData>(json);
                charList.SetLoadData(saveData);
            }
            else
            {
                charList.SetDefaultData();
                Debug.Log($"저장된 {charListAsRank.Rank} 랭크의 선택된 캐릭터 목록이 없습니다. 기본값으로 설정합니다.");
            }
        }

        // stagesData 불러오기
        if (PlayerPrefs.HasKey("StagesData"))
        {
            string json = PlayerPrefs.GetString("StagesData");
            StagesSO.StageSaveData saveData = JsonUtility.FromJson<StagesSO.StageSaveData>(json);
            stagesData.SetLoadData(saveData);
        }
        else
        {
            stagesData.SetDefaultData();
            Debug.Log("저장된 stagesData가 없습니다. 기본값으로 설정합니다.");
        }

        // RankUnlockData 불러오기
        if (PlayerPrefs.HasKey("RankUnlockData"))
        {
            string json = PlayerPrefs.GetString("RankUnlockData");
            RankUnlockSO.RankUnlockSaveData saveData = JsonUtility.FromJson<RankUnlockSO.RankUnlockSaveData>(json);
            rankUnlockData.SetLoadData(saveData);
        }
        else
        {
            rankUnlockData.SetDefaultData();
            Debug.Log("저장된 rankUnlockData가 없습니다. 기본값으로 설정합니다.");
        }
    }
}

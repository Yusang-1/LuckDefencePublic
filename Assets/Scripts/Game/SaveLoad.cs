using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    [SerializeField] private PlayerResourcesSO playerResources;
    [SerializeField] private CharacterData characterData;
    
    public void SaveGame()
    {
        Debug.Log("게임 저장");
        string json = JsonUtility.ToJson(playerResources.GetSaveData());
        PlayerPrefs.SetString("PlayerResources", json);
        
        foreach(var charListAsRank in characterData.OwnedCharacterListData.CharListAsRankDictionary.Values)
        {
            json = JsonUtility.ToJson(charListAsRank.GetSaveData());
            PlayerPrefs.SetString($"OwnedCharacterList_{charListAsRank.Rank}", json);
        }
        
        foreach(var charListAsRank in characterData.SelectedCharacterListData.CharListAsRankDictionary.Values)
        {
             json = JsonUtility.ToJson(charListAsRank.GetSaveData());
            PlayerPrefs.SetString($"SelectedCharacterList_{charListAsRank.Rank}", json);
        }       
    }
    
    public void LoadGame()
    {
        Debug.Log("게임 불러오기");
        GameManager gameManager = FindAnyObjectByType<GameManager>();
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
        
        characterData.Initialize();        
        
        foreach(var charListAsRank in characterData.OwnedCharacterListData.CharListAsRankDictionary.Values)
        {
            if (PlayerPrefs.HasKey($"OwnedCharacterList_{charListAsRank.Rank}"))
            {
                string json = PlayerPrefs.GetString($"OwnedCharacterList_{charListAsRank.Rank}");
                
                CharListAsRank.CharListAsRankSaveData saveData = JsonUtility.FromJson<CharListAsRank.CharListAsRankSaveData>(json);
                charListAsRank.SetLoadData(saveData);
            }
            else
            {
                charListAsRank.SetDefaultData();
                Debug.Log($"저장된 {charListAsRank.Rank} 랭크의 소유 캐릭터 목록이 없습니다. 기본값으로 설정합니다.");
            }
        }
        
        foreach(var charListAsRank in characterData.SelectedCharacterListData.CharListAsRankDictionary.Values)
        {
            if (PlayerPrefs.HasKey($"SelectedCharacterList_{charListAsRank.Rank}"))
            {
                string json = PlayerPrefs.GetString($"SelectedCharacterList_{charListAsRank.Rank}");
                
                CharListAsRank.CharListAsRankSaveData saveData = JsonUtility.FromJson<CharListAsRank.CharListAsRankSaveData>(json);
                charListAsRank.SetLoadData(saveData);
            }
            else
            {
                charListAsRank.SetDefaultData();
                Debug.Log($"저장된 {charListAsRank.Rank} 랭크의 선택된 캐릭터 목록이 없습니다. 기본값으로 설정합니다.");
            }
        }         
    }
}

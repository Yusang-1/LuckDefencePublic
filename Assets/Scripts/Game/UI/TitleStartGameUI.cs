using UnityEngine;

public class TitleStartGameUI : MonoBehaviour
{
    public void OnStartGame()
    {
        SceneChanger.LoadSceneAsync("LobbyScene");
    }
}

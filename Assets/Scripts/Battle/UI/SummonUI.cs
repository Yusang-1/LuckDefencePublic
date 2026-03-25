using UnityEngine;
using UnityEngine.UI;

public class SummonUI : MonoBehaviour
{
    [SerializeField] private Button button;
    private CharacterSpawner spawner;

    private void Start()
    {
        spawner = FindFirstObjectByType<CharacterSpawner>();
    }

    public void Initialize()
    {
        spawner = FindFirstObjectByType<CharacterSpawner>();
    }

    public void OnSummon()
    {
        spawner.SpawnEntity();
    }

    public void EnableButton()
    {
        button.enabled = true;
    }

    public void DisableButton()
    {
        button.enabled = false;
    }
}

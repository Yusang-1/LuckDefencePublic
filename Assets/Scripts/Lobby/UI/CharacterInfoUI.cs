using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterInfoUI : MonoBehaviour
{
    [SerializeField] private Image charPortrait;
    [SerializeField] private TextMeshProUGUI charName;
    [SerializeField] private TextMeshProUGUI CharAtkPoint;
    [SerializeField] private TextMeshProUGUI CharAtkRange;
    [SerializeField] private TextMeshProUGUI CharMaxMana;
    [SerializeField] private TextMeshProUGUI CharAtkSpeed;
    [SerializeField] private TextMeshProUGUI CharMoveSpeed;

    public void SetInfoUI(Entity entity)
    {
        charPortrait.sprite = (entity.Data as CharacterSO).FullImage;
        
        charName.text = entity.Data.EntityName;
        
        CachedTextNumber cachedText = new CachedTextNumber();
        int length;
        CharAtkPoint.SetCharArray(cachedText.GetCachedText(entity.Data.AttackPoint, out length), 0, length);
        CharAtkRange.SetCharArray(cachedText.GetCachedText(entity.Data.AttackRange, out length), 0, length);
        CharMaxMana.SetCharArray(cachedText.GetCachedText(entity.Data.MaxMp, out length), 0, length);
        CharAtkSpeed.SetCharArray(cachedText.GetCachedText(entity.Data.AttackSpeed, out length), 0, length);
        CharMoveSpeed.SetCharArray(cachedText.GetCachedText(entity.Data.MoveSpeed, out length), 0, length);
    }
}

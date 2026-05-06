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

        CharAtkPoint.SetCharArray(CachedTextNumber.GetCachedText(entity.Data.AttackPoint, out int length), 0, length);
        CharAtkRange.SetCharArray(CachedTextNumber.GetCachedText(entity.Data.AttackRange, out length), 0, length);
        CharMaxMana.SetCharArray(CachedTextNumber.GetCachedText(entity.Data.MaxMp, out length), 0, length);
        CharAtkSpeed.SetCharArray(CachedTextNumber.GetCachedText(entity.Data.AttackSpeed, out length), 0, length);
        CharMoveSpeed.SetCharArray(CachedTextNumber.GetCachedText(entity.Data.MoveSpeed, out length), 0, length);
    }
}

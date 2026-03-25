using UnityEngine;
using TMPro;

public class CharacterInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI charName;
    [SerializeField] private TextMeshProUGUI CharAtkPoint;
    [SerializeField] private TextMeshProUGUI CharAtkRange;
    [SerializeField] private TextMeshProUGUI CharMaxMana;
    [SerializeField] private TextMeshProUGUI CharAtkSpeed;
    [SerializeField] private TextMeshProUGUI CharMoveSpeed;

    public void SetInfoUI(Entity entity)
    {
        charName.text = entity.Data.EntityName;

        CharAtkPoint.text = entity.Data.AttackPoint.ToString();
        CharAtkRange.text = entity.Data.AttackRange.ToString();
        CharMaxMana.text = entity.Data.MaxMp.ToString();
        CharAtkSpeed.text = entity.Data.AttackSpeed.ToString();
        CharMoveSpeed.text = entity.Data.MoveSpeed.ToString();
    }
}

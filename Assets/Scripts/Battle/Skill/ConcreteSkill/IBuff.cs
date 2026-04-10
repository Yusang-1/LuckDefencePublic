using UnityEngine;

public interface IBuff : ISkill
{
    public void ApplyBuff(Entity target);
    public void RemoveBuff(Entity target);
}

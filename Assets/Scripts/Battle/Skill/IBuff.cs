public interface IBuff : ISkill
{
    public float ApplyBuff(Entity target);

    public void ReleaseBuff(Entity target);
}

using UnityEngine;
using System;

public interface ISkillUsableData
{
    public event Action<int> MPChanged;
}

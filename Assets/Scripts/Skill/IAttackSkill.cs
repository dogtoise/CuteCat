using UnityEngine;
public interface IAttackSkill
{
    void Initialize(Actor owner);

    int GetLevel();
    bool IsMaxLevel();
    void LevelUp();
}

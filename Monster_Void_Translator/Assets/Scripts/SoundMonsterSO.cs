using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterSound", menuName = "MonsterSound")]
public class SoundMonsterSO : ScriptableObject
{
    public List<AudioClip> monsterSounds = new List<AudioClip>();
}

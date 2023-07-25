using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterInfor", menuName = "MonsterInfor")]
public class MonsterInforSO : ScriptableObject
{
    public string monsterName;
    public string nickName;
    public string gender;
    public string birthDay;
    public string Origin;

    [TextArea]
    public string featuresAndStory;
    public int contentHeight;
}

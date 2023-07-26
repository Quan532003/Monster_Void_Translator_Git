using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterInfor", menuName = "MonsterInfor")]
public class MonsterInforSO : ScriptableObject
{
    public string monsterName;
    public string gender;
    public string birthDay;
    public string Origin;

    [TextArea]
    public string features;
    [TextArea]
    public string story;
    public int contentHeight;
    public int featureHeight;
    public int storyHeight;
    public int furturePosY;
    public int storyPosY;

    public Color colorTitle;
}

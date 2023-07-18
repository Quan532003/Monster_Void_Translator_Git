using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterInfor
{
    public string name;
    public string nickName;
    public string gender;
    public string birthDay;
    public string Origin;
    public string featuresAndStory;
}
public class FileController
{
    public static MonsterInfor ReadFile(int index)
    {
        MonsterInfor monsterInfor = new MonsterInfor();
        string path = "Monster" + index;
        TextAsset text = Resources.Load<TextAsset>(path);
        string[] contents = text.text.Split('\n');
        monsterInfor.name = contents[0];
        monsterInfor.nickName = contents[1];
        monsterInfor.gender = contents[2];
        monsterInfor.birthDay = contents[3];
        monsterInfor.Origin = contents[4];
        monsterInfor.featuresAndStory = contents[5] + "\n" + contents[6];
        return monsterInfor;
    }
}

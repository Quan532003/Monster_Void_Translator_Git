using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpInforController : MonoBehaviour
{
    public RectTransform contentInfor;
    List<Button> monsterSelect = new List<Button>();
    public GameObject monsterInforPopUp;
    public GameObject monsterSelectPopUp;

    public Text monsterName;
    public Text monsterNickName;
    public Text monsterGender;
    public Text monsterBirthDay;
    public Text monsterOrigin;
    public Text monsterFeature;

    public List<Sprite> avatar = new List<Sprite>();

    public Button closeBtn;
    public Text textInTittle;

    public List<Text> monsterNames = new List<Text>();
    public RectTransform contentInShowInfor;
    List<string> nameTxt = new List<string>
    {
        "Baby Opila Birds",
        "Bunbuleena",
        "Bunbun",
        "Blue",
        "Boogie",
        "Boxy Box",
        "Big pigster",
        "Cap Fiddless",
        "Syan",
        "Green", "New wuggy", "Jubo Jesh", "Mommy Mommy",
        "Nubnub", "Orange", "Purple", "Skubidu", "Slow Slainy",
        "Mr.Stinger", "Tarta", "Yellow", "Zamazaki & Zamataki"
    };

    List<int> widthOfContent = new List<int> { 1815,2300,2840,2410,2410,3755,1856,2485,1840,2250,3560,2110,
                                                4515,1910,2400,2400,2700,1676,2500,2215,2215,2300};
    public Image monsterAvatar;
    List<GameObject> lockInfor = new List<GameObject>();
    public static PopUpInforController Instance;
    private void Awake()
    {
        Instance = this;
        for(int i = 0; i < contentInfor.childCount; i++)
        {
            monsterSelect.Add(contentInfor.GetChild(i).GetComponent<Button>());
        }
        for(int i = 0; i < monsterSelect.Count; i++)
        {
            lockInfor.Add(monsterSelect[i].GetComponent<RectTransform>().GetChild(2).gameObject);
        }
        SetLockBtn();
        SetBtnMonsterClicked();
        closeBtn.onClick.AddListener(CloseBtnClicked);
        for(int i = 0; i < monsterSelect.Count; i++)
        {
            monsterNames.Add(monsterSelect[i].GetComponent<RectTransform>().GetChild(0).GetComponent<Text>());
        }
        for(int i =0; i < monsterNames.Count; i++)
        {
            monsterNames[i].text = nameTxt[i];
        }
    }

    public void SetLockBtn()
    {
        for(int i =0; i < lockInfor.Count; i++)
        {
            lockInfor[i].SetActive(false);
        }
        var index = Helper.ConvertFromMonsterLockToInt();
        for (int i = 0; i < index.Count; i++)
        {
            lockInfor[index[i]].SetActive(true);
        }
    }
    public void SetBtnMonsterClicked()
    {
        for (int i = 0; i < monsterSelect.Count; i++)
        {
            int index = i;
            monsterSelect[index].onClick.AddListener(() =>
            {
                OnSelectMonsterBtnClicked(index);
            });
        }
    }
    void OnSelectMonsterBtnClicked(int index)
    {
        if (!lockInfor[index].activeInHierarchy)
        {
            monsterSelectPopUp.SetActive(false);
            monsterInforPopUp.SetActive(true);
            MonsterInfor infor = FileController.ReadFile(index);
            SetTextAndAvatar(infor, index);
        }
        else
        {
            lockInfor[index].SetActive(false);
            Helper.SetLockMonster(index);
            monsterSelectPopUp.SetActive(false);
            monsterInforPopUp.SetActive(true);
            MonsterInfor infor = FileController.ReadFile(index);
            SetTextAndAvatar(infor, index);
        }
    }

    public void SetTextAndAvatar(MonsterInfor infor, int index)
    {
        monsterName.text = infor.name;
        monsterNickName.text = infor.nickName;
        monsterBirthDay.text = infor.birthDay;
        monsterOrigin.text = infor.Origin;
        monsterGender.text = infor.gender;
        monsterFeature.text = infor.featuresAndStory;
        textInTittle.text = infor.name;
        var contentSize = contentInShowInfor.sizeDelta;
        contentSize.y = widthOfContent[index] * 1f;
        contentInShowInfor.sizeDelta = contentSize;
        monsterAvatar.sprite = avatar[index];
    }

    public void CloseBtnClicked()
    {
        monsterSelectPopUp.SetActive(true);
        monsterInforPopUp.SetActive(false);
    }
}

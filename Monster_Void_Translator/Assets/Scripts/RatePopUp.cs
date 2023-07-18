using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatePopUp : MonoBehaviour
{
    public List<Button> brownStars;
    public List<Button> yellowStars;
    public Button rateBtn;
    public Button laterBtn;
    private void Awake()
    {
        for(int i = 0; i < brownStars.Count; i++)
        {
            int index = i;
            brownStars[index].onClick.AddListener(() =>
            {
                OnBrownStarBtnClicked(index);
            });
            yellowStars[index].onClick.AddListener(() =>
            {
                OnBrownStarBtnClicked(index);
            });
        }
        laterBtn.onClick.AddListener(OnLaterBtnClicked);
    }
    void OnBrownStarBtnClicked(int index)
    {
        for(int i = 0; i <= index; i++)
        {
            brownStars[i].gameObject.SetActive(false);
            yellowStars[i].gameObject.SetActive(true);
        }
        for(int i = index + 1; i < brownStars.Count; i++)
        {
            brownStars[i].gameObject.SetActive(true);
            yellowStars[i].gameObject.SetActive(false);
        }
    }
    void OnLaterBtnClicked()
    {
        PopUpMovement.Instance.HidePopUp(this.gameObject);
    }
}

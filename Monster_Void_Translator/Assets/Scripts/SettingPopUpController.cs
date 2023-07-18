using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopUpController : MonoBehaviour
{
    public Button settingBtn;
    public Button closeBtn;
    public Button vibrationBtn;
    public Button noAdBtn;
    public Button rateBtn;
    public GameObject settingPopUp;
    public GameObject ratePopUp;

    private void Awake()
    {
        settingBtn.onClick.AddListener(OnSettingBtn);
        closeBtn.onClick.AddListener(OnCloseBtn);
        vibrationBtn.onClick.AddListener(OnVibrationBtnClicked);
        noAdBtn.onClick.AddListener(OnNoAdBtnClicked);
        rateBtn.onClick.AddListener(OnRateBtnClicked);
    }
    public void OnSettingBtn()
    {
        PopUpMovement.Instance.ShowPopUp(settingPopUp);
    }
    public void OnCloseBtn()
    {
        PopUpMovement.Instance.HidePopUp(settingPopUp);
    }
    public void OnVibrationBtnClicked()
    {
    }
    public void OnNoAdBtnClicked()
    {

    }
    public void OnRateBtnClicked()
    {
        PopUpMovement.Instance.ShowPopUp(ratePopUp);
    }
}
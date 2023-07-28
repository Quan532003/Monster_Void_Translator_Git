using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public Slider slider;
    public void Load(int scene)
    {
        slider.gameObject.SetActive(true);
        slider.DOValue(1f, 3f).OnComplete(()=>
        {
            StartCoroutine(loadScene(1));
        });
        
        

    }
    bool start = false;

    private void Update()
    {
        if(!start)
        {
            Load(1);
            start = true;
        }
    }
    IEnumerator waitforSecond(float time, Action A)
    {
        yield return new WaitForSeconds(time);
        A?.Invoke();
    }
    IEnumerator loadScene(int scene)
    {
        yield return new WaitForSeconds(0.1f);
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(scene);

        while (!loadOperation.isDone)
        {
            yield return null;
        }
    }
}

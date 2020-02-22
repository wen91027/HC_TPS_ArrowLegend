﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    [Header("是否一開始顯示隨機技能")]
    public bool showRandomSkill;
    [Header("是否自動開門")]
    public bool autoOpenDoor;
    [Header("隨機技能介面")]
    public GameObject randomSkill;
    [Header("是否為魔王關")]
    public bool isboss;

    private Animator door;              // 門
    private Image cross;                // 轉場畫面
    private CanvasGroup panelRevival;   // 復活畫面
    private Text textCountRevival;      // 復活倒數秒數
    private GameObject panelResult;
    private AdsManager adManager;

    private void Start()
    {
        door = GameObject.Find("門").GetComponent<Animator>();
        cross = GameObject.Find("轉場畫面").GetComponent<Image>();

        adManager = FindObjectOfType<AdsManager>();
        panelRevival = GameObject.Find("復活畫面").GetComponent<CanvasGroup>();
        textCountRevival = panelRevival.transform.Find("倒數秒數").GetComponent<Text>();
        panelRevival.transform.Find("看廣告復活").GetComponent<Button>().onClick.AddListener(adManager.ShowAD);

        panelResult = GameObject.Find("結算畫面");
        panelResult.GetComponent<Button>().onClick.AddListener(BackToMenu);

        if (autoOpenDoor) Invoke("OpenDoor", 6);    // 延遲調用("方法名稱"，延遲時間)
        if (showRandomSkill) ShowRandomSkill();
    }


    private void BackToMenu()
    {
        SceneManager.LoadScene("選單畫面");
    }


    public void ShowResult()
    {
        panelResult.GetComponent<CanvasGroup>().alpha = 1;
        panelResult.GetComponent<CanvasGroup>().interactable = true;
        panelResult.GetComponent<CanvasGroup>().blocksRaycasts = true;
        panelResult.GetComponent<Animator>().SetTrigger("結算畫面觸發");
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        panelResult.transform.Find("關卡名稱").GetComponent<Text>().text = "Lv : " + currentLevel.ToString();
    }


    /// <summary>
    /// 顯示隨機技能介面
    /// </summary>
    private void ShowRandomSkill()
    {
        randomSkill.SetActive(true);
    }

    /// <summary>
    /// 播放開門動畫
    /// </summary>
    private void OpenDoor()
    {
        door.SetTrigger("開門");  // 動畫控制器.設定觸發("參數名稱")
    }

    /// <summary>
    /// 載入關卡
    /// </summary>
    private IEnumerator LoadLevel()
    {

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;       //抓當前索引值
        AsyncOperation ao = SceneManager.LoadSceneAsync(++sceneIndex);   // 載入場景資訊 = 載入場景("場景名稱")
        ao.allowSceneActivation = false;                                // 載入場景資訊.是否允許切換 = 否

        while (!ao.isDone)                                              // 當(載入場景資訊.是否完成 為 否)
        {
            print(ao.progress);
            cross.color = new Color(1, 1, 1, ao.progress);              // 轉場畫面.顏色 = 新 顏色(1，1，1，透明度) // ao.progress 載入進度 0 - 0.9
            yield return new WaitForSeconds(0.01f);

            if (ao.progress >= 0.9f) ao.allowSceneActivation = true;    // 當 載入進度 >= 0.9 允許切換
        }
    }

    /// <summary>
    /// 復活畫面倒數方法
    /// </summary>
    public IEnumerator CountDownRevival()
    {
        panelRevival.alpha = 1;                             // 顯示復活畫面
        panelRevival.interactable = true;                   // 可互動
        panelRevival.blocksRaycasts = true;                 // 阻擋射線

        for (int i = 3; i >= 0; i--)                        // 迴圈跑三次：3、2、1、0
        {
            textCountRevival.text = i.ToString();           // 更新復活倒數秒數
            yield return new WaitForSeconds(1);             // 等待一秒
        }

        panelRevival.alpha = 0;                             // 隱藏復活畫面
        panelRevival.interactable = false;                  // 不可互動
        panelRevival.blocksRaycasts = false;                // 不阻擋射線

        if (!AdsManager.lookAD)
        {
            SceneManager.LoadScene("選單畫面");    //倒數完回到選單
        }
        AdsManager.lookAD = false;
    }

    /// <summary>
    /// 關閉復活畫面
    /// </summary>
    public void CloseRevival()
    {
        StopCoroutine(CountDownRevival());
        panelRevival.alpha = 0;                             // 隱藏復活畫面
        panelRevival.interactable = false;                  // 不可互動
        panelRevival.blocksRaycasts = false;                // 不阻擋射線
    }

    public void PassLevel()
    {
        OpenDoor();


    }

}

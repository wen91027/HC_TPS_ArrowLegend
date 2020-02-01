using UnityEngine;
using UnityEngine.Advertisements;   // 引用 廣告 API

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    private string googleID = "3436899";            // Google 專案 ID
    private bool testMode = true;                   // 測試模式：允許在 Unity 內側試
    private string placemnetRevival = "revival";    // 廣告類型：復活
    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        Advertisement.AddListener(this);
        Advertisement.Initialize(googleID, testMode);   // 廣告.初始化(ID，測試模式);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ShowAD();
        }
    }

    public void ShowAD()
    {
        if (Advertisement.IsReady(placemnetRevival))
        {
            Advertisement.Show(placemnetRevival);
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
    }

    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Failed:
                Debug.Log("失敗");
                break;
            case ShowResult.Skipped:
                Debug.Log("略過");
                break;
            case ShowResult.Finished:
                player.Revival();
                Debug.Log("成功");
                break;
        }
    }
}

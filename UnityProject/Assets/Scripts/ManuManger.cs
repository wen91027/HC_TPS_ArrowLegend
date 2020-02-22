using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManuManger : MonoBehaviour
{

    [Header("玩家資料")]
    public PlayerData data;


    // Start is called before the first frame update
    public void LoadLevel()
    {
        SceneManager.LoadScene("關卡1");
    }

    public void BuyHp_500()
    {
        data.hpMax += 500;
        data.hp = data.hpMax;
    }
    public void BuyAtk_50()
    {
        data.attack += 50;
    }

}

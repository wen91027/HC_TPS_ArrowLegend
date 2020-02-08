using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public bool gotoplayer; //是否飛向角色
    [Header("道具音效")]
    public AudioClip sound;


    private Transform player;
    private AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        HandleCollision();
        aud = GetComponent<AudioSource>();
        player = GameObject.Find("玩家").transform;
        
        StartCoroutine("EnemyDieTime");
    }
    private void Update()
    {
        GoToPlayer();
    }


    /// <summary>
    /// 管理碰撞
    /// </summary>
    // Update is called once per frame
    private void HandleCollision()
    {
        Physics.IgnoreLayerCollision(10, 8); //忽略圖層碰撞
        Physics.IgnoreLayerCollision(10, 9);
    }

    private void GoToPlayer()
    {
        if (gotoplayer)
        {
            transform.position = Vector3.Lerp(transform.position, player.position, 0.5f * 10*Time.deltaTime);
        if(Vector3.Distance(transform.position,player.position)<3f)
            {
                aud.PlayOneShot(sound, 0.1f);
                Destroy(gameObject, 1f);  //延遲刪除物件
            }
        
        
        
        }

    }

    IEnumerator EnemyDieTime()
    {
        yield return new WaitForSeconds(0.5f);
        gotoplayer = true;
    }


}

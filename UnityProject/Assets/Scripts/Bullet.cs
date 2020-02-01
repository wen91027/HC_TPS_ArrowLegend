using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;    // 接收遠攻敵人的攻擊力
    public bool players; //判斷子彈是否為玩家擁有 true玩家 false怪

    private void OnTriggerEnter(Collider other)
    {
        if (!players)
        {
            if (other.tag == "Player")                          // 如果 碰到物件.標籤 為 "Player"
            {
                other.GetComponent<Player>().Hit(damage);       // 碰到物件.取得元件<Player>().受傷(攻擊力);
            }
        }
        else { 
        if (other.tag == "Enemy")                          // 如果 碰到物件.標籤 為 "Player"
        {
            other.GetComponent<Enemy>().Hit(damage);       // 碰到物件.取得元件<Player>().受傷(攻擊力);
        }
        }
    }
}

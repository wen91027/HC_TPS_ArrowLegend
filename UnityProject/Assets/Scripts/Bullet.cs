using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;    // 接收遠攻敵人的攻擊力

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")                          // 如果 碰到物件.標籤 為 "Player"
        {
            other.GetComponent<Player>().Hit(damage);       // 碰到物件.取得元件<Player>().受傷(攻擊力);
        }
    }
}


using UnityEngine;

public class Bullet:MonoBehaviour
{
    [SerializeField]private int damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Health>().GetDamage(damage);
            Destroy(gameObject);
        }
    }
}
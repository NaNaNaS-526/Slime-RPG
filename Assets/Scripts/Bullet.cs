
using UnityEngine;

public class Bullet:MonoBehaviour
{
    public int damage;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Health>().GetDamage(damage);
            Destroy(gameObject);
        }
        Destroy(gameObject, 3);
    }
}
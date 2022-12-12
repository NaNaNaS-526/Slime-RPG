using System.Collections;
using UnityEngine;

public class Player : Creature
{
    [SerializeField] private int damage;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float angleInDegrees;
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float timeBetweenShots;
    private readonly float _g = Physics.gravity.y;
    private int _money;

    private void OnEnable()
    {
        EventBus.OnDead += Move;
        EventBus.OnGotMoney += GetMoney;
        EventBus.OnAttackDamageEnhanced += EnhanceAttackDamage;
        EventBus.OnAttackSpeedEnhanced += EnhanceAttackSpeed;
        EventBus.OnSpentMoney += SpendMoney;
    }

    private void OnDisable()
    {
        EventBus.OnDead -= Move;
        EventBus.OnGotMoney -= GetMoney;
        EventBus.OnAttackDamageEnhanced -= EnhanceAttackDamage;
        EventBus.OnAttackSpeedEnhanced -= EnhanceAttackSpeed;
        EventBus.OnSpentMoney -= SpendMoney;
    }

    protected override void Move()
    {
        CurrentAction = Action.Move;
        Rigidbody.velocity = Vector3.right * (speed * Time.fixedDeltaTime);
        StopAllCoroutines();
    }

    protected void Update()
    {
        var playerTransform = transform;
        spawnTransform.localEulerAngles = new Vector3(-angleInDegrees, 0f, 0f);
        if (!targetTransform && CurrentAction == Action.Move)
        {
            Ray ray = new Ray
            {
                origin = playerTransform.position,
                direction = playerTransform.forward
            };
            Physics.Raycast(ray, out var hit);

            if (hit.collider.CompareTag("Enemy") && hit.distance < 4)
            {
                CurrentAction = Action.Attack;
                targetTransform = hit.collider.gameObject.transform;
                StartCoroutine(Shoot());
            }
        }
    }

    private IEnumerator Shoot()
    {
        while (targetTransform)
        {
            Vector3 fromTo = targetTransform.position - transform.position;
            Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

            transform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);


            float x = fromToXZ.magnitude;
            float y = fromTo.y;

            float angleInRadians = angleInDegrees * Mathf.PI / 180;

            float v2 = (_g * x * x) /
                       (2 * (y - Mathf.Tan(angleInRadians) * x) * Mathf.Pow(Mathf.Cos(angleInRadians), 2));
            float v = Mathf.Sqrt(Mathf.Abs(v2));

            GameObject newBullet = Instantiate(bullet, spawnTransform.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody>().velocity = spawnTransform.forward * v;
            newBullet.GetComponent<Bullet>().damage = damage;
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    private void GetMoney(int amount)
    {
        _money += amount;
        EventBus.OnMoneyUpdated(_money);
    }

    private void SpendMoney(int amount)
    {
        _money -= amount;
        EventBus.OnMoneyUpdated(_money);
    }

    private void EnhanceAttackDamage()
    {
        damage += 10;
        EventBus.OnAttackDamageUpdated(damage);
    }

    private void EnhanceAttackSpeed()
    {
        if (timeBetweenShots > 0.5f) timeBetweenShots -= 0.5f;
        EventBus.OnAttackSpeedUpdated(timeBetweenShots);
    }
}
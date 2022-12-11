using System.Collections;
using UnityEngine;

public class Player : Creature
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float angleInDegrees;
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private Transform targetTransform;
    private readonly float _g = Physics.gravity.y;
    private int _money;

    private void OnEnable()
    {
        EventBus.OnDead += GetMoney;
        EventBus.OnDead += Move;
    }

    private void OnDisable()
    {
        EventBus.OnDead -= GetMoney;
    }

    protected override void Move()
    {
        CurrentAction = Action.Move;
        Rigidbody.velocity = Vector3.right * (speed * Time.fixedDeltaTime);
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
                direction = playerTransform.right
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
            yield return new WaitForSeconds(1);
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
        }
    }

    private void GetMoney()
    {
        _money += 10;
    }
}
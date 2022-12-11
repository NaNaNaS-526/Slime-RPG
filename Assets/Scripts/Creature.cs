using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    [SerializeField] protected float speed;

    protected enum Action
    {
        Move,
        Attack
    }

    protected Action CurrentAction = Action.Move;
    protected Rigidbody Rigidbody;

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (CurrentAction == Action.Move) Move();
        else Attack();
    }

    protected virtual void Move()
    {
        Rigidbody.velocity = Vector3.left * (speed * Time.fixedDeltaTime);
    }

    private void Attack()
    {
        Rigidbody.velocity = Vector3.zero;
    }
}
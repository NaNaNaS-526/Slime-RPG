using UnityEngine;

public class Enemy : Creature
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) CurrentAction = Action.Attack;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) CurrentAction = Action.Move;
    }
}
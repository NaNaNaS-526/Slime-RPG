using System.Collections;
using UnityEngine;

public class Enemy : Creature
{
    [SerializeField] private float timeBetweenAttacks;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CurrentAction = Action.Attack;
            StartCoroutine(Attack(other.gameObject.GetComponent<Health>()));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) CurrentAction = Action.Move;
    }

    private IEnumerator Attack(Health playerHealth)
    {
        while (playerHealth.gameObject)
        {
            playerHealth.GetDamage(10);
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
    }
}
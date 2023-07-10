using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTrap : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
        {
            other.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 2;
            StartCoroutine(Slow(other));
        }
    }
    IEnumerator Slow(Collider other)
    {
        float timer = 0;
        
        while (timer < 5)
        {
            timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, timer);
            yield return null;
        }
        other.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 10;
        GameObject.Destroy(gameObject);
    }
}

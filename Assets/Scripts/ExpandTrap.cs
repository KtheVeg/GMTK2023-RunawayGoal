using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandTrap : MonoBehaviour
{
    [SerializeField]
    GameObject trapExpander;
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
        {
            StartCoroutine(Expand());
        }
    }
    IEnumerator Expand()
    {
        float timer = 0;
        trapExpander.SetActive(true);
        while (timer < 1)
        {
            timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, timer);
            trapExpander.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * 10, timer);
            yield return null;
        }
        GameObject.Destroy(trapExpander);
        GameObject.Destroy(gameObject);
    }
}

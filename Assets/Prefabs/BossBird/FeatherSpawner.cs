using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherSpawner : MonoBehaviour
{
    [SerializeField] GameObject FeatherPrefab;
    [SerializeField] int limit = 5;

    private int created = 0;

    private bool isActive = false;

    void FixedUpdate()
    {
        if (transform.parent.GetComponent<DeathbirdBehaviour>().bossState == DeathbirdBehaviour.BossState.Normal && isActive && transform.parent.GetComponentInChildren<FollowPlayerNonTrigger>() is null)
        {
            created++;
            new WaitForSecondsRealtime(3f);
        }
    }

    public void ResetSpawner()
    {
        isActive = true;
        StartCoroutine(SpawnFeathers());
    }

    public void DeactivateSpawner()
    {
        isActive = false;
    }

    IEnumerator SpawnFeathers()
    {
        while (isActive)
        {
            GameObject createdFeather = Instantiate(FeatherPrefab, transform.position, transform.rotation, transform.parent);
            createdFeather.GetComponent<FollowPlayerNonTrigger>().SetPlayer(FindObjectOfType<FrogControllerForce>().gameObject);
            yield return new WaitForSecondsRealtime(3f);
        }
        
    }
}

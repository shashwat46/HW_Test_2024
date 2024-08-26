using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulpitManager : MonoBehaviour
{
    public GameObject pulpitPrefab;
    public float minPulpitDestroyTime = 4f;
    public float maxPulpitDestroyTime = 5f;
    public float pulpitSpawnTime = 2.5f;
    public float platformSize = 9f;

    private List<GameObject> activePulpits = new List<GameObject>();
    private Vector3 lastPulpitPosition;

    private void Start()
    {
        SpawnInitialPulpit();
        StartCoroutine(PulpitSpawnRoutine());
    }

    private void SpawnInitialPulpit()
    {
        Vector3 initialPosition = Vector3.zero;
        SpawnPulpit(initialPosition);
    }

    private IEnumerator PulpitSpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(pulpitSpawnTime);
            if (activePulpits.Count < 2)
            {
                SpawnAdjacentPulpit();
            }
        }
    }

    private void SpawnAdjacentPulpit()
    {
        Vector3[] possiblePositions = new Vector3[]
        {
            lastPulpitPosition + Vector3.right * platformSize,
            lastPulpitPosition + Vector3.left * platformSize,
            lastPulpitPosition + Vector3.forward * platformSize,
            lastPulpitPosition + Vector3.back * platformSize
        };

        List<Vector3> validPositions = new List<Vector3>();
        foreach (Vector3 pos in possiblePositions)
        {
            if (!IsPulpitAtPosition(pos))
            {
                validPositions.Add(pos);
            }
        }

        if (validPositions.Count > 0)
        {
            int randomIndex = Random.Range(0, validPositions.Count);
            SpawnPulpit(validPositions[randomIndex]);
        }
    }

    private void SpawnPulpit(Vector3 position)
    {
        GameObject pulpit = Instantiate(pulpitPrefab, position, Quaternion.identity);
        activePulpits.Add(pulpit);
        lastPulpitPosition = position;

        float destroyTime = Random.Range(minPulpitDestroyTime, maxPulpitDestroyTime);
        
        PulpitTimer timer = pulpit.GetComponent<PulpitTimer>();
        if (timer == null)
        {
            timer = pulpit.AddComponent<PulpitTimer>();
        }
        timer.Initialize(destroyTime);

        StartCoroutine(RemovePulpitAfterTime(pulpit, destroyTime));
    }

    private IEnumerator RemovePulpitAfterTime(GameObject pulpit, float time)
    {
        yield return new WaitForSeconds(time);
        if (pulpit != null)
        {
            activePulpits.Remove(pulpit);
            Destroy(pulpit);
        }
    }

    private bool IsPulpitAtPosition(Vector3 position)
    {
        foreach (GameObject pulpit in activePulpits)
        {
            if (pulpit.transform.position == position)
            {
                return true;
            }
        }
        return false;
    }
}
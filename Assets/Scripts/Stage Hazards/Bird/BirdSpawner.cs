using System.Collections;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject template;

    public float spacing;

    public float simulateTime;

    void Start()
    {
        float remainingTime = simulateTime;

        while (remainingTime >= spacing)
        {
            remainingTime -= spacing;
            Instantiate(template, SimulateMovement(remainingTime), Quaternion.identity);
        }

        StartCoroutine(Spawner(spacing - remainingTime));
    }

    private Vector3 SimulateMovement(float time)
    {
        var behavior = template.GetComponent<BirdBehavior>();
        if (behavior.flightTime > 0)
        {
            int passes = Mathf.FloorToInt(time / behavior.flightTime);
            float remainder = time % behavior.flightTime;

            if (passes % 2 == 0)
            {
                return transform.position + (Vector3)(behavior.flightSpeed * remainder);
            } else
            {
                return transform.position + (Vector3)(behavior.flightSpeed * (behavior.flightTime - remainder));
            }
        } else
        {
            return transform.position + (Vector3)(behavior.flightSpeed * time);
        }
    }

    private IEnumerator Spawner(float firstTime)
    {
        yield return new WaitForSeconds(firstTime);

        while (true)
        {
            Instantiate(template, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spacing);
        }
    }
}

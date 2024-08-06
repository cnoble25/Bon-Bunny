using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SpaceShipSpawner : MonoBehaviour
{
    [SerializeField] List<Transform> PointToGoFrom;
    
    private float Angle;

    private float xAngle;

    private float yAngle;

    private bool canFire;

    private Random Rand;

    private int point;

    [SerializeField] private float speed;

    [SerializeField] private float time;

    [SerializeField] private GameObject SpaceShip;
    
    // Update is called once per frame

    void Awake()
    {
        Rand = new Random();
        canFire = true;
    }
    void FixedUpdate()
    {
        if (canFire)
        {
            point = Rand.Next(0, PointToGoFrom.Count);
            Angle = PointToGoFrom[point].transform.rotation.eulerAngles.z;
            xAngle = Mathf.Cos(Angle * Mathf.PI / 180);
            yAngle = Mathf.Sin(Angle * Mathf.PI / 180);
            canFire = false;
            StartCoroutine(MakeShip());
        }
    }
    
    IEnumerator MakeShip()
    {
        PointToGoFrom[point].transform.GetComponentInChildren<ParticleSystem>().Stop();
        PointToGoFrom[point].transform.GetComponentInChildren<ParticleSystem>().Play();
        yield return new WaitForSeconds(3f);
        GameObject newShip = Instantiate(SpaceShip, PointToGoFrom[point].transform.position, Quaternion.identity);
        newShip.GetComponent<AsteroidMovement>().direction = new Vector2(xAngle, yAngle);
        newShip.GetComponent<AsteroidMovement>().Magnitude = speed;
        yield return new WaitForSeconds(time);
        canFire = true;
    }
}

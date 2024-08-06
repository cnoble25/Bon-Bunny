using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCreator : MonoBehaviour
{
    private float Angle;

    private float xAngle;

    private float yAngle;

    private bool canFire;

    private IEnumerator makeAsteroid;

    [SerializeField] private float speed;

    [SerializeField] private float time;

    [SerializeField] private GameObject Asteroid;


    private void Awake()
    {
        makeAsteroid = MakeAsteroid();
        canFire = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
         Angle = transform.rotation.eulerAngles.z;
         xAngle = Mathf.Cos(Angle * Mathf.PI / 180);
         yAngle = Mathf.Sin(Angle * Mathf.PI / 180);

        if (canFire)
        {
            canFire = false;
            StartCoroutine(MakeAsteroid());
        }
    }

    IEnumerator MakeAsteroid()
    {
        GameObject newAsteroid = Instantiate(Asteroid, transform.position, Quaternion.identity);
        newAsteroid.GetComponent<AsteroidMovement>().direction = new Vector2(xAngle, yAngle);
        newAsteroid.GetComponent<AsteroidMovement>().Magnitude = speed;
        newAsteroid.GetComponent<AsteroidMovement>().rotationSpeed = 50;
        yield return new WaitForSeconds(time);
        canFire = true;
    }
}

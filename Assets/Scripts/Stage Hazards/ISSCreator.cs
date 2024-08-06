using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISSCreator : MonoBehaviour
{
    private float Angle;

    private float xAngle;

    private float yAngle;

    private bool canFire;

    [SerializeField] private float speed;

    [SerializeField] private float time;

    [SerializeField] private GameObject ISS;


    private void Awake()
    {
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
            StartCoroutine(MakeISS());
        }
    }

    IEnumerator MakeISS()
    {
        GameObject newISS = Instantiate(ISS, transform.position, Quaternion.identity);
        newISS.GetComponent<AsteroidMovement>().direction = new Vector2(xAngle, yAngle);
        newISS.GetComponent<AsteroidMovement>().Magnitude = speed;
        yield return new WaitForSeconds(time);
        canFire = true;
    }
}

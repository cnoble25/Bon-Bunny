using UnityEngine;

public class AsteroidLauncher : MonoBehaviour
{
    [SerializeField]
    private int columns = 5;

    [SerializeField]
    private int[] firingPattern;

    [SerializeField]
    private GameObject launcher;

    [SerializeField]
    private GameObject asteroid;

    public float asteroidSpeed = 2f;

    [SerializeField]
    private float launcherSpacing = 0.5f;

    [SerializeField]
    private float firingDelay = 5f;

    [SerializeField]
    private float warningTime = 3f;

    private GameObject[] launchers;

    private float lastFiringTime;
    private int firingIdx = 0;
    private bool fired = false;

    void Start()
    {
        Vector3 position = new(launcherSpacing * (columns-1) / 2, 0, 0);

        launchers = new GameObject[columns];

        for (int i = 0; i < columns; i++) {
            launchers[i] = Instantiate(launcher, gameObject.transform);
            launchers[i].transform.localPosition = position;
            launchers[i].SetActive(false);
            position.x -= launcherSpacing;
        }

        lastFiringTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float diff = Time.time - lastFiringTime;

        if (diff >= (firingDelay + warningTime))
        {
            fired = false;
            lastFiringTime = Time.time;
            firingIdx++;
            if (firingIdx >= firingPattern.Length) firingIdx = 0;

            foreach (var launcher in launchers)
            {
                launcher.SetActive(false);
            }
        } else if (diff >= firingDelay) {
            if (!fired) {
                int pattern = firingPattern[firingIdx];

                float zrot = gameObject.transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
                Vector2 rotation = new(Mathf.Sin(zrot), -Mathf.Cos(zrot));
                foreach (var launcher in launchers)
                {
                    int value = pattern % 10;
                    pattern /= 10;

                    if (value == 0) continue;
                    launcher.SetActive(true);

                    const float rockSpacing = 2.5f;
                    Vector2 position = (Vector2)launcher.transform.position - rotation * (asteroidSpeed * warningTime);
                    for (int i = 0; i < value; i++)
                    {
                        var rock = Instantiate(asteroid, position, Quaternion.identity);
                        var controller = rock.GetComponent<AsteroidMovement>();
                        controller.direction = rotation;
                        controller.Magnitude = asteroidSpeed;

                        position -= rotation * rockSpacing;
                    }
                }
                fired = true;
            }

            Color opacity = new(1, 1, 1, Mathf.Abs(0.5f - (diff % 1f)) * 2);

            foreach (var launcher in launchers)
            {
                launcher.GetComponent<SpriteRenderer>().color = opacity;
            }
        }
    }
}

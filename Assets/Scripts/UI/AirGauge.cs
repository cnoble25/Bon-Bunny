using UnityEngine;
using UnityEngine.UI;

public class AirGauge : MonoBehaviour
{
    void Update()
    {
        if (PlayerStateManager.instance != null && PlayerStateManager.instance.player != null)
        {
            var movement = PlayerStateManager.instance.player.GetComponent<PlayerMovement>();
            GetComponent<Image>().fillAmount = Mathf.Clamp01((movement.GasVolume + 0.7f) / 0.7f);
        }
    }
}

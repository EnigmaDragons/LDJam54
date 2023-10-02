using UnityEngine;

public class KPIDisplayToggler : MonoBehaviour
{
    public Canvas KPICanvas;

    public void Start()
    {
        KPICanvas.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        KPICanvas.enabled = !WorkdayState.IsWorkdayEnded;
    }
}

using System.Linq;
using UnityEngine;

public class ShippingUIToggler : MonoBehaviour
{
    [SerializeField] private GameObject shippingScreen;

    public void Start()
    {
        shippingScreen.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        shippingScreen.SetActive(!WorkdayState.IsWorkdayEnded && CurrentGameState.State.ActiveKPIs.Contains(KPI.BoxesShipped));
    }
}

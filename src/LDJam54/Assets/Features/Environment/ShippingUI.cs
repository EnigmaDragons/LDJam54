using System.Linq;
using TMPro;
using UnityEngine;

public class ShippingUI : OnMessage<ShipmentWantedUpdated, GameStateChanged>
{
    [SerializeField] private ShipmentWanted shipment;
    [SerializeField] private GameObject shippingScreen;
    [SerializeField] private TextMeshProUGUI size;

    protected override void Execute(ShipmentWantedUpdated msg)
    {
        size.text = $"{shipment.Size.ToString()} {shipment.Color.ToString()} Box";
    }

    protected override void Execute(GameStateChanged msg)
    {
        shippingScreen.SetActive(CurrentGameState.State.ActiveKPIs.Contains(KPI.BoxesShipped));
    }
}
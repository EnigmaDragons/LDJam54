using System.Linq;
using TMPro;
using UnityEngine;

public class ShippingUI : OnMessage<ShipmentWantedUpdated, GameStateChanged>
{
    [SerializeField] private ShipmentWanted shipment;
    [SerializeField] private GameObject shippingScreen;
    [SerializeField] private TextMeshProUGUI size;

    private bool _active;

    private void Awake()
    {
        shippingScreen.SetActive(_active);
    }

    protected override void Execute(ShipmentWantedUpdated msg)
    {
        size.text = $"{shipment.Size.ToString()} {shipment.Color.ToString()} Box";
    }

    protected override void Execute(GameStateChanged msg)
    {
        var boxesShippedKpiActive = CurrentGameState.State.ActiveKPIs.Contains(KPI.BoxesShipped);
        if (_active && !boxesShippedKpiActive)
        {
            _active = false;
            shippingScreen.SetActive(_active);
        }
        else if (!_active && boxesShippedKpiActive)
        {
            _active = true;
            shippingScreen.SetActive(_active);
        }
    }
}
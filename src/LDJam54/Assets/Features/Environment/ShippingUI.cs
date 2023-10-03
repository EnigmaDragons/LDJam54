using System.Linq;
using TMPro;
using UnityEngine;

public class ShippingUI : OnMessage<ShipmentWantedUpdated>
{
    [SerializeField] private ShipmentWanted shipment;
    [SerializeField] private TextMeshProUGUI size;

    protected override void Execute(ShipmentWantedUpdated msg)
    {
        size.text = $"{shipment.Size.ToString()} {shipment.Color.ToString()} Box";
    }
}

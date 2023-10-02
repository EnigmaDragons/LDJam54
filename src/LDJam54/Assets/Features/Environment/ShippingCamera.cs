using UnityEngine;

public class ShippingCamera : OnMessage<ShipmentWantedUpdated>
{
    [SerializeField] private ShipmentWanted shipment;
    [SerializeField] private GameObject smallRed;
    [SerializeField] private GameObject smallBlue;
    [SerializeField] private GameObject smallGreen;
    [SerializeField] private GameObject smallYellow;
    [SerializeField] private GameObject smallPurple;
    [SerializeField] private GameObject mediumRed;
    [SerializeField] private GameObject mediumBlue;
    [SerializeField] private GameObject mediumGreen;
    [SerializeField] private GameObject mediumYellow;
    [SerializeField] private GameObject mediumPurple;
    [SerializeField] private GameObject largeRed;
    [SerializeField] private GameObject largeBlue;
    [SerializeField] private GameObject largeGreen;
    [SerializeField] private GameObject largeYellow;
    [SerializeField] private GameObject largePurple;

    private void Awake() => SetBoxActive();

    protected override void Execute(ShipmentWantedUpdated msg) => SetBoxActive();

    private void SetBoxActive()
    {
        smallRed.SetActive(shipment.Size == DropSize.Small && shipment.Color == SortingColor.Red);
        smallBlue.SetActive(shipment.Size == DropSize.Small && shipment.Color == SortingColor.Blue);
        smallGreen.SetActive(shipment.Size == DropSize.Small && shipment.Color == SortingColor.Green);
        smallYellow.SetActive(shipment.Size == DropSize.Small && shipment.Color == SortingColor.Yellow);
        smallPurple.SetActive(shipment.Size == DropSize.Small && shipment.Color == SortingColor.Purple);
        mediumRed.SetActive(shipment.Size == DropSize.Medium && shipment.Color == SortingColor.Red);
        mediumBlue.SetActive(shipment.Size == DropSize.Medium && shipment.Color == SortingColor.Blue);
        mediumGreen.SetActive(shipment.Size == DropSize.Medium && shipment.Color == SortingColor.Green);
        mediumYellow.SetActive(shipment.Size == DropSize.Medium && shipment.Color == SortingColor.Yellow);
        mediumPurple.SetActive(shipment.Size == DropSize.Medium && shipment.Color == SortingColor.Purple);
        largeRed.SetActive(shipment.Size == DropSize.Large && shipment.Color == SortingColor.Red);
        largeBlue.SetActive(shipment.Size == DropSize.Large && shipment.Color == SortingColor.Blue);
        largeGreen.SetActive(shipment.Size == DropSize.Large && shipment.Color == SortingColor.Green);
        largeYellow.SetActive(shipment.Size == DropSize.Large && shipment.Color == SortingColor.Yellow);
        largePurple.SetActive(shipment.Size == DropSize.Large && shipment.Color == SortingColor.Purple);
    }
}
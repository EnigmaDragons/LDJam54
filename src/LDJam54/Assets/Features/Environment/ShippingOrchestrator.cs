using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShippingOrchestrator : OnMessage<BoxShipped, ColoredBoxAppeared, ColoredBoxDisappeared>
{
    [SerializeField] private ShipmentWanted shipment;

    private bool _showingShipping;
    private void Start()
    {
        _showingShipping = false;
        RandomizeBoxWanted();
    }

    protected override void Execute(BoxShipped msg)
    {
        if (msg.Box.Color == shipment.Color && msg.Box.Size == shipment.Size)
        {
            CurrentGameState.IncrementKPIStatic(KPI.BoxesShipped);
            RandomizeBoxWanted();
        }
        else
        {
            CurrentGameState.IncrementKPIStatic(KPI.BoxShippingMistakes);
        }
    }

    private List<ColoredBox> _boxes = new List<ColoredBox>();

    protected override void Execute(ColoredBoxAppeared msg)
    {
        _boxes.Add(msg.Box);
        if (_boxes.Count == 1)
            RandomizeBoxWanted();
    }

    protected override void Execute(ColoredBoxDisappeared msg)
    {
        _boxes.RemoveAll(x => x == msg.Box);
        if (msg.Box.Color == shipment.Color && msg.Box.Size == shipment.Size && !_boxes.Any(x => x.Color != shipment.Color && x.Size != shipment.Size))
            RandomizeBoxWanted();
    }

    private void RandomizeBoxWanted()
    {
        if (_boxes.Any(x => x.Color != shipment.Color))
        {
            shipment.Color = _boxes.Where(x => x.Color != shipment.Color).Select(x => x.Color).Distinct().TakeRandom(1)[0];
            shipment.Size = _boxes.Where(x => x.Color == shipment.Color).Select(x => x.Size).Distinct().TakeRandom(1)[0];
        }
        else if (_boxes.Any(x => x.Size != shipment.Size))
        {
            shipment.Size = _boxes.Where(x => x.Size == shipment.Size).Select(x => x.Size).Distinct().TakeRandom(1)[0];
        }
        Message.Publish(new ShipmentWantedUpdated());
    }
}
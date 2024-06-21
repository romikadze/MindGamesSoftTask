namespace Source.Scripts.Game.ItemTransporting
{
    public interface IPickupItemStorage : IHighlightItem
    {
        public bool Store(IValuableItem pickupItem);

        public bool Drop(out IValuableItem pickupItem);
    }
}
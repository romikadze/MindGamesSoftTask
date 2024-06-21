namespace Source.Scripts.Transporting
{
    public interface IValuableItem : IPickupItem
    {
        public ItemValue GetItemValue();
    }

    public enum ItemValue
    {
        None = 0,
        Dark = 1,
        Light = 2
    }
}
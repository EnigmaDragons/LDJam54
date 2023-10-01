namespace Messages
{
    public struct BoxDelivered
    {
        public SortingColor color;

        public BoxDelivered(SortingColor color)
        {
            this.color = color;
        }
    }
}
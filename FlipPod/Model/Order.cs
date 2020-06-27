namespace FlipPod.Model
{
    public class Order
    {
        #region Fields

        private int orderId;

        private int customerId;

        private LockerSizeEnum lockerSize;

        #endregion

        #region Constructor

        public Order(int orderId, int customerId, LockerSizeEnum lockerSize)
        {
            this.lockerSize = lockerSize;
            this.orderId = orderId;
            this.customerId = customerId;
        }

        #endregion

        #region Properties

        public int GetOrderId{get{ return this.orderId; }}

        public LockerSizeEnum GetLockerSize{ get{ return this.lockerSize; }}

        #endregion

        #region Methods

        public Order CreateOrder(int orderId, int customerId, LockerSizeEnum lockerSize)
        {
            return new Order(orderId, customerId, lockerSize);
        }

        #endregion
    }
}
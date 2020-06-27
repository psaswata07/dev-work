namespace FlipPod.Model
{
    public  class Lockers
    {
        #region Fields

        private LockerSizeEnum lockerSize;

        private int orderId;

        #endregion

        #region Constructors

        public Lockers(LockerSizeEnum lockerSize)
        {
            this.orderId = -1; //Means this locker is not being used for any customer order. Free for allocation.
            this.lockerSize = lockerSize;
        }

        #endregion

        #region Properties

        public LockerSizeEnum GetLockerSize{ get { return this.lockerSize; }} 

        public int GetOrderId{ get { return this.orderId; }}

        #endregion

        #region Methods

        public void SetOrderId(int orderId) => this.orderId = orderId;

        #endregion

    }
}
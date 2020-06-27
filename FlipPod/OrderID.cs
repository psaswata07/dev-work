namespace FlipPod
{
    public static class OrderID
    {
        #region Fields

        private static int order_id = 1;

        #endregion;

        #region Methods


        public static int GetOrderId()
        {
            var id =  OrderID.order_id;
            OrderID.IncrementOrderId();
            return id;
        }

        private static void IncrementOrderId()
        {
            OrderID.order_id++;
        }


        #endregion
    }
}
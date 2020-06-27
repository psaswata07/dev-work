using System.Collections.Generic;

namespace FlipPod.Model
{
    public class Customer
    {
        #region Fields

        private int customerId;

        private List<int> orderIDs;

        #endregion

        #region Constructors

        public Customer(int customerId)
        {
            this.customerId = customerId;
            this.orderIDs = new List<int>();
        }

        #endregion

        #region Properties

        public int GetCurrentNumberOfOrdersForCustomer{ get { return this.orderIDs.Count; }}

        public int GetCustomerId{get { return this.customerId; }}

        public List<int> GetOrdersForCustomer{get { return this.orderIDs; }}

        #endregion

        #region Methods

        public void AddCurrentOrderToCustomerOrderIDs(int orderIDs)
        {
            this.orderIDs.Add(orderIDs);
        }

        public void SetCurrentOrderIDsListToCustomer(List<int> orderIDs)
        {
            this.orderIDs = orderIDs;
        }

        #endregion;

    }

}
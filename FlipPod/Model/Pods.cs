using System.Collections.Generic;
namespace FlipPod.Model
{
    public class Pods
    {

        #region Constants

        private static readonly int numberOfSmallLockers = 1;
        private static readonly int numberfOfLargeLockers = 2;

        #endregion

        #region Fields

        private int podId;

        private List<Lockers> lockerList;

        #endregion

        #region Constructor

        public Pods(int podId)
        {
            this.podId = podId;
            this.lockerList = new List<Lockers>();

            for(int i = 0; i<numberfOfLargeLockers; i++)
            {
                Lockers locker = new Lockers(LockerSizeEnum.large);
                this.lockerList.Add(locker);
            }

            for(int i = 0; i<numberOfSmallLockers; i++)
            {
                Lockers locker = new Lockers(LockerSizeEnum.large);
                this.lockerList.Add(locker);
            }
        }

        #endregion

        #region Properties

        public int GetPodId{get { return this.podId; }}

        public List<Lockers> GetLockerList{get {return this.lockerList; }}

        #endregion

        #region Methods

        public bool IsPodFull()
        {
            foreach(var locker in this.lockerList)
            {
                if(locker.GetOrderId == -1)
                return false;
            }

            return true;
        }

        public int GetNumberOfUnassignedSmallPods()
        {
            int count = 0;
            foreach(var locker in this.lockerList)
            {
                if(locker.GetOrderId == -1 && locker.GetLockerSize == LockerSizeEnum.small)
                count++;
            }

            return count;
        }

        public int GetNumberOfUnassignedLargePods()
        {
            int count = 0;
            foreach(var locker in this.lockerList)
            {
                if(locker.GetOrderId == -1 && locker.GetLockerSize == LockerSizeEnum.large)
                count++;
            }

            return count;
        }

        public bool AssignOrderToPod(int orderId, LockerSizeEnum orderSize)
        {           
            foreach(var locker in this.lockerList)
            {
                if(locker.GetOrderId == -1 && locker.GetLockerSize == orderSize)
                {
                    locker.SetOrderId(orderId);
                    return true;
                }
            }

            return false;
        }

        public void UpdatePodOnCustomerOrderCollection(int orderId)
        {
            foreach(var locker in this.lockerList)
            {
                if(locker.GetOrderId == orderId)
                {
                    locker.SetOrderId(-1);
                }
            }
        }

        #endregion
    }
}
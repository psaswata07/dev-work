using System;
using System.Linq;
using System.Xml.Linq;
using FlipPod.Model;
using System.Collections.Generic;

namespace FlipPod.Services
{
    public static class OrderProcessor
    {
        #region Fields

        private static List<Pods> pods;

        private static List<Customer> customers;

        private static Queue<Order> ordersQueue;

        private static Dictionary<Order, bool> orderStatus; // Checks if a order has been allocated a locker.

        #endregion

        #region Methods

        public static void SetupEnvironment(int numberOfPods, int numberOfCustomers)
        {
            OrderProcessor.pods = new List<Pods>();
            for(int i = 0; i <numberOfPods; i++)
            {
                var pod = new Pods(i);
                OrderProcessor.pods.Add(pod);
            }

            OrderProcessor.customers = new List<Customer>();
            for(int i = 0; i<numberOfCustomers; i++)
            {
                var customer = new Customer(i);
                OrderProcessor.customers.Add(customer);                
            }
        }

        public static void AddOrderToQueue(int orderId, int customerId, LockerSizeEnum lockerSize)
        {
            if(OrderProcessor.ordersQueue == null)
            {
                OrderProcessor.ordersQueue = new Queue<Order>();
            }

            var newOrder = OrderProcessor.CreateOrder(orderId, customerId, lockerSize);

            if(OrderProcessor.TryAllocateLockerToOrder(newOrder))
            {
                if(OrderProcessor.orderStatus == null)
                {
                    OrderProcessor.orderStatus = new Dictionary<Order, bool>();
                }

                OrderProcessor.orderStatus[newOrder] = true;
            }
            else OrderProcessor.ordersQueue.Enqueue(newOrder);


            OrderProcessor.customers[customerId].AddCurrentOrderToCustomerOrderIDs(orderId);            
        }

        private static bool TryAllocateLockerToOrder(Order order)
        {
            OrderProcessor.TryAllocatingForPendingOrders();

            var allocationStatus = false;
            foreach(var pod in OrderProcessor.pods)
            {
                if(!pod.IsPodFull())
                {
                    allocationStatus = pod.AssignOrderToPod(order.GetOrderId, order.GetLockerSize);
                }
                if(allocationStatus == true)
                {
                    OrderProcessor.PrintPodStatusOnAllocation(pod.GetPodId);
                    return allocationStatus;
                }
            }

            if(order.GetLockerSize == LockerSizeEnum.small)
            {
                bool largeSizeOrderPendingInQueue = false;

                foreach(var o in OrderProcessor.ordersQueue)
                {
                    if(o.GetLockerSize == LockerSizeEnum.large || o.GetLockerSize == LockerSizeEnum.small)
                    {
                        largeSizeOrderPendingInQueue = true;
                        break;
                    }
                }

                if(!largeSizeOrderPendingInQueue)
                {
                    foreach(var pod in OrderProcessor.pods)
                    {
                        if(!pod.IsPodFull())
                        {
                            allocationStatus = pod.AssignOrderToPod(order.GetOrderId, LockerSizeEnum.large);                            
                        }
                        if(allocationStatus == true)
                        {
                            OrderProcessor.PrintPodStatusOnAllocation(pod.GetPodId);
                            return allocationStatus;
                        }
                    }
                }
            }

            return allocationStatus;
        }

        public static void ClearOrdersOnCustomerCollection(int customerId)
        {
            var customer = OrderProcessor.customers[customerId];
            if(customer.GetCurrentNumberOfOrdersForCustomer == 0)
            return;

            var orders1 = customer.GetOrdersForCustomer;
            List<int> ordersTemp = new List<int>();

            List<Order> keysToRemove = new List<Order>();


            foreach(var order in orders1)
            {
                foreach(var processedOrder in OrderProcessor.orderStatus)
                {
                    if(processedOrder.Key.GetOrderId == order)
                    {
                        foreach(var pod in OrderProcessor.pods)
                        {
                            foreach(var locker in pod.GetLockerList)
                            {
                                if(locker.GetOrderId == order)
                                {
                                    locker.SetOrderId(-1);
                                }
                            }
                        }

                        keysToRemove.Add(processedOrder.Key);
                        ordersTemp.Add(order);
                    }
                }
            }

            foreach(var key in keysToRemove)
            OrderProcessor.orderStatus.Remove(key);

            foreach(var order in ordersTemp)
            orders1.Remove(order);

            customer.SetCurrentOrderIDsListToCustomer(orders1);

        }

        private static void TryAllocatingForPendingOrders()
        {
            if(OrderProcessor.ordersQueue.Count == 0)
            return;

            if(OrderProcessor.ordersQueue.Count > 20) // If more than 20 pending orders create new pod;
            {
                var pod = new Pods(OrderProcessor.pods.Count);
                OrderProcessor.pods.Add(pod);
            }

            List<Order> orderList1 = OrderProcessor.ordersQueue.ToList();
            List<Order> orderList2 = new List<Order>();

            foreach(var order in orderList1)
            {
                var allocationStatus = false;
                foreach(var pod in OrderProcessor.pods)
                {
                    if(!pod.IsPodFull())
                    {
                        allocationStatus = pod.AssignOrderToPod(order.GetOrderId, order.GetLockerSize);
                    }
                    if(allocationStatus == true)
                    {
                        OrderProcessor.PrintPodStatusOnAllocation(pod.GetPodId);
                        OrderProcessor.orderStatus[order] = true;
                        orderList2.Add(order);
                        break;
                    }
                }
            }

            OrderProcessor.ordersQueue.Clear();


            foreach(var order in orderList2)
            orderList1.Remove(order);

            foreach(var order in orderList1)
            OrderProcessor.ordersQueue.Enqueue(order);
        }

        private static Order CreateOrder(int orderId, int customerId, LockerSizeEnum lockerSize)
        {
            return new Order(orderId, customerId, lockerSize);
        }

        private static void PrintPodStatusOnAllocation(int podId)
        {
            Console.WriteLine("PodId -> {0} : Free Small Lockers -> {1} : Free Large Lockers -> {2} ", OrderProcessor.pods[podId].GetPodId, OrderProcessor.pods[podId].GetNumberOfUnassignedSmallPods(), OrderProcessor.pods[podId].GetNumberOfUnassignedLargePods());
            foreach(var pod in OrderProcessor.pods)
            {
                foreach(var locker in pod.GetLockerList)
                {
                    Console.WriteLine(locker.GetOrderId.ToString());
                }
            }

            Console.WriteLine("Queue Status");
            foreach(var order in OrderProcessor.ordersQueue)
            {
                Console.WriteLine(order.GetOrderId.ToString());

            }
        }

        #endregion
    }
}
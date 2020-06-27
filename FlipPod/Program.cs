using System;
using System.Collections.Generic;
using FlipPod.Model;
using FlipPod.Services;

namespace FlipPod
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World From FlipPod!");

            OrderProcessor.SetupEnvironment(2, 25);

            OrderProcessor.AddOrderToQueue(OrderID.GetOrderId(), 1, LockerSizeEnum.small);
            OrderProcessor.AddOrderToQueue(OrderID.GetOrderId(), 5, LockerSizeEnum.small);
            OrderProcessor.AddOrderToQueue(OrderID.GetOrderId(), 2, LockerSizeEnum.small);
            // OrderProcessor.AddOrderToQueue(OrderID.GetOrderId(), 10, LockerSizeEnum.large);
            // OrderProcessor.AddOrderToQueue(OrderID.GetOrderId(), 2, LockerSizeEnum.small);
            // OrderProcessor.AddOrderToQueue(OrderID.GetOrderId(), 5, LockerSizeEnum.small);
            // OrderProcessor.AddOrderToQueue(OrderID.GetOrderId(), 7, LockerSizeEnum.large);
            // OrderProcessor.AddOrderToQueue(OrderID.GetOrderId(), 8, LockerSizeEnum.small);
            // OrderProcessor.AddOrderToQueue(OrderID.GetOrderId(), 9, LockerSizeEnum.small);
            // OrderProcessor.AddOrderToQueue(OrderID.GetOrderId(), 10, LockerSizeEnum.large);
            // OrderProcessor.AddOrderToQueue(OrderID.GetOrderId(), 20, LockerSizeEnum.small);
            // OrderProcessor.AddOrderToQueue(OrderID.GetOrderId(), 22, LockerSizeEnum.large);
            // OrderProcessor.AddOrderToQueue(OrderID.GetOrderId(), 23, LockerSizeEnum.small);

            // OrderProcessor.ClearOrdersOnCustomerCollection(6);
            // OrderProcessor.ClearOrdersOnCustomerCollection(24);
            // OrderProcessor.ClearOrdersOnCustomerCollection(3);
            // OrderProcessor.ClearOrdersOnCustomerCollection(5);
            // OrderProcessor.ClearOrdersOnCustomerCollection(7);

        }
    }
}

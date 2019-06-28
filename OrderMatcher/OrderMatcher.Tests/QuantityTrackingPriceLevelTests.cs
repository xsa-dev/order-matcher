﻿using System;
using Xunit;

namespace OrderMatcher.Tests
{
    public class QuantityTrackingPriceLevelTests
    {
        [Fact]
        public void AddOrder_AddsOrder()
        {
            QuantityTrackingPriceLevel quantityTrackingPriceLevel = new QuantityTrackingPriceLevel(100);
            Order order1 = new Order() { IsBuy = true, OrderId = 1, Price = 100, Sequnce = 1, OpenQuantity = 1000, Quantity = 1000, StopPrice = 0 };
            quantityTrackingPriceLevel.AddOrder(order1);

            Assert.Equal((Quantity)1000, quantityTrackingPriceLevel.Quantity);
            Assert.Equal(1, quantityTrackingPriceLevel.OrderCount);

            Order order2 = new Order() { IsBuy = true, OrderId = 2, Price = 100, Sequnce = 2, OpenQuantity = 1000, Quantity = 1000, StopPrice = 0 };
            quantityTrackingPriceLevel.AddOrder(order2);

            Assert.Equal((Quantity)2000, quantityTrackingPriceLevel.Quantity);
            Assert.Equal(2, quantityTrackingPriceLevel.OrderCount);
            Assert.Contains(order1, quantityTrackingPriceLevel);
            Assert.Contains(order2, quantityTrackingPriceLevel);
        }

        [Fact]
        public void RemoveOrder_RemovesOrder()
        {
            QuantityTrackingPriceLevel quantityTrackingPriceLevel = new QuantityTrackingPriceLevel(100);
            Order order1 = new Order() { IsBuy = true, OrderId = 1, Price = 100, Sequnce = 1, OpenQuantity = 1000, Quantity = 1000, StopPrice = 0 };
            quantityTrackingPriceLevel.AddOrder(order1);

            Assert.Equal((Quantity)1000, quantityTrackingPriceLevel.Quantity);
            Assert.Equal(1, quantityTrackingPriceLevel.OrderCount);

            Order order2 = new Order() { IsBuy = true, OrderId = 2, Price = 100, Sequnce = 2, OpenQuantity = 1000, Quantity = 1000, StopPrice = 0 };
            quantityTrackingPriceLevel.AddOrder(order2);

            Assert.Equal((Quantity)2000, quantityTrackingPriceLevel.Quantity);
            Assert.Equal(2, quantityTrackingPriceLevel.OrderCount);

            Order order3 = new Order() { IsBuy = true, OrderId = 3, Price = 100, Sequnce = 3, OpenQuantity = 1000, Quantity = 1000, StopPrice = 0 };
            quantityTrackingPriceLevel.AddOrder(order3);

            Assert.Equal((Quantity)3000, quantityTrackingPriceLevel.Quantity);
            Assert.Equal(3, quantityTrackingPriceLevel.OrderCount);

            Order order4 = new Order() { IsBuy = true, OrderId = 4, Price = 100, Sequnce = 4, OpenQuantity = 1000, Quantity = 1000, StopPrice = 0 };
            quantityTrackingPriceLevel.AddOrder(order4);

            Assert.Equal((Quantity)4000, quantityTrackingPriceLevel.Quantity);
            Assert.Equal(4, quantityTrackingPriceLevel.OrderCount);

            quantityTrackingPriceLevel.RemoveOrder(order3);

            Assert.Equal((Quantity)3000, quantityTrackingPriceLevel.Quantity);
            Assert.Equal(3, quantityTrackingPriceLevel.OrderCount);
            Assert.Contains(order1, quantityTrackingPriceLevel);
            Assert.Contains(order2, quantityTrackingPriceLevel);
            Assert.Contains(order4, quantityTrackingPriceLevel);
        }

        [Fact]
        public void Fill_RemovesOrder_IfOpenQuantityIs0()
        {
            QuantityTrackingPriceLevel quantityTrackingPriceLevel = new QuantityTrackingPriceLevel(100);
            Order order1 = new Order() { IsBuy = true, OrderId = 1, Price = 100, OpenQuantity = 1000, Sequnce = 1, Quantity = 1000, StopPrice = 0 };
            quantityTrackingPriceLevel.AddOrder(order1);

            Assert.Equal((Quantity)1000, quantityTrackingPriceLevel.Quantity);
            Assert.Equal(1, quantityTrackingPriceLevel.OrderCount);

            Order order2 = new Order() { IsBuy = true, OrderId = 2, Price = 100, OpenQuantity = 1000, Sequnce = 2, Quantity = 1000, StopPrice = 0 };
            quantityTrackingPriceLevel.AddOrder(order2);

            Assert.Equal((Quantity)2000, quantityTrackingPriceLevel.Quantity);
            Assert.Equal(2, quantityTrackingPriceLevel.OrderCount);

            quantityTrackingPriceLevel.Fill(order1, 1000);

            Assert.Equal((Quantity)1000, quantityTrackingPriceLevel.Quantity);
            Assert.Equal(1, quantityTrackingPriceLevel.OrderCount);
            Assert.Contains(order2, quantityTrackingPriceLevel);
        }

        [Fact]
        public void Fill_DoesNotRemoveOrder_IfOpenQuantityIsNot0()
        {
            QuantityTrackingPriceLevel quantityTrackingPriceLevel = new QuantityTrackingPriceLevel(100);
            Order order1 = new Order() { IsBuy = true, OrderId = 1, OpenQuantity = 1000, Price = 100, Sequnce = 1, Quantity = 1000, StopPrice = 0 };
            quantityTrackingPriceLevel.AddOrder(order1);

            Assert.Equal((Quantity)1000, quantityTrackingPriceLevel.Quantity);
            Assert.Equal(1, quantityTrackingPriceLevel.OrderCount);

            Order order2 = new Order() { IsBuy = true, OrderId = 2, OpenQuantity = 1000, Price = 100, Sequnce = 2, Quantity = 1000, StopPrice = 0 };
            quantityTrackingPriceLevel.AddOrder(order2);

            Assert.Equal((Quantity)2000, quantityTrackingPriceLevel.Quantity);
            Assert.Equal(2, quantityTrackingPriceLevel.OrderCount);

            quantityTrackingPriceLevel.Fill(order1, 900);

            Assert.Equal((Quantity)1100, quantityTrackingPriceLevel.Quantity);
            Assert.Equal(2, quantityTrackingPriceLevel.OrderCount);
            Assert.Contains(order1, quantityTrackingPriceLevel);
            Assert.Contains(order2, quantityTrackingPriceLevel);
            Assert.True(100 == order1.OpenQuantity, "Quantity should be 100");
        }

        [Fact]
        public void Fill_ThrowsException_IfOpenQuantityIsLessThanFillQuantity()
        {
            QuantityTrackingPriceLevel quantityTrackingPriceLevel = new QuantityTrackingPriceLevel(100);
            Order order1 = new Order() { IsBuy = true, OrderId = 1, Price = 100, Sequnce = 1, OpenQuantity = 1000, Quantity = 1000, StopPrice = 0 };
            quantityTrackingPriceLevel.AddOrder(order1);

            Assert.Equal((Quantity)1000, quantityTrackingPriceLevel.Quantity);
            Assert.Equal(1, quantityTrackingPriceLevel.OrderCount);

            Order order2 = new Order() { IsBuy = true, OrderId = 2, Price = 100, Sequnce = 2, OpenQuantity = 1000, Quantity = 1000, StopPrice = 0 };
            quantityTrackingPriceLevel.AddOrder(order2);

            Assert.Equal((Quantity)2000, quantityTrackingPriceLevel.Quantity);
            Assert.Equal(2, quantityTrackingPriceLevel.OrderCount);

            Exception ex = Assert.Throws<Exception>(() => quantityTrackingPriceLevel.Fill(order1, 1100));
            Assert.Equal("Order quantity is less then requested fill quanity", ex.Message);
            Assert.Equal((Quantity)2000, quantityTrackingPriceLevel.Quantity);
            Assert.Equal(2, quantityTrackingPriceLevel.OrderCount);
        }
    }
}

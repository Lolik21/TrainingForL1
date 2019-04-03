// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Linq;
using SampleSupport;
using Task.Data;

// Version Mad01

namespace SampleQueries
{
	[Title("LINQ Module")]
	[Prefix("Linq")]
	public class LinqSamples : SampleHarness
	{

		private DataSource dataSource = new DataSource();

		[Category("Training Operators")]
		[Title("Linq1")]
		[Description("List all customers whose total turnover (the sum of all orders) exceeds a certain X value. Demonstrate the execution of a query with different X (consider whether you can do without copying the query several times)")]
		public void Linq1()
        {
            const decimal totalX = 1000;
            const decimal totalY = 10000;
            const decimal totalZ = 30000;
            Console.WriteLine($@"Customers orders total turnover > x({totalX})");
            var result = dataSource.Customers.Where(customer => customer.Orders.Sum(order => order.Total) > totalX)
                .Select(customer =>
                    new {Id = customer.CustomerID, TurnOver = customer.Orders.Sum(order => order.Total)});
            ObjectDumper.Write(result);
            Console.WriteLine($@"Customers orders total turnover > y({totalY})");
            result = result.Where(arg => arg.TurnOver > totalY);
            ObjectDumper.Write(result);
            Console.WriteLine($@"Customers orders total turnover > z({totalZ})");
            result = result.Where(arg => arg.TurnOver > totalZ);
            ObjectDumper.Write(result);
        }

        [Category("Training Operators")]
        [Title("Linq2")]
		[Description("For each customer, make a list of suppliers located in the same country and in the same city. Do tasks using grouping and without.")]
		public void Linq2()
		{
            Console.WriteLine(@"With grouping :");
            var join = dataSource.Suppliers.Join(dataSource.Customers,
                supplier => new {supplier.City, supplier.Country},
                customer => new {customer.City, customer.Country}, (supplier, customer) => new {supplier, customer})
                .GroupBy(arg => arg.supplier);

            foreach (var group in join)
            {
                Console.WriteLine(group.Key.SupplierName);
                foreach (var customer in group)
                {
                    Console.Write(@"  ");
                    ObjectDumper.Write(customer.customer);
                }
            }

            Console.WriteLine(@"Without grouping :");
            var result2 = dataSource.Customers.Select(customer => new
            {
                CusotmerId = customer.CustomerID,
                Suppliers = string.Join("|",
                    dataSource.Suppliers
                        .Where(supplier => supplier.City == customer.City && supplier.Country == customer.Country)
                        .Select(supplier => supplier.SupplierName))
            });

            ObjectDumper.Write(result2);
        }

        [Category("Training Operators")]
        [Title("Linq3")]
        [Description("Find all customers who have orders that exceed the sum of X")]
        public void Linq3()
        {
            const decimal totalX = 10000;
            const decimal totalY = 15000;
            Console.WriteLine($@"Clients and orders > x({totalX})");
            var result = dataSource.Customers.Where(customer => customer.Orders.Any(order => order.Total > totalX));
            ObjectDumper.Write(result);
            Console.WriteLine($@"Clients and orders > y({totalY})");
            result = result.Where(customer => customer.Orders.Any(order => order.Total > totalY));
            ObjectDumper.Write(result);
        }

        [Category("Training Operators")]
        [Title("Linq4")]
        [Description("Issue a list of customers, indicating from which month of what year they became customers (to accept the month and year of the first order as such)")]
        public void Linq4()
        {
            var result = dataSource.Customers.Where(customer => customer.Orders.Length > 0).Select(customer => new
            {
                CustomerId = customer.CustomerID,
                Order = customer.Orders.Min(order => order.OrderDate)
            });
            ObjectDumper.Write(result);
        }

        [Category("Training Operators")]
        [Title("Linq5")]
        [Description("Do the previous task, but give the list sorted by year, month, customer turnover (from maximum to minimum) and customer name")]
        public void Linq5()
        {
            var result = dataSource.Customers.Where(customer => customer.Orders.Length > 0).Select(customer => new
            {
                CustomerId = customer.CustomerID,
                Order = customer.Orders.Min(order => order.OrderDate),
                TurnOver = customer.Orders.Sum(order => order.Total)
            }).OrderBy(arg => arg.Order.Date).ThenBy(arg => arg.TurnOver).ThenBy(arg => arg.CustomerId);
            ObjectDumper.Write(result);
        }

        [Category("Training Operators")]
        [Title("Linq6")]
        [Description("Indicate all customers who have a non-numeric postal code or a region is not filled or the operator code is not specified in the phone (for simplicity, we assume that this is equivalent to “no round brackets at the beginning”).")]
        public void Linq6()
        {
            var invalidCustomers = dataSource.Customers.Where(customer => !int.TryParse(customer.PostalCode, out _) || 
                                                                          string.IsNullOrWhiteSpace(customer.Region) || 
                                                                          !customer.Phone.StartsWith("("));
            ObjectDumper.Write(invalidCustomers);
        }

        [Category("Training Operators")]
        [Title("Linq7")]
        [Description("Group all products into categories, inside - by stock availability, within the last group, sort by cost")]
        public void Linq7()
        {
            var result = dataSource.Products
                .GroupBy(product => new {product.Category, IsOnWarehouser = product.UnitsInStock > 0})
                .GroupBy(products => products.Key.Category).ToArray();
            foreach (var group in result)
            {
                Console.WriteLine($@"{group.Key}");
                foreach (var innerGroup in group)
                {
                    Console.WriteLine($@"  {innerGroup.Key.IsOnWarehouser}");
                    foreach (var product in innerGroup)
                    {
                        Console.Write(@"    ");
                        ObjectDumper.Write(product);
                    }
                }
            }
        }

        [Category("Training Operators")]
        [Title("Linq8")]
        [Description("Group products into groups of \"cheap\", \"average price\", \"expensive\". Define the boundaries of each group")]
        public void Linq8()
        {
            var result = dataSource.Products.Select(product =>
            {
                string range;
                if (product.UnitPrice > 100)
                {
                    range = "BIG > 100";
                }
                else if (product.UnitPrice > 30)
                {
                    range = "AVERAGE > 30";
                }
                else
                {
                    range = "SMALL";
                }

                return new {product, Range = range};
            }).GroupBy(arg => arg.Range);

            foreach (var grouping in result)
            {
                Console.WriteLine(grouping.Key);
                foreach (var product in grouping)
                {
                    Console.Write(@"  ");
                    ObjectDumper.Write(product.product);
                }
            }
        }

        [Category("Training Operators")]
        [Title("Linq9")]
        [Description("Calculate the average profitability of each city (average order amount for all customers from this city) and average intensity (average number of orders per customer from each city)")]
        public void Linq9()
        {
            var result = dataSource.Customers.GroupBy(customer => customer.City).Select(grouping =>
                new
                {
                    City = grouping.Key,
                    EverageOrder = Math.Round(grouping.SelectMany(customer => customer.Orders).Average(order => order.Total), 2),
                    EverageIntersity = grouping.Average(customer => customer.Orders.Length)
                }
            );
            ObjectDumper.Write(result);
        }

        [Category("Training Operators")]
        [Title("Linq10")]
        [Description("Make the average annual activity statistics of customers by months (excluding the year), statistics by year, by year and month (that is, when one month in different years has its own value).")]
        public void Linq10()
        {
            var result = dataSource.Customers.Select(customer => new
            {
                customer.CustomerID,
                MonthStat = customer.Orders.GroupBy(order => order.OrderDate.Month)
                    .Select(orders => new {orders.Key, Count = orders.Count()}),
                YearStat = customer.Orders.GroupBy(order => order.OrderDate.Year)
                    .Select(orders => new {orders.Key, Count = orders.Count()}),
                MonthYearStat = customer.Orders.GroupBy(order => new {order.OrderDate.Year, order.OrderDate.Month})
                    .Select(orders => new {orders.Key, Count = orders.Count()}),
            });

            foreach (var stat in result)
            {
                Console.WriteLine(stat.CustomerID);
                Console.WriteLine(@"Month:");
                foreach (var monthStat in stat.MonthStat)
                {
                    ObjectDumper.Write(monthStat);
                }
                Console.WriteLine(@"Year:");
                foreach (var yearStat in stat.YearStat)
                {
                    ObjectDumper.Write(yearStat);
                }
                Console.WriteLine(@"MonthYear:");
                foreach (var yearMonthStat in stat.MonthYearStat)
                {
                    ObjectDumper.Write(yearMonthStat.Key);
                    Console.WriteLine($@"Count: {yearMonthStat.Count}");
                }
            }
        }
    }
}

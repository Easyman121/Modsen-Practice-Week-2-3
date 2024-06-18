﻿#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace DataAccessLayer.Models;

public class OrderItems
{
    public int Id { get; set; }
    public Orders OrderId { get; set; }
    public Products ProductId { get; set; }
    public int Count { get; set; }
}
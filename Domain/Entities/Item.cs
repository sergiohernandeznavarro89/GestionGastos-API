﻿namespace Domain.Entities;

public class Item
{
    public int ItemId { get; set; }
    public string ItemName { get; set; }
    public string ItemDesc { get; set; }
    public decimal Ammount { get; set; }
    public int? Periodity { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool Cancelled { get; set; }
    public int CategoryId { get; set; }
    public int? SubCategoryId { get; set; }
    public int ItemTypeId { get; set; }
    public int AmmountTypeId { get; set; }
    public int PeriodTypeId { get; set; }
    public int UserId { get; set; }
    public int AccountId { get; set; }
}

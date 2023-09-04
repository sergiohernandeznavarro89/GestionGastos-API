namespace Application.Dto;

public class NextMonthPendingPayItemResponse
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
    public string AccountName { get; set; }
    public string CategoryDesc { get; set; }
    public string SubCategoryDesc { get; set; }
    public string ItemTypeDesc { get; set; }
    public string AmmountTypeDesc { get; set; }
    public string PeriodTypeDesc { get; set; }
}

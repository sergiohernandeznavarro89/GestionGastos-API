using System;

namespace Application.Dto.Transfer;

public class PendingTransferResponse
{
    public int TransferId { get; set; }
    public int CategoryId { get; set; }
    public int? SubCategoryId { get; set; }
    public int PeriodTypeId { get; set; }
    public int UserId { get; set; }
    public int OriginAccountId { get; set; }
    public int DestinationAccountId { get; set; }
    public string TransferName { get; set; }
    public string TransferDesc { get; set; }
    public decimal Ammount { get; set; }
    public int? Periodity { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool Cancelled { get; set; }
    
    public string OriginAccountName { get; set; }
    public string DestinationAccountName { get; set; }
    public string CategoryDesc { get; set; }
    public string SubCategoryDesc { get; set; }
    public string PeriodTypeDesc { get; set; }
}

namespace Domain.Entities;

public class TransferSummary : Transfer
{
    public string OriginAccountName { get; set; }
    public string DestinationAccountName { get; set; }
    public string CategoryDesc { get; set; }
    public string SubCategoryDesc { get; set; }
    public string PeriodTypeDesc { get; set; }
}

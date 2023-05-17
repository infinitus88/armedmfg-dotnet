namespace ArmedMFG.PublicApi.Modules.Orders.Dtos.SharedDtos;

public class OrderForEditDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string RequiredDate { get; set; }
    public string OrderedDate { get; set; }
    public string FinishDate { get; set; }
    public byte Status { get; set; }
    public string Description { get; set; }
}

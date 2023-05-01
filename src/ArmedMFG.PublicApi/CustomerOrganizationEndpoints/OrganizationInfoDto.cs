namespace ArmedMFG.PublicApi.CustomerOrganizationEndpoints;

public class OrganizationInfoDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string TaxpayerIdNum { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string MainBranchAddress { get; set; }
    public string? Description { get; set; }
}

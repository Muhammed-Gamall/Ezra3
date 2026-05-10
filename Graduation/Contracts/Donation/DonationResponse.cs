namespace Graduation.Contracts.Donation
{
    public record DonationResponse
      (
        string Name,
        string treeType,
        double Amount,
        string Street,
        string District,
        string City,
        string Governorate

        );
}

namespace Graduation.Contracts.Donation
{
    public record DonationRequest
    (
        string treeType,
        double Amount,
        string Street,
        string District,
        string City,
        string Governorate

        );
}

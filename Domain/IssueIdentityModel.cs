namespace PersonalTreker.Domain
{
    public class IssueIdentityModel : IIssueIdentity
    {
        public int Id { get; }

        public IssueIdentityModel(int id)
        {
            Id = id;
        }
    }
}
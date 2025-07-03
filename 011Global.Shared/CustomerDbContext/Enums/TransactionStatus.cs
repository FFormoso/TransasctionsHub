namespace _011Global.Shared.CustomerDbContext.Enums;

public enum TransactionStatus : byte
{
    Approved = 1,
    PartiallyApproved = 2,
    Declined = 3,
    Error = 4,
    VerificationRequired = 5
}
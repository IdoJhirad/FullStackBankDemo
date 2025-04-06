

namespace Model.Enums
{
    /// <summary>
    /// TransactionType
    /// </summary>
    public enum TransactionType
    {
        /// <summary>
        /// Deposit
        /// </summary>
        Deposit = 0,
        /// <summary>
        /// Withdrawal
        /// </summary>
        Withdrawal = 1,

    }
    /// <summary>
    /// TransactionStatus
    /// </summary>
    public enum TransactionStatus
    {
        /// <summary>
        /// Pending
        /// </summary>
        Pending = 0,
        /// <summary>
        /// Completed
        /// </summary>
        Completed = 1,
        /// <summary>
        /// Failed
        /// </summary>
        Failed = 2
    }
}

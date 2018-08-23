namespace iHoaDon.Business
{
    /// <summary>
    /// 
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// 
        /// </summary>
        public const string Supplementary = "_KHBS01",
                            Common = "_common";

        /// <summary>
        /// Various password settings
        /// NOTE: DONOT change these values, ever
        /// </summary>
        public const int PasswordDerivationIteration = 1000,
                         PasswordBytesLength = 64,
                         MinPasswordLength = 8,
                         PasswordSaltLength = 16,
                         ActivationCodeLength = 32;
        /// <summary>
        /// 
        /// </summary>
        public enum StatusSendEmail
        {
            /// <summary>
            /// gửi từ email
            /// </summary>
            SendToMailServer = 1,
            /// <summary>
            /// gửi từ email
            /// </summary>
            SendMailServerTo = 2,
            /// <summary>
            /// gửi thành công
            /// </summary>
            SendSuccess = 3
        }
        /// <summary>
        /// 
        /// </summary>
        public enum StatusPhone
        {
            /// <summary>
            /// insert db
            /// </summary>
            SendToDb=1,
            /// <summary>
            /// gửi tới bsg
            /// </summary>
            SendToBsg = 2,
            /// <summary>
            /// gửi thành công
            /// </summary>
            SendSuccess = 3
        }
    }

}
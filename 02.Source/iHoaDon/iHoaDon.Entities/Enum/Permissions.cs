using System;

namespace iHoaDon.Entities
{
    ///<summary>
    /// All permissions
    ///</summary>
    [Flags]
    public enum Permissions:long
    {
#pragma warning disable 1591
        None               = 0x00,
        Forms              = 0x01,//all others
        Submit             = 0x02,//sign and submit
        ManageSubAccounts  = 0x04,
        ManageSubProfiles  = 0x08,

        BusinessSubAccount = 0x10,//marker only
        ServiceSubAccount  = 0x20,//marker only

        ManageForms        = 0x100,
        ManageHelp         = 0x200,
        ManageTaxes        = 0x400,
        ManagePlans        = 0x800,

        ManageTransactions = 0x1000,
        ManageAccounts     = 0x2000,
        ManageLogs         = 0x4000,//no logging yet
        Diag               = 0x8000,

        Individual   = Forms | Submit,
        Business     = Individual   | ManageSubAccounts,
        BusinessUser = Individual   | BusinessSubAccount,
        Service      = Business     | ManageSubProfiles,
        ServiceUser  = Individual   | ServiceSubAccount,
        
        OperationAdmin    = ManageHelp   | ManageForms | ManageTaxes | ManagePlans,
        SupportAdmin      = ManageHelp   | ManageTransactions,
        RegistrationAdmin = ManageHelp   | ManageAccounts,
        Overlord          = OperationAdmin    | SupportAdmin | RegistrationAdmin | Diag
#pragma warning restore 1591
    }
}
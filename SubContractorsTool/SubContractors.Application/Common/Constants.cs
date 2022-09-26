namespace SubContractors.Application.Common
{
    public static class Constants
    {
        public static class ValidationErrors
        {
            public const string Field_Is_Required = "Field is required";
            public const string Identifier_Min_Value = "Min. value of identifier must be greater than or equal to 1";
            public const string Identifier_Max_Value =
                "Max. value of identifier must be less than or equal to int.MaxValue";
            public const string SubContractor_QueryType_Value_Range = "Value must be in range of 0 - 1";
            public const string SubContractor_Type_Value_Range = "Value must be in range of 1-7";
            public const string SubContractor_Status_Value_Range = "Value must be in range of 1-4";
            public const string Email_Required = "Input text is not valid email format";
            public const string Project_Status_Value_Range = "Value must be in range of 1-2";
            public const string Invoice_Status_Value_Range = "Value must be in range of 1-4";
            public const string Identifier_Invoice_Status_Approve_Value = "Value must be equal to 3";
            public const string Invoice_PaymentNumber_Value_Range = "Value for PaymentNumber must be in range of 1-2 ";
            public const string Invoice_File_Content = "Value must be containe files";
            public const string Invoice_ReferralId_SubcontractorType_Is_Not_HR = "Value for ReferralId must be equal to null, when SubContractorType isn't equal to HR Partner";
            public const string Invoice_ReferralId_Value_SubcontractorType_HR = "Value must not be equal to null, when SubContractorType is equal to HR Partner";
            public const string Invoice_PaymentNumber_Value_Null = "Value must be is equal to null, when SubContractorType isn't equal to HR Partner";
            public const string Invoice_SubContractorType_Value = "Value must be equal to 6";
            public const string Check_ParentType_Value_Range = "Value must be in range of 1-2";
            public const string Compliance_Type_Value_Range = "Value must be in range of 1-4";
            public const string StartDate_less_than_EndDate = "StartDate must be equal or less than EndDate";
            public const string EndDate_more_than_StartDate = "EndDate must be equal or more than StartDate";
            public const string FromDate_less_than_ToDate = "FromDate must be equal or less than ToDate";
            public const string Check_Status_Value_Range = "Value must be in range of 1-2";
            public const string File_Length = "File is empty";
            
        }
    }
}
package com.dataart.subcontractorstool.apitests.utils.payloads;

public class AgreementPayloads {
    public static String createAgreement(String title, int subContractorId, int legalEntityId, String startDate, String endDate, int budgetLocationId, String condition, int paymentMethodId, String agreementUrl) {
        return "{\n" +
                "  \"title\": \"" + title + "\",\n" +
                "  \"subContractorId\": " + subContractorId + ",\n" +
                "  \"legalEntityId\": " + legalEntityId + ",\n" +
                "  \"startDate\": \"" + startDate + "\",\n" +
                "  \"endDate\": \"" + endDate + "\",\n" +
                "  \"budgetLocationId\": " + budgetLocationId + ",\n" +
                "  \"conditions\": \"" + condition + "\",\n" +
                "  \"paymentMethodId\": " + paymentMethodId + ",\n" +
                "  \"agreementUrl\": \"" + agreementUrl + "\"\n" +
                "}";
    }

    public static String updateAgreement(int id, String title, int legalEntityId, String startDate, String endDate, int budgetLocationId, String condition, int paymentMethodId, String agreementUrl) {
        return "{\n" +
                "  \"id\": " + id + ",\n" +
                "  \"title\": \"" + title + "\",\n" +
                "  \"legalEntityId\": " + legalEntityId + ",\n" +
                "  \"startDate\": \"" + startDate + "\",\n" +
                "  \"endDate\": \"" + endDate + "\",\n" +
                "  \"budgetLocationId\": " + budgetLocationId + ",\n" +
                "  \"conditions\": \"" + condition + "\",\n" +
                "  \"paymentMethodId\": " + paymentMethodId + ",\n" +
                "  \"agreementUrl\": \"" + agreementUrl + "\"\n" +
                "}";
    }

    public static String createAddendum(String title, int agreementId, String projectIds, String startDate, String endDate, String comment, int paymentTermId, int paymentTermInDays, int currencyId, String url,   Boolean isForNonBillableProjects) {
        return "{\n" +
                "  \"title\": \"" + title + "\",\n" +
                "  \"agreementId\": " + agreementId + ",\n" +
                "  \"projectIds\": [\"" + projectIds + "\"],\n" +
                "  \"startDate\": \"" + startDate + "\",\n" +
                "  \"endDate\": \"" + endDate + "\",\n" +
                "  \"comment\": \"" + comment + "\",\n" +
                "  \"paymentTermId\": " + paymentTermId + ",\n" +
                "  \"paymentTermInDays\": " + paymentTermInDays + ",\n" +
                "  \"currencyId\": " + currencyId + ",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"isForNonBillableProjects\": " + isForNonBillableProjects + "\n" +
                "}";
    }

    public static String updateAddendum(int addendumId, String title, int agreementId, String projectIds, String startDate, String endDate, String comment, int paymentTermId, int paymentTermInDays, int currencyId, String url,   Boolean isForNonBillableProjects) {
        return "{\n" +
                "  \"id\": " + addendumId + ",\n" +
                "  \"title\": \"" + title + "\",\n" +
                "  \"agreementId\": " + agreementId + ",\n" +
                "  \"projectIds\": [\"" + projectIds + "\"],\n" +
                "  \"startDate\": \"" + startDate + "\",\n" +
                "  \"endDate\": \"" + endDate + "\",\n" +
                "  \"comment\": \"" + comment + "\",\n" +
                "  \"paymentTermId\": " + paymentTermId + ",\n" +
                "  \"paymentTermInDays\": " + paymentTermInDays + ",\n" +
                "  \"currencyId\": " + currencyId + ",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"isForNonBillableProjects\": " + isForNonBillableProjects + "\n" +
                "}";
    }

    public static String createRate(int addendumId, int staffId, String name, int rate, int rateUnitId, String fromDate, String toDate, String description) {
        return "{\n" +
                "  \"addendumId\": " + addendumId + ",\n" +
                "  \"staffId\": " + staffId + ",\n" +
                "  \"name\": \"" + name + "\",\n" +
                "  \"rate\": " + rate + ",\n" +
                "  \"rateUnitId\": " + rateUnitId + ",\n" +
                "  \"fromDate\": \"" + fromDate + "\",\n" +
                "  \"toDate\": \"" + toDate + "\",\n" +
                "  \"description\": \"" + description + "\"\n" +
                "}";
    }

    public static String updateRate(int rateId, int staffId, String name, int rate, int rateUnitId, String fromDate, String toDate, String description) {
        return "{\n" +
                "  \"id\": " + rateId + ",\n" +
                "  \"staffId\": " + staffId + ",\n" +
                "  \"name\": \"" + name + "\",\n" +
                "  \"rate\": " + rate + ",\n" +
                "  \"rateUnitId\": " + rateUnitId + ",\n" +
                "  \"fromDate\": \"" + fromDate + "\",\n" +
                "  \"toDate\": \"" + toDate + "\",\n" +
                "  \"description\": \"" + description + "\"\n" +
                "}";
    }
}
package com.dataart.subcontractorstool.apitests.utils.payloads;

public class SubContractorPayloads {
    public static String createSubContractor(String name, int subContractorType, int subContractorStatus, int locationId, String skills, String comment, String contact, String lastInteractionDate, Boolean isNdaSigned, int salesOfficeId, int developmentOfficeId, String companySite, String materials, int marketId) {
        return "{\n" +
                "  \"name\": \"" + name + "\",\n" +
                "  \"subContractorType\": " + subContractorType + ",\n" +
                "  \"subContractorStatus\": " + subContractorStatus + ",\n" +
                "  \"isNdaSigned\": " + isNdaSigned + ",\n" +
                "  \"locationId\": " + locationId + ",\n" +
                "  \"skills\": \"" + skills + "\",\n" +
                "  \"comment\": \"" + comment + "\",\n" +
                "  \"contact\": \"" + contact + "\",\n" +
                "  \"lastInteractionDate\": \"" + lastInteractionDate + "\",\n" +
                "  \"companySite\": \"" + companySite + "\",\n" +
                "  \"salesOfficeId\": " + salesOfficeId + ",\n" +
                "  \"developmentOfficeId\": " + developmentOfficeId +  ",\n" +
                "  \"materials\": \"" + materials + "\",\n" +
                "  \"marketId\": " + marketId + "\n" +
                "}";
    }

    public static String updateSubContractor(int subContractorId, String name, int subContractorType, int subContractorStatus, int locationId, String skills, String comment, String contact, String lastInteractionDate, Boolean isNdaSigned, int salesOfficeId, int developmentOfficeId, String companySite, String materials, int marketId, int accountManagerId) {
        return "{\n" +
                "  \"id\": " + subContractorId + ",\n" +
                "  \"name\": \"" + name + "\",\n" +
                "  \"subContractorType\": " + subContractorType + ",\n" +
                "  \"subContractorStatus\": " + subContractorStatus + ",\n" +
                "  \"isNdaSigned\": " + isNdaSigned + ",\n" +
                "  \"locationId\": " + locationId + ",\n" +
                "  \"skills\": \"" + skills + "\",\n" +
                "  \"comment\": \"" + comment + "\",\n" +
                "  \"contact\": \"" + contact + "\",\n" +
                "  \"lastInteractionDate\": \"" + lastInteractionDate + "\",\n" +
                "  \"companySite\": \"" + companySite + "\",\n" +
                "  \"salesOfficeId\": " + salesOfficeId + ",\n" +
                "  \"developmentOfficeId\": " + developmentOfficeId +  ",\n" +
                "  \"materials\": \"" + materials + "\",\n" +
                "  \"marketId\": " + marketId + ",\n" +
                "  \"accountManagerId\": " + accountManagerId + "\n" +
                "}";
    }

    public static String updateSubContractorStatus(int subContractorId, int subContractorStatusId) {
        return "{\n" +
                "  \"subContractorId\": " + subContractorId + ",\n" +
                "  \"subContractorStatusId\": " + subContractorStatusId + "\n" +
                "}";
    }

    public static String createTax(int subContractorId, int taxTypeId, String taxName, String taxNumber, String url, String date) {
        return "{\n" +
                "  \"subContractorId\": " + subContractorId + ",\n" +
                "  \"taxTypeId\": " + taxTypeId + ",\n" +
                "  \"name\": \"" + taxName + "\",\n" +
                "  \"taxNumber\": \"" + taxNumber + "\",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"date\": \"" + date + "\"\n" +
                "}";
    }

    public static String createTax(int subContractorId, long taxTypeId, String taxName, String taxNumber, String url, String date) {
        return "{\n" +
                "  \"subContractorId\": " + subContractorId + ",\n" +
                "  \"taxTypeId\": " + taxTypeId + ",\n" +
                "  \"name\": \"" + taxName + "\",\n" +
                "  \"taxNumber\": \"" + taxNumber + "\",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"date\": \"" + date + "\"\n" +
                "}";
    }

    public static String createTax(long subContractorId, int taxTypeId, String taxName, String taxNumber, String url, String date) {
        return "{\n" +
                "  \"subContractorId\": " + subContractorId + ",\n" +
                "  \"taxTypeId\": " + taxTypeId + ",\n" +
                "  \"name\": \"" + taxName + "\",\n" +
                "  \"taxNumber\": \"" + taxNumber + "\",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"date\": \"" + date + "\"\n" +
                "}";
    }

    public static String createTax(int subContractorId, int taxTypeId, String taxName, String taxNumber, String url, int date) {
        return "{\n" +
                "  \"subContractorId\": " + subContractorId + ",\n" +
                "  \"taxTypeId\": " + taxTypeId + ",\n" +
                "  \"name\": \"" + taxName + "\",\n" +
                "  \"taxNumber\": \"" + taxNumber + "\",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"date\": " + date + "\n" +
                "}";
    }

    public static String createTax(int subContractorId, int taxTypeId, String taxName, String taxNumber, int url, String date) {
        return "{\n" +
                "  \"subContractorId\": " + subContractorId + ",\n" +
                "  \"taxTypeId\": " + taxTypeId + ",\n" +
                "  \"name\": \"" + taxName + "\",\n" +
                "  \"taxNumber\": \"" + taxNumber + "\",\n" +
                "  \"url\": " + url + ",\n" +
                "  \"date\": \"" + date + "\"\n" +
                "}";
    }

    public static String createTax(int subContractorId, int taxTypeId, String taxName, int taxNumber, String url, String date) {
        return "{\n" +
                "  \"subContractorId\": " + subContractorId + ",\n" +
                "  \"taxTypeId\": " + taxTypeId + ",\n" +
                "  \"name\": \"" + taxName + "\",\n" +
                "  \"taxNumber\": " + taxNumber + ",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"date\": \"" + date + "\"\n" +
                "}";
    }

    public static String createTax(int subContractorId, int taxTypeId, int taxName, String taxNumber, String url, String date) {
        return "{\n" +
                "  \"subContractorId\": " + subContractorId + ",\n" +
                "  \"taxTypeId\": " + taxTypeId + ",\n" +
                "  \"name\": " + taxName + ",\n" +
                "  \"taxNumber\": \"" + taxNumber + "\",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"date\": \"" + date + "\"\n" +
                "}";
    }

    public static String createTax(String subContractorId, int taxTypeId, String taxName, String taxNumber, String url, String date) {
        return "{\n" +
                "  \"subContractorId\": \"" + subContractorId + "\",\n" +
                "  \"taxTypeId\": " + taxTypeId + ",\n" +
                "  \"name\": \"" + taxName + "\",\n" +
                "  \"taxNumber\": \"" + taxNumber + "\",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"date\": \"" + date + "\"\n" +
                "}";
    }

    public static String createTaxSubContractorIdEmpty(int taxTypeId, String taxName, String taxNumber, String url, String date) {
        return "{\n" +
                "  \"subContractorId\": ,\n" +
                "  \"taxTypeId\": " + taxTypeId + ",\n" +
                "  \"name\": \"" + taxName + "\",\n" +
                "  \"taxNumber\": \"" + taxNumber + "\",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"date\": \"" + date + "\"\n" +
                "}";
    }

    public static String createTax(int subContractorId, String taxTypeId, String taxName, String taxNumber, String url, String date) {
        return "{\n" +
                "  \"subContractorId\": " + subContractorId + ",\n" +
                "  \"taxTypeId\": \"" + taxTypeId + "\",\n" +
                "  \"name\": \"" + taxName + "\",\n" +
                "  \"taxNumber\": \"" + taxNumber + "\",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"date\": \"" + date + "\"\n" +
                "}";
    }

    public static String createTaxTaxTypeIdEmpty(int subContractorId, String taxName, String taxNumber, String url, String date) {
        return "{\n" +
                "  \"subContractorId\": " + subContractorId + ",\n" +
                "  \"taxTypeId\": ,\n" +
                "  \"name\": \"" + taxName + "\",\n" +
                "  \"taxNumber\": \"" + taxNumber + "\",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"date\": \"" + date + "\"\n" +
                "}";
    }

    public static String createTaxTaxTypeIdSpecChar(int subContractorId, String taxName, String taxNumber, String url, String date) {
        return "{\n" +
                "  \"subContractorId\": " + subContractorId + ",\n" +
                "  \"taxTypeId\": 1$,\n" +
                "  \"name\": \"" + taxName + "\",\n" +
                "  \"taxNumber\": \"" + taxNumber + "\",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"date\": \"" + date + "\"\n" +
                "}";
    }

    public static String createTaxSubContractorIdSpecChar(int taxTypeId, String taxName, String taxNumber, String url, String date) {
        return "{\n" +
                "  \"subContractorId\": 214$,\n" +
                "  \"taxTypeId\": " + taxTypeId + ",\n" +
                "  \"name\": \"" + taxName + "\",\n" +
                "  \"taxNumber\": \"" + taxNumber + "\",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"date\": \"" + date + "\"\n" +
                "}";
    }

    public static String createTaxBrokenJSONSchema(int subContractorId, int taxTypeId, String taxName, String taxNumber, String url, String date) {
        return "{\n" +
                "  \"subContractorId\" " + subContractorId + ",\n" +
                "  \"taxTypeId\": " + taxTypeId + ",\n" +
                "  \"name\": \"" + taxName + "\",\n" +
                "  \"taxNumber\": \"" + taxNumber + "\",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"date\": \"" + date + "\"\n" +
                "}";
    }

    public static String updateTax(int taxId, int taxTypeId, String taxName, String taxNumber, String url, String date) {
        return "{\n" +
                "  \"id\": " + taxId + ",\n" +
                "  \"taxTypeId\": " + taxTypeId + ",\n" +
                "  \"name\": \"" + taxName + "\",\n" +
                "  \"taxNumber\": \"" + taxNumber + "\",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"date\": \"" + date + "\"\n" +
                "}";
    }

    public static String updateTax(int taxId, long taxTypeId, String taxName, String taxNumber, String url, String date) {
        return "{\n" +
                "  \"id\": " + taxId + ",\n" +
                "  \"taxTypeId\": " + taxTypeId + ",\n" +
                "  \"name\": \"" + taxName + "\",\n" +
                "  \"taxNumber\": \"" + taxNumber + "\",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"date\": \"" + date + "\"\n" +
                "}";
    }

    public static String updateTax(long taxId, int taxTypeId, String taxName, String taxNumber, String url, String date) {
        return "{\n" +
                "  \"id\": " + taxId + ",\n" +
                "  \"taxTypeId\": " + taxTypeId + ",\n" +
                "  \"name\": \"" + taxName + "\",\n" +
                "  \"taxNumber\": \"" + taxNumber + "\",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"date\": \"" + date + "\"\n" +
                "}";
    }

    public static String updateTax(int taxId, int taxTypeId, String taxName, String taxNumber, String url, int date) {
        return "{\n" +
                "  \"id\": " + taxId + ",\n" +
                "  \"taxTypeId\": " + taxTypeId + ",\n" +
                "  \"name\": \"" + taxName + "\",\n" +
                "  \"taxNumber\": \"" + taxNumber + "\",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"date\": " + date + "\n" +
                "}";
    }

    public static String updateTax(int taxId, int taxTypeId, String taxName, String taxNumber, int url, String date) {
        return "{\n" +
                "  \"id\": " + taxId + ",\n" +
                "  \"taxTypeId\": " + taxTypeId + ",\n" +
                "  \"name\": \"" + taxName + "\",\n" +
                "  \"taxNumber\": \"" + taxNumber + "\",\n" +
                "  \"url\": " + url + ",\n" +
                "  \"date\": \"" + date + "\"\n" +
                "}";
    }

    public static String updateTax(int taxId, int taxTypeId, String taxName, int taxNumber, String url, String date) {
        return "{\n" +
                "  \"id\": " + taxId + ",\n" +
                "  \"taxTypeId\": " + taxTypeId + ",\n" +
                "  \"name\": \"" + taxName + "\",\n" +
                "  \"taxNumber\": " + taxNumber + ",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"date\": \"" + date + "\"\n" +
                "}";
    }

    public static String updateTax(int taxId, int taxTypeId, int taxName, String taxNumber, String url, String date) {
        return "{\n" +
                "  \"id\": " + taxId + ",\n" +
                "  \"taxTypeId\": " + taxTypeId + ",\n" +
                "  \"name\": " + taxName + ",\n" +
                "  \"taxNumber\": \"" + taxNumber + "\",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"date\": \"" + date + "\"\n" +
                "}";
    }

    public static String updateTax(int taxId, String taxTypeId, String taxName, String taxNumber, String url, String date) {
        return "{\n" +
                "  \"id\": " + taxId + ",\n" +
                "  \"taxTypeId\": " + taxTypeId + ",\n" +
                "  \"name\": \"" + taxName + "\",\n" +
                "  \"taxNumber\": \"" + taxNumber + "\",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"date\": \"" + date + "\"\n" +
                "}";
    }

    public static String updateTax(String taxId, int taxTypeId, String taxName, String taxNumber, String url, String date) {
        return "{\n" +
                "  \"id\": " + taxId + ",\n" +
                "  \"taxTypeId\": " + taxTypeId + ",\n" +
                "  \"name\": \"" + taxName + "\",\n" +
                "  \"taxNumber\": \"" + taxNumber + "\",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"date\": \"" + date + "\"\n" +
                "}";
    }

    public static String updateTaxTaxIdEmpty(int taxTypeId, String taxName, String taxNumber, String url, String date) {
        return "{\n" +
                "  \"id\": ,\n" +
                "  \"taxTypeId\": " + taxTypeId + ",\n" +
                "  \"name\": \"" + taxName + "\",\n" +
                "  \"taxNumber\": \"" + taxNumber + "\",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"date\": \"" + date + "\"\n" +
                "}";
    }

    public static String updateTaxTaxIdLeadingZero(int taxTypeId, String taxName, String taxNumber, String url, String date) {
        return "{\n" +
                "  \"id\": 01,\n" +
                "  \"taxTypeId\": " + taxTypeId + ",\n" +
                "  \"name\": \"" + taxName + "\",\n" +
                "  \"taxNumber\": \"" + taxNumber + "\",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"date\": \"" + date + "\"\n" +
                "}";
    }

    public static String updateTaxTaxIdSpecChar(int taxTypeId, String taxName, String taxNumber, String url, String date) {
        return "{\n" +
                "  \"id\": 1$,\n" +
                "  \"taxTypeId\": " + taxTypeId + ",\n" +
                "  \"name\": \"" + taxName + "\",\n" +
                "  \"taxNumber\": \"" + taxNumber + "\",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"date\": \"" + date + "\"\n" +
                "}";
    }

    public static String updateTaxTaxTypeIdEmpty(int taxId, String taxName, String taxNumber, String url, String date) {
        return "{\n" +
                "  \"id\": " + taxId + ",\n" +
                "  \"taxTypeId\": ,\n" +
                "  \"name\": \"" + taxName + "\",\n" +
                "  \"taxNumber\": \"" + taxNumber + "\",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"date\": \"" + date + "\"\n" +
                "}";
    }

    public static String updateTaxTaxTypeIdLeadingZero(int taxId, String taxName, String taxNumber, String url, String date) {
        return "{\n" +
                "  \"id\": " + taxId + ",\n" +
                "  \"taxTypeId\": 01,\n" +
                "  \"name\": \"" + taxName + "\",\n" +
                "  \"taxNumber\": \"" + taxNumber + "\",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"date\": \"" + date + "\"\n" +
                "}";
    }

    public static String updateTaxTaxTypeIdSpecChar(int taxId, String taxName, String taxNumber, String url, String date) {
        return "{\n" +
                "  \"id\": " + taxId + ",\n" +
                "  \"taxTypeId\": 1$,\n" +
                "  \"name\": \"" + taxName + "\",\n" +
                "  \"taxNumber\": \"" + taxNumber + "\",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"date\": \"" + date + "\"\n" +
                "}";
    }

    public static String updateTaxBrokenJSONSchema(int taxId, int taxTypeId, String taxName, String taxNumber, String url, String date) {
        return "{\n" +
                "  \"id\" " + taxId + ",\n" +
                "  \"taxTypeId\": " + taxTypeId + ",\n" +
                "  \"name\": \"" + taxName + "\",\n" +
                "  \"taxNumber\": \"" + taxNumber + "\",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"date\": \"" + date + "\"\n" +
                "}";
    }

    public static String deleteTax(int taxId) {
        return "{\n" +
                "  \"id\": " + taxId + "\n" +
                "}";
    }

    public static String deleteTax(long taxId) {
        return "{\n" +
                "  \"id\": " + taxId + "\n" +
                "}";
    }

    public static String deleteTax(String taxId) {
        return "{\n" +
                "  \"id\": \"" + taxId + "\"\n" +
                "}";
    }

    public static String deleteTax() {
        return "{\n" +
                "  \"id\": \n" +
                "}";
    }

    public static String emptyPayload() { return "{}"; }

    public static String noPayload() { return ""; }

    public static String createTaxHugePayload(int subContractorId, int taxTypeId, String taxName, String taxNumber, String url, String date) {
        return "{\n" +
                "  \"subContractorId\": " + subContractorId + ",\n" +
                "  \"taxTypeId\": " + taxTypeId + ",\n" +
                "  \"name\": \"" + taxName + "\",\n" +
                "  \"taxNumber\": \"" + taxNumber + "\",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"date\": \"" + date + "\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\"\n" +
                "}";
    }

    public static String updateTaxHugePayload(int taxId, int taxTypeId, String taxName, String taxNumber, String url, String date) {
        return "{\n" +
                "  \"id\": " + taxId + ",\n" +
                "  \"taxTypeId\": " + taxTypeId + ",\n" +
                "  \"name\": \"" + taxName + "\",\n" +
                "  \"taxNumber\": \"" + taxNumber + "\",\n" +
                "  \"url\": \"" + url + "\",\n" +
                "  \"date\": \"" + date + "\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\",\n" +
                "  \"parameter\": \"parameter\"\n" +
                "}";
    }

    public static String assignStaffToSubContractor(int subContractorId, int staffId) {
        return "{\n" +
                "  \"subContractorId\": " + subContractorId + ",\n" +
                "  \"staffId\": " + staffId + "\n" +
                "}";
    }
}
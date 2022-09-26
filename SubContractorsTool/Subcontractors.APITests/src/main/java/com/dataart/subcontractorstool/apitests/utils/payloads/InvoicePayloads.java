package com.dataart.subcontractorstool.apitests.utils.payloads;

public class InvoicePayloads {
    public static String createInvoice(String invoiceFileId, int subContractorId, String startDate, String endDate, String invoiceDate, Integer paymentNumber, Integer referralId, int amount, String invoiceNumber, int rate, byte taxRate, String comment, String projectId, int addendumId, String supportingDocumentsIds) {
        return "{\n" +
                "  \"invoiceFileId\": \"" + invoiceFileId + "\",\n" +
                "  \"subContractorId\": " + subContractorId + ",\n" +
                "  \"startDate\": \"" + startDate + "\",\n" +
                "  \"endDate\": \"" + endDate + "\",\n" +
                "  \"invoiceDate\": \"" + invoiceDate + "\",\n" +
                "  \"paymentNumber\": " + paymentNumber + ",\n" +
                "  \"referralId\": " + referralId + ",\n" +
                "  \"amount\": " + amount + ",\n" +
                "  \"invoiceNumber\": \"" + invoiceNumber + "\",\n" +
                "  \"taxRate\": " + taxRate + ",\n" +
                "  \"comment\": \"" + comment + "\",\n" +
                "  \"projectId\": \"" + projectId + "\",\n" +
                "  \"addendumId\": " + addendumId + ",\n" +
                "  \"supportingDocumentsIds\": " + supportingDocumentsIds + "\n" +
                "}";
    }

    public static String updateInvoice(int invoiceId, String invoiceFileId, String startDate, String endDate, String invoiceDate, int amount, String invoiceNumber, int rate, byte taxRate, String comment, String projectId, int addendumId, String supportingDocumentsIds) {
        return "{\n" +
                "  \"id\": " + invoiceId + ",\n" +
                "  \"invoiceFileId\": \"" + invoiceFileId + "\",\n" +
                "  \"startDate\": \"" + startDate + "\",\n" +
                "  \"endDate\": \"" + endDate + "\",\n" +
                "  \"invoiceDate\": \"" + invoiceDate + "\",\n" +
                "  \"amount\": " + amount + ",\n" +
                "  \"invoiceNumber\": \"" + invoiceNumber + "\",\n" +
                "  \"taxRate\": " + taxRate + ",\n" +
                "  \"comment\": \"" + comment + "\",\n" +
                "  \"projectId\": \"" + projectId + "\",\n" +
                "  \"addendumId\": " + addendumId + ",\n" +
                "  \"supportingDocumentsIds\": " + supportingDocumentsIds + "\n" +
                "}";
    }

    public static String updateInvoiceStatus(int invoiceId, int statusId) {
        return "{\n" +
                "  \"invoiceId\": " + invoiceId + ",\n" +
                "  \"statusId\": " + statusId + "\n" +
                "}";
    }
}
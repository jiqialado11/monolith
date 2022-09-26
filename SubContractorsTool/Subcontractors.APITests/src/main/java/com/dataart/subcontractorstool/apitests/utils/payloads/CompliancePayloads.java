package com.dataart.subcontractorstool.apitests.utils.payloads;

public class CompliancePayloads {
    public static String createCompliance(int subContractorId, int typeId, int ratingId, String expirationDate, String comment, String fileId) {
        return "{\n" +
                "  \"subContractorId\": " + subContractorId + ",\n" +
                "  \"typeId\": " + typeId + ",\n" +
                "  \"ratingId\": " + ratingId + ",\n" +
                "  \"expirationDate\": \"" + expirationDate + "\",\n" +
                "  \"comment\": \"" + comment + "\",\n" +
                "  \"fileId\": \"" + fileId + "\"\n" +
                "}";
    }

    public static String updateCompliance(int id, int typeId, int ratingId, String expirationDate, String comment, String fileId) {
        return "{\n" +
                "  \"id\": " + id + ",\n" +
                "  \"typeId\": " + typeId + ",\n" +
                "  \"ratingId\": " + ratingId + ",\n" +
                "  \"expirationDate\": \"" + expirationDate + "\",\n" +
                "  \"comment\": \"" + comment + "\",\n" +
                "  \"fileId\": \"" + fileId + "\"\n" +
                "}";
    }
}
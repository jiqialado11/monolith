package com.dataart.subcontractorstool.apitests.utils.payloads;

public class CheckPayloads {
    public static String createSanctionCheck(int parentId, int parentType, int approverId, int checkStatusId, String date, String comment) {
        return "{\n" +
                "  \"parentId\": " + parentId + ",\n" +
                "  \"parentType\": " + parentType + ",\n" +
                "  \"approverId\": " + approverId + ",\n" +
                "  \"checkStatusId\": " + checkStatusId + ",\n" +
                "  \"date\": \"" + date + "\",\n" +
                "  \"comment\": \"" + comment + "\"\n" +
                "}";
    }

    public static String updateSanctionCheck(int parentId, int parentType, int checkId, int approverId, int checkStatusId, String date, String comment) {
        return "{\n" +
                "  \"parentId\": " + parentId + ",\n" +
                "  \"parentType\": " + parentType + ",\n" +
                "  \"checkId\": " + checkId + ",\n" +
                "  \"approverId\": " + approverId + ",\n" +
                "  \"checkStatusId\": " + checkStatusId + ",\n" +
                "  \"date\": \"" + date + "\",\n" +
                "  \"comment\": \"" + comment + "\"\n" +
                "}";
    }

    public static String createBackgroundCheck(int staffId, int approverId, int checkStatusId, String date, String link) {
        return "{\n" +
                "  \"staffId\": " + staffId + ",\n" +
                "  \"approverId\": " + approverId + ",\n" +
                "  \"checkStatusId\": " + checkStatusId + ",\n" +
                "  \"date\": \"" + date + "\",\n" +
                "  \"link\": \"" + link + "\"\n" +
                "}";
    }

    public static String updateBackgroundCheck(int staffId, int checkId, int approverId, int checkStatusId, String date, String link) {
        return "{\n" +
                "  \"staffId\": " + staffId + ",\n" +
                "  \"checkId\": " + checkId + ",\n" +
                "  \"approverId\": " + approverId + ",\n" +
                "  \"checkStatusId\": " + checkStatusId + ",\n" +
                "  \"date\": \"" + date + "\",\n" +
                "  \"link\": \"" + link + "\"\n" +
                "}";
    }
}
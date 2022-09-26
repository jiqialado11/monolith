package com.dataart.subcontractorstool.apitests.utils.payloads;

public class StaffPayloads {
    public static String createStaff(int pmId, int subContractorId, String firstName, String lastName, String email, String skype, String position, String startDate, String endDate, String qualifications, String realLocation, String cellPhone, Boolean isNdaSigned, String departmentName, String domainLogin) {
        return "{\n" +
                "  \"pmId\": " + pmId + ",\n" +
                "  \"subContractorId\": " + subContractorId + ",\n" +
                "  \"firstName\": \"" + firstName + "\",\n" +
                "  \"lastName\": \"" + lastName + "\",\n" +
                "  \"email\": \"" + email + "\",\n" +
                "  \"skype\": \"" + skype + "\",\n" +
                "  \"position\": \"" + position + "\",\n" +
                "  \"startDate\": \"" + startDate + "\",\n" +
                "  \"endDate\": \"" + endDate  + "\",\n" +
                "  \"qualifications\": \"" + qualifications  + "\",\n" +
                "  \"realLocation\": \"" + realLocation  + "\",\n" +
                "  \"cellPhone\": \"" + cellPhone  + "\",\n" +
                "  \"isNdaSigned\": " + isNdaSigned  + ",\n" +
                "  \"departmentName\": \"" + departmentName  + "\",\n" +
                "  \"domainLogin\": \"" + domainLogin + "\"\n" +
                "}";
    }

    public static String updateStaff(int id, int pmId, String firstName, String lastName, String email, String skype, String position, String startDate, String endDate, String qualifications, String realLocation, String cellPhone, Boolean isNdaSigned, String departmentName, String domainLogin) {
        return "{\n" +
                "  \"id\": " + id + ",\n" +
                "  \"pmId\": " + pmId + ",\n" +
                "  \"firstName\": \"" + firstName + "\",\n" +
                "  \"lastName\": \"" + lastName + "\",\n" +
                "  \"email\": \"" + email + "\",\n" +
                "  \"skype\": \"" + skype + "\",\n" +
                "  \"position\": \"" + position + "\",\n" +
                "  \"startDate\": \"" + startDate + "\",\n" +
                "  \"endDate\": \"" + endDate  + "\",\n" +
                "  \"qualifications\": \"" + qualifications  + "\",\n" +
                "  \"realLocation\": \"" + realLocation  + "\",\n" +
                "  \"cellPhone\": \"" + cellPhone  + "\",\n" +
                "  \"isNdaSigned\": " + isNdaSigned  + ",\n" +
                "  \"departmentName\": \"" + departmentName  + "\",\n" +
                "  \"domainLogin\": \"" + domainLogin + "\"\n" +
                "}";
    }

    public static String assignProjectToStaff(String projectId, int staffId) {
        return "{\n" +
                "  \"projectId\": \"" + projectId  + "\",\n" +
                "  \"staffId\": " + staffId + "\n" +
                "}";
    }
}
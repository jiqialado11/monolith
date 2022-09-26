package com.dataart.subcontractorstool.apitests.utils.payloads;

public class ProjectPayloads {
    public static String assignProjectToSubContractor(int pmId, int subContractorId, String projectName, int projectGroupId, String projectGroup, int projectManagerId, String startDate, String estimatedFinishDate, String finishDate, int projectStatusId) {
        return "{\n" +
                "  \"pmId\": " + pmId + ",\n" +
                "  \"subContractorId\": " + subContractorId + ",\n" +
                "  \"projectName\": \"" + projectName + "\",\n" +
                "  \"projectGroupId\": " + projectGroupId + ",\n" +
                "  \"projectGroup\": \"" + projectGroup + "\",\n" +
                "  \"projectManagerId\": " + projectManagerId + ",\n" +
                "  \"startDate\": \"" + startDate + "\",\n" +
                "  \"estimatedFinishDate\": \"" + estimatedFinishDate + "\",\n" +
                "  \"finishDate\": \"" + finishDate + "\",\n" +
                "  \"projectStatusId\": " + projectStatusId + "\n" +
                "}";
    }
}
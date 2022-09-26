package com.dataart.subcontractorstool.apitests.controllers;

import com.dataart.subcontractorstool.apitests.responseentities.project.*;
import com.dataart.subcontractorstool.apitests.utils.Specifications;

import static io.restassured.RestAssured.given;

public class ProjectController {
    private final String projectPath = "/api/Project";
    private final String projectStaffPath = "/api/Project/Staff";
    private final String projectStatusesPath = "/api/Project/Status";
    private final String pmProjectsListPath = "/api/Project/PM";
    private final String projectSubContractorPath = "/api/Project/SubContractor";

    public ProjectGet getProjectsList() {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(projectPath)
                .then().extract().as(ProjectGet.class);
    }

    public ProjectCreate assignProjectToSubContractor(String payload) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .body(payload)
                .when().post(projectPath)
                .then().extract().as(ProjectCreate.class);
    }

    public ProjectDelete unassignProjectFromSubContractor(int subContractorId, String projectId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().delete(projectPath + "?SubContractorId=" + subContractorId + "&ProjectId=" + projectId)
                .then().extract().as(ProjectDelete.class);
    }

    public ProjectStaffGet getProjectsByStaffId(int staffId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(projectStaffPath + "/" + staffId)
                .then().extract().as(ProjectStaffGet.class);
    }

    public ProjectStatusGet getProjectStatuses() {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(projectStatusesPath)
                .then().extract().as(ProjectStatusGet.class);
    }

    public ProjectPmGet getProjectsListFromPM() {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(pmProjectsListPath)
                .then().extract().as(ProjectPmGet.class);
    }

    public ProjectPmIdGet getProjectFromPM(int projectPmId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(pmProjectsListPath + "/" + projectPmId)
                .then().extract().as(ProjectPmIdGet.class);
    }

    public ProjectIdGet getProject(String projectId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(projectPath + "/" + projectId)
                .then().extract().as(ProjectIdGet.class);
    }

    public ProjectSubContractorIdGet getProjectsBySubContractorId(int subContractorId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(projectSubContractorPath + "/" + subContractorId)
                .then().extract().as(ProjectSubContractorIdGet.class);
    }
}
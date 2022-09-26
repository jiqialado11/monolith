package com.dataart.subcontractorstool.apitests.controllers;

import com.dataart.subcontractorstool.apitests.responseentities.compliance.*;
import com.dataart.subcontractorstool.apitests.utils.Specifications;

import java.io.File;

import static io.restassured.RestAssured.given;

public class ComplianceController {
    private final String complianceRatingsPath = "/api/Compliance/Ratings";
    private final String complianceTypesPath = "/api/Compliance/Types";
    private final String uploadFilePath = "/api/Compliance/File";
    private final String compliancePath = "/api/Compliance";

    public ComplianceCreate createCompliance(String payload) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .body(payload)
                .when().post(compliancePath)
                .then().extract().as(ComplianceCreate.class);
    }

    public ComplianceIdGet getCompliance(int complianceId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(compliancePath + "/" + complianceId )
                .then().extract().as(ComplianceIdGet.class);
    }

    public ComplianceGet getComplianceList(int subContractorId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(compliancePath + "?" + "SubContractorId=" + subContractorId )
                .then().extract().as(ComplianceGet.class);
    }

    public ComplianceUpdate updateCompliance(String payload) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .body(payload)
                .when().put(compliancePath)
                .then().extract().as(ComplianceUpdate.class);
    }

    public ComplianceIdDelete deleteCompliance(int complianceId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().delete(compliancePath + "/" + complianceId )
                .then().extract().as(ComplianceIdDelete.class);
    }

    public ComplianceRatingsGet getRatings() {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(complianceRatingsPath)
                .then().extract().as(ComplianceRatingsGet.class);
    }

    public ComplianceTypesGet getTypes() {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(complianceTypesPath)
                .then().extract().as(ComplianceTypesGet.class);
    }

    public ComplianceFilePost uploadFile(File complianceDocument) {
        Specifications.run(Specifications.requestSpecUploadFile(), Specifications.responseSpecOk200());

        return given()
                .multiPart(complianceDocument)
                .when().post(uploadFilePath)
                .then().extract().as(ComplianceFilePost.class);
    }

    public ComplianceFileIdGet getFile(String complianceDocumentId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(uploadFilePath + "/" + complianceDocumentId)
                .then().extract().as(ComplianceFileIdGet.class);
    }
}
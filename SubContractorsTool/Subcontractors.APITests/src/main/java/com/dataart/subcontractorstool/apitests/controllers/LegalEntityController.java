package com.dataart.subcontractorstool.apitests.controllers;

import com.dataart.subcontractorstool.apitests.responseentities.legalentity.LegalEntityGet;
import com.dataart.subcontractorstool.apitests.utils.Specifications;

import static io.restassured.RestAssured.given;

public class LegalEntityController {
    private final String LegalEntityPath = "/api/LegalEntity";

    public LegalEntityGet getLegalEntity() {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(LegalEntityPath)
                .then().extract().as(LegalEntityGet.class);
    }
}
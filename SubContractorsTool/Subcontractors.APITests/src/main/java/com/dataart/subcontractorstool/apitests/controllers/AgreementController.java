package com.dataart.subcontractorstool.apitests.controllers;

import com.dataart.subcontractorstool.apitests.responseentities.agreement.*;
import com.dataart.subcontractorstool.apitests.utils.Specifications;

import static io.restassured.RestAssured.given;

public class AgreementController {
    private final String agreementPath = "/api/Agreement";
    private final String agreementsPath = "/api/Agreement/Agreements";
    private final String addendumPath = "/api/Agreement/Addendum";
    private final String addendaPath = "/api/Agreement/Addenda";
    private final String ratePath = "/api/Agreement/Addendum/Rate";

    public AgreementCreate createAgreement(String payload) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .body(payload)
                .when().post(agreementPath)
                .then().extract().as(AgreementCreate.class);
    }

    public AgreementGet getAgreement(int agreementId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(agreementPath + "/" + agreementId)
                .then().extract().as(AgreementGet.class);
    }

    public AgreementUpdate updateAgreement(String payload) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .body(payload)
                .when().put(agreementPath)
                .then().extract().as(AgreementUpdate.class);
    }

    public AgreementDelete deleteAgreement(int agreementId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().delete(agreementPath + "/" + agreementId)
                .then().extract().as(AgreementDelete.class);
    }

    public AgreementsGet getAgreements(int subContractorId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(agreementsPath + "/" + subContractorId)
                .then().extract().as(AgreementsGet.class);
    }

    public AgreementAddendumCreate createAddendum(String payload) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .body(payload)
                .when().post(addendumPath)
                .then().extract().as(AgreementAddendumCreate.class);
    }

    public AgreementAddendumGet getAddendum(int addendumId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(addendumPath + "/" + addendumId)
                .then().extract().as(AgreementAddendumGet.class);
    }

    public AgreementAddendumUpdate updateAddendum(String payload) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .body(payload)
                .when().put(addendumPath)
                .then().extract().as(AgreementAddendumUpdate.class);
    }

    public AgreementAddendumDelete deleteAddendum(int addendumId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().delete(addendumPath + "/" + addendumId)
                .then().extract().as(AgreementAddendumDelete.class);
    }

    public AgreementAddendaGet getAddenda(int subContractorId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(addendaPath + "/" + subContractorId)
                .then().extract().as(AgreementAddendaGet.class);
    }

    public AgreementAddendumRateCreate createRate(String payload) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .body(payload)
                .when().post(ratePath)
                .then().extract().as(AgreementAddendumRateCreate.class);
    }

    public AgreementAddendumRateGet getRate(int addendumId, int rateId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(addendumPath + "/" + addendumId + "/Rates/" + rateId)
                .then().extract().as(AgreementAddendumRateGet.class);
    }

    public AgreementAddendumRateUpdate updateRate(String payload) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .body(payload)
                .when().put(ratePath)
                .then().extract().as(AgreementAddendumRateUpdate.class);
    }

    public AgreementAddendumRatesGet getRates(int addendumId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(addendumPath + "/" + addendumId + "/Rates")
                .then().extract().as(AgreementAddendumRatesGet.class);
    }

    public AgreementAddendumRateDelete deleteRate(int rateId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().delete(ratePath + "/" + rateId)
                .then().extract().as(AgreementAddendumRateDelete.class);
    }
}
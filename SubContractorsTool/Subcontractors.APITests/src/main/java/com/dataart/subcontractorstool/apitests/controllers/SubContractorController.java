package com.dataart.subcontractorstool.apitests.controllers;

import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.all.SubContractorAllGet;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.markets.SubContractorMarketsGet;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.offices.SubContractorOfficesGet;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.staff.SubContractorStaffCreate;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.staff.SubContractorStaffDelete;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.status.SubContractorStatusGet;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.status.SubContractorStatusUpdate;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.subcontractor.SubContractorCreate;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.subcontractor.SubContractorGet;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.subcontractor.SubContractorUpdate;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.subcontractor.SubContractorsGet;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.tax.*;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.types.SubContractorTypesGet;
import com.dataart.subcontractorstool.apitests.utils.Specifications;
import com.dataart.subcontractorstool.apitests.utils.payloads.SubContractorPayloads;
import io.restassured.response.Response;

import static io.restassured.RestAssured.given;

public class SubContractorController {
    private final String subContractorPath = "/api/SubContractor";
    private final String subContractorAllPath = "/api/SubContractor/All";
    private final String subContractorTypesPath = "/api/SubContractor/Types";
    private final String subContractorMarketsPath = "/api/SubContractor/Markets";
    private final String subContractorStatusPath = "/api/SubContractor/Status";
    private final String subContractorOfficesPath = "/api/SubContractor/Offices";
    private final String taxPath = "/api/SubContractor/Tax";
    private final String taxTypePath = "/api/SubContractor/TaxType";
    private final String taxesPath = "/api/SubContractor";
    private final String subContractorStaffPath = "/api/SubContractor/Staff";

    public SubContractorCreate createSubContractor(String payload) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .body(payload)
                .when().post(subContractorPath)
                .then().extract().as(SubContractorCreate.class);
    }

    public SubContractorUpdate updateSubContractor(String payload) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .body(payload)
                .when().put(subContractorPath)
                .then().extract().as(SubContractorUpdate.class);
    }

    public SubContractorsGet getSubContractors(byte queryType, int resultsQuantity) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(subContractorPath + "?QueryType=" + queryType + "&Results=" + resultsQuantity)
                .then().extract().as(SubContractorsGet.class);
    }

    public SubContractorAllGet getSubContractorsAll() {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(subContractorAllPath)
                .then().extract().as(SubContractorAllGet.class);
    }

    public SubContractorGet getSubContractor(int subContractorId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(subContractorPath + "/" + subContractorId)
                .then().extract().as(SubContractorGet.class);
    }

    public SubContractorTypesGet getSubContractorTypes() {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(subContractorTypesPath)
                .then().extract().as(SubContractorTypesGet.class);
    }

    public SubContractorMarketsGet getSubContractorMarkets() {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(subContractorMarketsPath)
                .then().extract().as(SubContractorMarketsGet.class);
    }

    public SubContractorOfficesGet getSubContractorOffices(int officeTypeId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(subContractorOfficesPath + "?OfficeTypeId=" + officeTypeId)
                .then().extract().as(SubContractorOfficesGet.class);
    }

    public SubContractorStatusGet getSubContractorStatuses() {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(subContractorStatusPath)
                .then().extract().as(SubContractorStatusGet.class);
    }

    public SubContractorStatusUpdate updateSubContractorStatus(String payload) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .body(payload)
                .when().put(subContractorStatusPath)
                .then().extract().as(SubContractorStatusUpdate.class);
    }

    public SubContractorTaxCreate createTax(String payload) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .body(payload)
                .when().post(taxPath)
                .then().extract().as(SubContractorTaxCreate.class);
    }

    public SubContractorTaxCreateValidationError createTaxValidationError(String payload) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .body(payload)
                .when().post(taxPath)
                .then().extract().as(SubContractorTaxCreateValidationError.class);
    }

    public SubContractorTaxUpdate updateTax(String payload) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .body(payload)
                .when().put(taxPath)
                .then().extract().as(SubContractorTaxUpdate.class);
    }

    public SubContractorTaxUpdateValidationError updateTaxValidationError(String payload) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .body(payload)
                .when().put(taxPath)
                .then().extract().as(SubContractorTaxUpdateValidationError.class);
    }

    public SubContractorTaxGet getTax(int taxId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(taxPath + "/" + taxId)
                .then().extract().as(SubContractorTaxGet.class);
    }

    public SubContractorTaxGetValidationFailure getTax() {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(taxPath + "/")
                .then().extract().as(SubContractorTaxGetValidationFailure.class);
    }

    public SubContractorTaxGetValidationFailure getTaxValidationFailure(int taxId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(taxPath + "/" + taxId)
                .then().extract().as(SubContractorTaxGetValidationFailure.class);
    }

    public SubContractorTaxGetValidationFailure getTaxValidationFailure(String taxId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(taxPath + "/" + taxId)
                .then().extract().as(SubContractorTaxGetValidationFailure.class);
    }

    public SubContractorTaxDelete deleteTax(int taxId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .body(SubContractorPayloads.deleteTax(taxId))
                .when().delete(taxPath + "/" + taxId)
                .then().extract().as(SubContractorTaxDelete.class);
    }

    public SubContractorTaxDelete deleteTax(long taxId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .body(SubContractorPayloads.deleteTax(taxId))
                .when().delete(taxPath + "/" + taxId)
                .then().extract().as(SubContractorTaxDelete.class);
    }

    public SubContractorTaxDelete deleteTax(String taxId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .body(SubContractorPayloads.deleteTax(taxId))
                .when().delete(taxPath + "/" + taxId)
                .then().extract().as(SubContractorTaxDelete.class);
    }

    public Response deleteTax() {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecMethodNotAllowed405());

        return given()
                .body(SubContractorPayloads.deleteTax())
                .when().delete(taxPath);
    }

    public SubContractorTaxTypeGet getTaxType() {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(taxTypePath)
                .then().extract().as(SubContractorTaxTypeGet.class);
    }

    public Response createTaxType() {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecMethodNotAllowed405());

        return given()
                .when().post(taxTypePath);
    }

    public Response updateTaxType() {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecMethodNotAllowed405());

        return given()
                .when().put(taxTypePath);
    }

    public Response deleteTaxType() {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecMethodNotAllowed405());

        return given()
                .when().delete(taxTypePath);
    }

    public SubContractorTaxesGet getTaxes(int subContractorId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(taxesPath + "/" + subContractorId + "/Taxes" )
                .then().extract().as(SubContractorTaxesGet.class);
    }

    public SubContractorTaxesGet getTaxes(String subContractorId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(taxesPath + "/" + subContractorId + "/Taxes" )
                .then().extract().as(SubContractorTaxesGet.class);
    }

    public Response createTaxes(int subContractorId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecMethodNotAllowed405());

        return given()
                .when().post(taxesPath + "/" + subContractorId + "/Taxes");
    }

    public Response updateTaxes(int subContractorId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecMethodNotAllowed405());

        return given()
                .when().put(taxesPath + "/" + subContractorId + "/Taxes");
    }

    public Response deleteTaxes(int subContractorId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecMethodNotAllowed405());

        return given()
                .when().delete(taxesPath + "/" + subContractorId + "/Taxes");
    }

    public SubContractorStaffCreate assignStaffToSubContractor(String payload) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .body(payload)
                .when().post(subContractorStaffPath)
                .then().extract().as(SubContractorStaffCreate.class);
    }

    public SubContractorStaffDelete unassignStaffToSubContractor(int subContractorId, int staffId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().delete(subContractorStaffPath + "?SubContractorId=" + subContractorId + "&StaffId=" + staffId)
                .then().extract().as(SubContractorStaffDelete.class);
    }
}
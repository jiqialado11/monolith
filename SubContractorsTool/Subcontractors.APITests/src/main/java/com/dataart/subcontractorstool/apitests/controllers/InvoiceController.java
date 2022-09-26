package com.dataart.subcontractorstool.apitests.controllers;

import com.dataart.subcontractorstool.apitests.responseentities.invoice.InvoiceCreate;
import com.dataart.subcontractorstool.apitests.responseentities.invoice.InvoiceFilesCreate;
import com.dataart.subcontractorstool.apitests.responseentities.invoice.InvoiceGet;
import com.dataart.subcontractorstool.apitests.responseentities.invoice.*;
import com.dataart.subcontractorstool.apitests.utils.Specifications;

import java.io.File;

import static io.restassured.RestAssured.given;

public class InvoiceController {
    private final String invoiceFilesPath = "/api/Invoice/Files";
    private final String invoicePath = "/api/Invoice";
    private final String invoicePagesAllPath = "/api/Invoice/Pages/All";
    private final String invoiceStatusesPath = "/api/Invoice/Status";
    private final String invoiceTypesPath = "/api/Invoice/Types";
    private final String invoiceExportPath = "/api/Invoice/Export";
    private final String invoiceStatusPath = "/api/Invoice/Status";
    private final String invoiceMilestonesPath = "/api/Invoice/Milestones";
    private final String invoiceReferralsPath = "/api/Invoice/Referral";

    public InvoiceCreate createInvoice(String payload) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .body(payload)
                .when().post(invoicePath)
                .then().extract().as(InvoiceCreate.class);
    }

    public InvoiceIdGet getInvoice(int invoiceId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(invoicePath + "/" + invoiceId)
                .then().extract().as(InvoiceIdGet.class);
    }

    public InvoiceUpdate updateInvoice(String payload) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .body(payload)
                .when().put(invoicePath)
                .then().extract().as(InvoiceUpdate.class);
    }

    public InvoiceGet getInvoices(int subContractorId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(invoicePath + "?" + "SubContractorId=" + subContractorId)
                .then().extract().as(InvoiceGet.class);
    }

    public InvoicePagesAllGet getInvoices(Integer currentPage, Integer resultsPerPage) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(invoicePagesAllPath + "?" + "Page=" + currentPage +  "&" + "Results=" + resultsPerPage)
                .then().extract().as(InvoicePagesAllGet.class);
    }

    public InvoiceStatusesGet getInvoiceStatuses() {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(invoiceStatusesPath)
                .then().extract().as(InvoiceStatusesGet.class);
    }

    public InvoiceFilesCreate uploadFiles(File[] invoiceDocuments) {
        Specifications.run(Specifications.requestSpecUploadFile(), Specifications.responseSpecOk200());

        return given()
                .multiPart("files", invoiceDocuments[0])
                .multiPart("files", invoiceDocuments[1])
                .multiPart("files", invoiceDocuments[2])
                .multiPart("files", invoiceDocuments[3])
                .multiPart("files", invoiceDocuments[4])
                .when().post(invoiceFilesPath)
                .then().extract().as(InvoiceFilesCreate.class);
    }

    public InvoiceFilesCreate uploadFile(File[] invoiceDocuments) {
        Specifications.run(Specifications.requestSpecUploadFile(), Specifications.responseSpecOk200());

        return given()
                .multiPart("files", invoiceDocuments[0])
                .when().post(invoiceFilesPath)
                .then().extract().as(InvoiceFilesCreate.class);
    }

    public InvoiceExportGet exportInvoiceList(String startDate, String endDate) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(invoiceExportPath + "?StartDate=" + startDate + "&EndDate=" + endDate)
                .then().extract().as(InvoiceExportGet.class);
    }

    public InvoiceStatusUpdate updateInvoiceStatus(String payload) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .body(payload)
                .when().put(invoiceStatusPath)
                .then().extract().as(InvoiceStatusUpdate.class);
    }

    public InvoiceMilestonesGet getMilestones(int projectId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(invoiceMilestonesPath + "?" + "ProjectId=" + projectId)
                .then().extract().as(InvoiceMilestonesGet.class);
    }

    public InvoiceReferralGet getReferrals(int paymentNumber) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(invoiceReferralsPath + "?" + "PaymentNumber=" + paymentNumber)
                .then().extract().as(InvoiceReferralGet.class);
    }
}
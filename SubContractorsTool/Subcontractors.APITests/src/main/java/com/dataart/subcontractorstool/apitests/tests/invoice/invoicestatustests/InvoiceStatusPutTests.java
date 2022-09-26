package com.dataart.subcontractorstool.apitests.tests.invoice.invoicestatustests;

import com.dataart.subcontractorstool.apitests.controllers.*;
import com.dataart.subcontractorstool.apitests.responseentities.invoice.InvoiceFilesCreateData;
import com.dataart.subcontractorstool.apitests.tests.agreement.agreementaddendumtests.AgreementAddendumTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.agreement.agreementtests.AgreementTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.invoice.invoicefilestests.InvoiceFilesTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.invoice.invoicetests.InvoiceTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.project.projecttests.ProjectTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortests.SubContractorTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.TestsUtils;
import com.dataart.subcontractorstool.apitests.utils.payloads.AgreementPayloads;
import com.dataart.subcontractorstool.apitests.utils.payloads.InvoicePayloads;
import com.dataart.subcontractorstool.apitests.utils.payloads.ProjectPayloads;
import com.dataart.subcontractorstool.apitests.utils.payloads.SubContractorPayloads;
import org.testng.annotations.AfterClass;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.io.File;
import java.io.IOException;

public class InvoiceStatusPutTests {
    private SubContractorController subContractorController;
    private AgreementController agreementController;
    private InvoiceController invoiceController;
    private ProjectController projectController;
    private byte invoiceDocumentsQuantity = 1;
    private File[] invoiceDocuments = new File[invoiceDocumentsQuantity];
    private String[] invoiceDocumentsIds = new String[invoiceDocumentsQuantity];
    private byte supportingDocumentsQuantity = 5;
    private File[] supportingDocuments = new File[supportingDocumentsQuantity];
    private String[] supportingDocumentsIds = new String[supportingDocumentsQuantity];
    private int subContractorId;
    private String projectId;
    private int agreementId;
    private int addendumId;
    private int invoiceId;

    @BeforeClass
    public void setupTest() throws IOException {
        projectController = new ProjectController();
        invoiceController = new InvoiceController();
        agreementController = new AgreementController();
        subContractorController = new SubContractorController();

        subContractorId = subContractorController.createSubContractor(SubContractorPayloads.createSubContractor(SubContractorTestsConstants.SUBCONTRACTOR_NAME, SubContractorTestsConstants.SUBCONTRACTOR_TYPE_ID, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_INACTIVE, TestsUtils.getLocationId(), SubContractorTestsConstants.SKILLS, SubContractorTestsConstants.COMMENT, SubContractorTestsConstants.CONTACT, SubContractorTestsConstants.LAST_INTERACTION_DATE, SubContractorTestsConstants.IS_NDA_SIGNED, SubContractorTestsConstants.SALES_OFFICE_ID, SubContractorTestsConstants.DEVELOPMENT_OFFICE_ID, SubContractorTestsConstants.COMPANY_SITE, SubContractorTestsConstants.MATERIALS, SubContractorTestsConstants.MARKET_ID)).getData();

        agreementId = agreementController.createAgreement(AgreementPayloads.createAgreement(AgreementTestsConstants.TITLE, subContractorId, TestsUtils.getLegalEntityId(), AgreementTestsConstants.START_DATE, AgreementTestsConstants.END_DATE, TestsUtils.getLocationId(), AgreementTestsConstants.CONDITIONS, TestsUtils.getPaymentMethodId(), AgreementTestsConstants.AGREEMENT_URL)).getData();

        for(int i = 0; i<supportingDocumentsQuantity; i++){
            supportingDocuments[i] = TestsUtils.createMicrosoftWordDocument(InvoiceFilesTestsConstants.FILES_PATH + InvoiceFilesTestsConstants.FILES_NAMES[i], InvoiceFilesTestsConstants.FILE_CONTENT);
        }
        for(int i = 0; i<supportingDocumentsQuantity; i++){
            InvoiceFilesCreateData[] filesData = invoiceController.uploadFiles(supportingDocuments).getData();
            supportingDocumentsIds[i] = filesData[i].getId();
        }

        for(int i = 0; i<invoiceDocumentsQuantity; i++){
            invoiceDocuments[i] = TestsUtils.createMicrosoftWordDocument(InvoiceFilesTestsConstants.FILES_PATH + InvoiceFilesTestsConstants.FILE_NAME, InvoiceFilesTestsConstants.FILE_CONTENT);
        }
        for(int i = 0; i<invoiceDocumentsQuantity; i++){
            InvoiceFilesCreateData[] filesData = invoiceController.uploadFile(invoiceDocuments).getData();
            invoiceDocumentsIds[i] = filesData[i].getId();
        }

        projectId = projectController.assignProjectToSubContractor(ProjectPayloads.assignProjectToSubContractor(ProjectTestsConstants.PROJECT_PM_ID, subContractorId, ProjectTestsConstants.PROJECT_NAME, ProjectTestsConstants.PROJECT_GROUP_PM_ID, ProjectTestsConstants.PROJECT_GROUP, ProjectTestsConstants.PROJECT_MANAGER_ID, ProjectTestsConstants.START_DATE, ProjectTestsConstants.ESTIMATED_FINISH_DATE, ProjectTestsConstants.FINISH_DATE, ProjectTestsConstants.PROJECT_STATUS_ID)).getData();

        addendumId = agreementController.createAddendum(AgreementPayloads.createAddendum(AgreementAddendumTestsConstants.TITLE, agreementId, projectId, AgreementAddendumTestsConstants.START_DATE, AgreementAddendumTestsConstants.END_DATE, AgreementAddendumTestsConstants.COMMENT, TestsUtils.getPaymentTermId(), AgreementAddendumTestsConstants.PAYMENT_TERM_IN_DAYS, TestsUtils.getCurrencyId(), AgreementAddendumTestsConstants.ADDENDUM_URL, AgreementAddendumTestsConstants.IS_FOR_NON_BILLABLE_PROJECTS)).getData();

        invoiceId = invoiceController.createInvoice(InvoicePayloads.createInvoice(invoiceDocumentsIds[0], subContractorId, InvoiceTestsConstants.START_DATE, InvoiceTestsConstants.END_DATE, InvoiceTestsConstants.INVOICE_DATE, InvoiceTestsConstants.PAYMENT_NUMBER, InvoiceTestsConstants.REFERRAL_ID,  InvoiceTestsConstants.AMOUNT, InvoiceTestsConstants.NOT_PACKAGE_INVOICE_NUMBER, InvoiceTestsConstants.RATE, InvoiceTestsConstants.TAX_RATE, InvoiceTestsConstants.COMMENT, projectId, addendumId, TestsUtils.convertArrayOfStringsToString(supportingDocumentsIds))).getData();
    }

    @AfterClass
    public void setDownTest() {
        for (File supportingDocument : supportingDocuments) {supportingDocument.delete();}
        for (File invoiceDocument : invoiceDocuments) {invoiceDocument.delete();}
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a PUT to api/Invoice/Status endpoint
     * THEN I should get Status Code of 202 and success message
     */
    @Test
    public void updateInvoiceStatusTest() {
        // Update invoice status to "Rejected" and check if status is "Rejected"
        TestsUtils.updateAndCheckInvoiceStatus(invoiceId, InvoiceTestsConstants.INVOICE_REJECTED_STATUS_ID, subContractorId);

        // Update invoice status to "New" and check if status is "New"
        TestsUtils.updateAndCheckInvoiceStatus(invoiceId, InvoiceTestsConstants.INVOICE_NEW_STATUS_ID, subContractorId);

        // Update invoice status to "Reviewed" and check if status is "Reviewed"
        TestsUtils.updateAndCheckInvoiceStatus(invoiceId, InvoiceTestsConstants.INVOICE_REVIEWED_STATUS_ID, subContractorId);

        // Update invoice status to "Rejected" and check if status is "Rejected"
        TestsUtils.updateAndCheckInvoiceStatus(invoiceId, InvoiceTestsConstants.INVOICE_REJECTED_STATUS_ID, subContractorId);

        // Update invoice status to "New" and check if status is "New"
        TestsUtils.updateAndCheckInvoiceStatus(invoiceId, InvoiceTestsConstants.INVOICE_NEW_STATUS_ID, subContractorId);

        // Update invoice status to "Reviewed" and check if status is "Reviewed"
        TestsUtils.updateAndCheckInvoiceStatus(invoiceId, InvoiceTestsConstants.INVOICE_REVIEWED_STATUS_ID, subContractorId);

        // Update invoice status to "Approved" and check if status is "Approved"
        TestsUtils.updateAndCheckInvoiceStatus(invoiceId, InvoiceTestsConstants.INVOICE_APPROVED_STATUS_ID, subContractorId);
    }
}
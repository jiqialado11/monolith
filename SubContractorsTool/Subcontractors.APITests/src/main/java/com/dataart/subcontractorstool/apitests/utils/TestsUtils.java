package com.dataart.subcontractorstool.apitests.utils;

import com.dataart.subcontractorstool.apitests.controllers.*;
import com.dataart.subcontractorstool.apitests.responseentities.invoice.InvoiceGet;
import com.dataart.subcontractorstool.apitests.responseentities.staff.StaffPmGetData;
import com.dataart.subcontractorstool.apitests.responseentities.staff.StaffSubContractorIdGetData;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.subcontractor.SubContractorsGet;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.subcontractor.SubContractorsGetDataItem;
import com.dataart.subcontractorstool.apitests.tests.invoice.invoicestatusestests.InvoiceStatusesTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortests.SubContractorTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.payloads.InvoicePayloads;
import com.dataart.subcontractorstool.apitests.utils.testentities.Invoice;
import org.apache.commons.io.FileUtils;
import org.apache.poi.xwpf.usermodel.XWPFDocument;
import org.apache.poi.xwpf.usermodel.XWPFParagraph;
import org.apache.poi.xwpf.usermodel.XWPFRun;
import org.testng.Assert;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.*;
import java.util.stream.Stream;

import static org.testng.Assert.fail;

public class TestsUtils {
    private static final SubContractorController subContractorController = new SubContractorController();
    private static final InvoiceController invoiceController = new InvoiceController();
    private static final LegalEntityController legalEntityController = new LegalEntityController();
    private static final CommonController commonController = new CommonController();
    private static final BudgetController budgetController = new BudgetController();
    private static final StaffController staffController = new StaffController();

    public static void checkSubContractorPresenceInVendorLibrary(int subContractorId) {
        SubContractorsGet response = subContractorController.getSubContractors(SubContractorTestsConstants.QUERY_TYPE_LIBRARY, SubContractorTestsConstants.RESULTS_QUANTITY);
        Boolean isSubcontractorPresentInVendorLibrary = false;
        for (SubContractorsGetDataItem item : response.getData().getItems()) {
            if (item.getId() == subContractorId) { isSubcontractorPresentInVendorLibrary = true; }
        }
        if (!isSubcontractorPresentInVendorLibrary) { fail("SubContractor with ID " + subContractorId + " is not present in Vendor Library."); }
    }

    public static void checkSubContractorPresenceInVendorList(int subContractorId) {
        SubContractorsGet response = subContractorController.getSubContractors(SubContractorTestsConstants.QUERY_TYPE_LIST, SubContractorTestsConstants.RESULTS_QUANTITY);
        Boolean isSubcontractorPresentInVendorLibrary = false;
        for (SubContractorsGetDataItem item : response.getData().getItems()) {
            if (item.getId() == subContractorId) { isSubcontractorPresentInVendorLibrary = true; }
        }
        if (!isSubcontractorPresentInVendorLibrary) { fail("SubContractor with ID " + subContractorId + " is not present in Vendor List."); }
    }

    public static void checkSubContractorAbsenceInVendorList(int subContractorId) {
        SubContractorsGet response = subContractorController.getSubContractors(SubContractorTestsConstants.QUERY_TYPE_LIST, SubContractorTestsConstants.RESULTS_QUANTITY);
        Boolean isSubcontractorPresentInVendorList = false;
        for (SubContractorsGetDataItem item : response.getData().getItems()) {
            if (item.getId() == subContractorId) { isSubcontractorPresentInVendorList = true; }
        }
        if (isSubcontractorPresentInVendorList) { fail("SubContractor with ID " + subContractorId + " is present in Vendor List."); }
    }

    public static File createMicrosoftWordDocument(String filePathName, String content) throws IOException {
        XWPFDocument document  = new XWPFDocument();
        File file = new File(filePathName);
        FileOutputStream fos = new FileOutputStream(file);

        XWPFParagraph paragraph = document.createParagraph();
        XWPFRun run = paragraph.createRun();
        run.setText(content);

        document.write(fos);

        fos.close();

        return file;
    }

    public static File convertBase64StringToFile(String filePathName, String base64String) throws IOException {
        byte[] decodedBytes = Base64.getDecoder().decode(base64String);
        FileUtils.writeByteArrayToFile(new File(filePathName), decodedBytes);

        return new File(filePathName);
    }

    public static String convertArrayOfStringsToString(String[] arrayOfStrings) {
        StringBuilder builder = new StringBuilder();

        builder.append("[");
        for(int i = 0; i<arrayOfStrings.length; i++) {
            if( !(i==arrayOfStrings.length-1) ){
                builder.append("\"").append(arrayOfStrings[i]).append("\"").append(", ");
            } else builder.append("\"").append(arrayOfStrings[i]).append("\"");

        }
        builder.append("]");

        return builder.toString();
    }

    public static void updateAndCheckInvoiceStatus(int invoiceId, int statusId, int subContractorId) {
        invoiceController.updateInvoiceStatus(InvoicePayloads.updateInvoiceStatus(invoiceId, statusId));

        InvoiceGet getResponse = invoiceController.getInvoices(subContractorId);

        Assert.assertEquals(getResponse.getData()[0].getInvoiceStatusId(), statusId);
        Assert.assertEquals(getResponse.getData()[0].getInvoiceStatus(), InvoiceStatusesTestsConstants.INVOICE_STATUSES.get(statusId));
    }

    public static List<Invoice> parseFileToInvoiceObjects(String filePathName) throws IOException {
        List<Invoice> invoices = new ArrayList<>();
        List<List<String>> lines = new ArrayList<>();

        try (Stream<String> stream = Files.lines(Paths.get(filePathName))) {

            stream.forEach(line -> {
                List<String> invoice = Arrays.asList(line.split("\\s*,\\s*"));
                lines.add(invoice);
            });

            lines.forEach(line -> {
                invoices.add( new Invoice(line.get(0), line.get(1), line.get(2), line.get(3), line.get(4), line.get(5), line.get(6), line.get(7), line.get(8), line.get(9)));
            });

        }

        return invoices;
    }

    public static String convertDateToISOFormat (String date) {
        return LocalDate.parse(
                date,
                DateTimeFormatter.ofPattern("MM/dd/yyyy hh:mm:ss" , Locale.US)
        ).format(DateTimeFormatter.ISO_DATE);
    }

    public static int getUniquePmStaffId() {
        List<Integer> pmStaffId = new ArrayList<>();
        List<Integer> localStaffId = new ArrayList<>();

        StaffPmGetData[] pmStaff = staffController.getStaffListFromPM().getData();
        StaffSubContractorIdGetData[] localStaff = staffController.getStaffListBySubContractorID(null).getData();

        Arrays.stream(pmStaff).forEach(staff -> pmStaffId.add(staff.getPmId()));
        Arrays.stream(localStaff).forEach(staff -> localStaffId.add(staff.getPmId()));

        pmStaffId.removeAll(localStaffId);

        if(pmStaffId.size() == 0) {
            pmStaffId.add(localStaffId.stream().max(Integer::compare).get() + 1);
        }

        return pmStaffId.get(0);
    }

    public static String addSlashToDomainLogin (String domainLogin) {
        StringBuilder sb = new StringBuilder(domainLogin);
        sb.insert(8, '\\');

        return sb.toString();
    }

    public static Boolean compareArraysOfStrings (String[] actualArray, String[] expectedArray) {
        boolean arraysAreEqual = false;

        Arrays.parallelSort(actualArray);
        Arrays.parallelSort(expectedArray);

        if(Arrays.equals(actualArray, expectedArray)) { arraysAreEqual = true; }

        return arraysAreEqual;
    }

    public static Integer getCurrencyId () { return commonController.getCurrencies().getData()[0].getId(); }

    public static Integer getPaymentTermId () { return budgetController.getPaymentTerms().getData()[0].getId(); }

    public static Integer getLocationId () { return commonController.getLocations().getData()[0].getId(); }

    public static Integer getLegalEntityId () { return legalEntityController.getLegalEntity().getData()[1].getId(); }

    public static Integer getPaymentMethodId () { return budgetController.getPaymentMethods().getData()[0].getId(); }
}
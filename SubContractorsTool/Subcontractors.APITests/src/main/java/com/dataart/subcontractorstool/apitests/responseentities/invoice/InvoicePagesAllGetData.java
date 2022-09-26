package com.dataart.subcontractorstool.apitests.responseentities.invoice;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class InvoicePagesAllGetData {
    InvoicePagesAllGetDataItem[] items;
    Integer currentPage;
    Integer resultsPerPage;
    Integer totalPages;
    Integer totalResults;
}
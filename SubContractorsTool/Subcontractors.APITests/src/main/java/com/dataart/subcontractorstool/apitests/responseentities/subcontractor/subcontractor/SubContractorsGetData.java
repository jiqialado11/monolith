package com.dataart.subcontractorstool.apitests.responseentities.subcontractor.subcontractor;

import lombok.AllArgsConstructor;
import lombok.Getter;

import java.util.List;

@Getter
@AllArgsConstructor
public class SubContractorsGetData {
    SubContractorsGetDataItem[] items;
}
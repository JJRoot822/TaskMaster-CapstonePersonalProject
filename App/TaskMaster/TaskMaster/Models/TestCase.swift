//
//  TestCase.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/4/23.
//

import SwiftData

@model
class TestCase: Codable {
    var testCaseId: Int
    var title: String
    var details: String
    var userId: Int
    var projectId: Int
}

//
//  IssueReport.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/4/23.
//

import SwiftData

@model
class IssueReport: Codable {
    var issueReportId: Int
    var title: String
    var priority: Int
    var severity: Int
    var issueType: Int
    var notes: String
    var fixed: Bool
    var dateResolved: Date?
    var details: String
    var projectId: Int
    var userId: Int
}

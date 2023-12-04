//
//  BugReport.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/4/23.
//

import SwiftData

@Model
class BugReport: Codable {
    var bugReportId: Int
    var title: String
    var details: String
    var notes: String
    var priority: Int
    var severity: Int
    var fixed: Bool
    var dateFixed: Date?
    var userId: Int
    var projectId: Int
}

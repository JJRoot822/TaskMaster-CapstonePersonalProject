//
//  IssueReport.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/4/23.
//

import SwiftData
import Foundation

@Model
class IssueReport {
    var title: String
    var priority: Int
    var severity: Int
    var issueType: Int
    var details: String
    var fixed: Bool
    var dateResolved: Date?
    var notes: String
    var project: Project

    init(title: String, priority: Int, severity: Int, issueType: Int, details: String, fixed: Bool, dateResolved: Date?, notes: String, project: Project) {
        self.title = title
        self.priority = priority
        self.severity = severity
        self.issueType = issueType
        self.details = details
        self.fixed = fixed
        self.dateResolved = dateResolved
        self.notes = notes
        self.project = project
    }

    
}

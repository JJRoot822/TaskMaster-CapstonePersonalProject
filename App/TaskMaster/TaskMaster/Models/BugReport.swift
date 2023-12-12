//
//  BugReport.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/4/23.
//

import SwiftData
import Foundation

@Model
class BugReport {
    var title: String
    var details: String
    var notes: String
    var priority: Int
    var severity: Int
    var fixed: Bool
    var dateFixed: Date?
    var project: Project

    

    init(title: String, details: String, notes: String, priority: Int, severity: Int, fixed: Bool, dateFixed: Date?, user: User, project: Project) {
        self.title = title
        self.details = details
        self.notes = notes
        self.priority = priority
        self.severity = severity
        self.fixed = fixed
        self.dateFixed = dateFixed
        self.user = user
        self.project = project
    }

    
}

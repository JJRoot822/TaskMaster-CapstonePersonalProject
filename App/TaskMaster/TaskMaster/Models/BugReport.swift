//
//  BugReport.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/4/23.
//

import Foundation

struct BugReport: Codable, Identifiable {
    var id: UUID?
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
    
    private enum CodingKeys: String, CodingKey {
        case id
        case bugReportId
        case title
        case details
        case notes
        case priority
        case severity
        case fixed
        case dateFixed
        case userId
        case projectId
    }
}


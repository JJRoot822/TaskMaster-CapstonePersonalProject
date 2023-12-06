//
//  IssueReport.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/4/23.
//

import Foundation

struct IssueReport: Codable, Identifiable {
    var id: UUID?
    var issueReportId: Int
    var title: String
    var priority: Int
    var severity: Int
    var issueType: Int
    var details: String
    var fixed: Bool
    var dateResolved: Date?
    var notes: String
    var userId: Int
    var projectId: Int
    
    private enum CodingKeys: String, CodingKey {
        case id
        case issueReportId
        case title
        case priority
        case severity
        case issueType
        case details
        case fixed
        case dateResolved
        case notes
        case userId
        case projectId
    }
}


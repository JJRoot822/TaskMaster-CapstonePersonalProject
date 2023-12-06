//
//  Project.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/4/23.
//

import Foundation

struct Project: Codable, Identifiable {
    var id: UUID?
    var projectId: Int
    var name: String
    var color: String
    var description: String
    var releaseDate: Date
    var userId: Int
    var tasks: [TaskItem]
    var bugs: [BugReport]
    var issues: [IssueReport]
    var testCases: [TestCase]
    
    private enum CodingKeys: String, CodingKey {
        case id
        case projectId
        case name
        case color
        case description
        case releaseDate
        case userId
        case tasks
        case bugs
        case issues
        case testCases
    }
}




//
//  Project.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/4/23.
//

import SwiftData

@Model
class Project: Codable {
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
}

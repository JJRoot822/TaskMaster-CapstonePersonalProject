//
//  Project.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/4/23.
//

import SwiftData
import Foundation

@Model
class Project {
    var name: String
    var color: String
    var projectDescription: String
    var releaseDate: Date
    
    @Relationship(deleteRule: .cascade, inverse: \TaskItem.Project)
    var tasks: [TaskItem]
    
    @Relationship(deleteRule: .cascade, inverse: \BugReport.Project)
    var bugs: [BugReport]
    
    @Relationship(deleteRule: .cascade, inverse: \IssueReport.Project)
    var issues: [IssueReport]
    
    @Relationship(deleteRule: .cascade, inverse: \TestCase.project)
    var testCases: [TestCase]
    
    

    init(name: String, color: String, projectDescription: String, releaseDate: Date, tasks: [TaskItem], bugs: [BugReport], issues: [IssueReport], testCases: [TestCase]) {
        self.name = name
        self.color = color
        self.projectDescription = projectDescription
        self.releaseDate = releaseDate
        self.tasks = tasks
        self.bugs = bugs
        self.issues = issues
        self.testCases = testCases
    }
}

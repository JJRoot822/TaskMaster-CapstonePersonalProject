//
//  TestCase.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/4/23.
//

import SwiftData
import Foundation

@Model
class TestCase {
    var title: String
    var details: String
    var project: Project
    
    init(title: String, details: String, project: Project) {
        self.testCaseId = testCaseId
        self.title = title
        self.details = details
        self.project = project
    }

    
}

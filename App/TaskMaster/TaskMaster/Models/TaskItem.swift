//
//  TaskItem.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/4/23.
//

import SwiftData
import Foundation

@Model
class TaskItem {
    var title: String
    var priority: Int
    var completed: Bool
    var notes: String
    var details: String
    var dueDate: Date
    var completedDate: Date?
    var project: Project
    
    
    
    init(title: String, priority: Int, completed: Bool, notes: String, details: String, dueDate: Date, completedDate: Date?, project: Project) {
        self.title = title
        self.priority = priority
        self.completed = completed
        self.notes = notes
        self.details = details
        self.dueDate = dueDate
        self.completedDate = completedDate
        self.project = project
    }
    
    
}

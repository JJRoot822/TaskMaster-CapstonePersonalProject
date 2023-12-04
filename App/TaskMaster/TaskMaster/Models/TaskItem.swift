//
//  TaskItem.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/4/23.
//

import SwiftData

@Model
class TaskItem: Codable {
    var taskItemId: Int
    var title: String
    var priority: Int
    var completed: Bool
    var notes: String
    var details: String
    var dueDate: Date
    var completedDate: Date?
    var projectId: Int
    var userId: Int
}

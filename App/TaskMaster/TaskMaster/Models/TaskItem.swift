//
//  TaskItem.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/4/23.
//

import Foundation

struct TaskItem: Codable, Identifiable {
    var id: UUID?
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
    
    private enum CodingKeys: String, CodingKey {
        case id
        case taskItemId
        case title
        case priority
        case completed
        case notes
        case details
        case dueDate
        case completedDate
        case projectId
        case userId
    }
}


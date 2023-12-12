//
//  TaskItemDetailsScreen.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/11/23.
//

//
//  TaskItemDetailsScreen.swift
//  TaskMaster
//
//  Created by YourName on 12/11/23.
//

import SwiftUI

struct TaskItemDetailsScreen: View {
    var taskItem: TaskItem
    var project: Project
    
    @State private var isEditing: Bool = false
    @State private var id: UUID = UUID()
    
    var body: some View {
        NavigationStack {
            Form {
                Section(header: Text("Task Details")) {
                    Text("Title: \(taskItem.title)")
                    Text("Priority: \(taskItem.priority)")
                    Text("Completed: \(taskItem.completed ? "Yes" : "No")")
                    Text("Notes: \(taskItem.notes)")
                    Text("Details: \(taskItem.details)")
                    Text("Due Date: \(formattedDate(taskItem.dueDate))")
                    Text("Completed Date: \(formattedCompletedDate(taskItem.completedDate))")
                }
                
                Button("Edit Task") {
                    isEditing = true
                }
            }
            .sheet(isPresented: $isEditing, onDismiss: {
                id = UUID()
            }) {
                EditTaskItemScreen(taskItem: taskItem)
            }
            #if os(iOS)
            .navigationTitle(Text("\(taskItem.title)"))
            #endif
        }
    }
    
    private func formattedDate(_ date: Date) -> String {
        let dateFormatter = DateFormatter()
        dateFormatter.dateStyle = .medium
        dateFormatter.timeStyle = .short
        return dateFormatter.string(from: date)
    }
    
    private func formattedCompletedDate(_ date: Date?) -> String {
        if let completedDate = date {
            let dateFormatter = DateFormatter()
            dateFormatter.dateStyle = .medium
            dateFormatter.timeStyle = .short
            return dateFormatter.string(from: completedDate)
        } else {
            return "Not completed"
        }
    }
}

//
//  TaskItemCell.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/11/23.
//

import SwiftUI

struct TaskItemCell: View {
    var taskItem: TaskItem
    
    var body: some View {
        HStack(spacing: 10) {
            Image(systemName: taskItem.completed ? "checkmark.circle" : "multiply.circle")
                .resizable()
                .frame(width: 25, height: 25)
                .scaledToFit()

VStack(alignment: .leading) {
    Text("\(taskItem.title)")
        .bold()

    PriorityLevelLabel(priority: taskItem.priority)
}
        }
    }
}

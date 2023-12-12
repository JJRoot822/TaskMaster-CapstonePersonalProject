//
//  ProjectDetailsScreen.swift
//  TaskMaster
//
//  Created by YourName on 12/11/23.
//

import SwiftUI

struct ProjectDetailsScreen: View {
    var project: Project
    
    var body: some View {
        NavigationStack {
            Form {
                Section(header: Text("Project Details")) {
                    Text("Name: \(project.name)")
                    Text("Color: \(project.color)")
                    Text("Description: \(project.projectDescription)")
                    Text("Release Date: \(formattedDate(project.releaseDate))")
                }
            }
            #if os(iOS)
            .navigationTitle(Text("\(project.name)"))
            #endif
        }
    }
    
    private func formattedDate(_ date: Date) -> String {
        let dateFormatter = DateFormatter()
        dateFormatter.dateStyle = .medium
        dateFormatter.timeStyle = .short
        return dateFormatter.string(from: date)
    }
}


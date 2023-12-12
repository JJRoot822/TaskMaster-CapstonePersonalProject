//
//  CreateProjectScreen.swift
//  TaskMaster
//
//  Created by YourName on 12/11/23.
//

import SwiftUI
import SwiftData

struct CreateProjectScreen: View {
    @State private var name: String
    @State private var projectDescription: String = ""
    @State private var color: String = "Blue"
    @State private var releaseDate: Date = Date()

    @Environment(\.modelContext) var modelContext
    
    var body: some View {
        NavigationStack {
            Form {
                TextField("Name", text: $name)
                TextField("Description", text: $projectDescription)
                
                DatePicker("Release Date", selection: $releaseDate, displayedComponents: .date)
                
                Picker("Color", selection: $color) {
                    ForEach(ColorUtility.colors, id: \.name.self) { option in
                        Text("\(option.name)").tag(option.name)
                    }
                }
                
                Button("Create Project") {
                    save()
                }
            }
            #if os(iOS)
            .navigationTitle(Text("Create a Project"))
            #endif
        }
    }
    
    private func save() {
        let project = Project(name: name, color: color, projectDescription: projectDescription, releaseDate: releaseDate, tasks: [], bugs: [], issues: [], testCases: [])
        modelContext.insert(project)
        
        do {
            try modelContext.save()
        } catch {
            fatalError("Failed to save")
        }
    }
}


//
//  EditProjectScreen.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/11/23.
//

import SwiftUI
import SwiftData

struct EditProjectScreen: View {
    @Bindable var project: Project
    
    @Environment(\.modelContext) var modelContext
    
    
    
    var body: some View {
        NavigationStack {
            Form {
                TextField("Name", text: $project.name)
                TextField("Description", text: $project.projectDescription)
                
                DatePicker("Release Date", selection: $project.releaseDate, displayedComponents: .date)
                
                Picker("Color", selection: $project.color) {
                    ForEach(ColorUtility.colors, id: \.name.self) { option in
                        Text("\(option.name)").tag(option.name)
                    }
                }
                
                Button("Save Changes") {
                    save()
                }
            }
            #if os(iOS)
            .navigationTitle(Text("Edit Project"))
            #endif
        }
    }
    
    private func save() {
        do {
            try modelContext.save()
        } catch {
            fatalError("Failed to Save")
        }
    }
}


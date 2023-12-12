//
//  CreateTestCaseScreen.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/11/23.
//

import SwiftUI
import SwiftData

struct CreateTestCaseScreen: View {
    @State private var title: String = ""
    @State private var details: String = ""
    
    @Environment(\.modelContext) var modelContext
    @Environment(\.dismiss) var dismiss

    var project: Project
    
    var body: some View {
        NavigationStack {
            Form {
                TextField("Title", text: $title)
                TextField("Details", text: $details)
                
                Button("Create Test Case") {
                    save()
                }
            }
        }
    }
    
    private func save() {
        let testCase = TestCase(title: title, details: details, project: project)
        
        modelContext.insert(testCase)
        
        do {
            try modelContext.save()
        } catch {
            fatalError("Failed to save.")
        }
    }
}

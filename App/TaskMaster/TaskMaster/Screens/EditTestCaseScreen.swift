//
//  EditTestCaseScreen.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/11/23.
//

import SwiftUI
import SwiftData

struct EditTestCaseScreen: View {
    @Bindable var testCase: TestCase
    
    @Environment(\.modelContext) var modelContext
    @Environment(\.dismiss) var dismiss
    
    var body: some View {
        Form {
            TextField("Title", text: $testCase.title)
            TextField("Details", text: $testCase.details)
            
            Button("Save Changes") {
                save()
                dismiss()
            }
        }
    }
    
    private func save() {
        do {
            try modelContext.save()
        } catch {
            fatalError("Failed to save.")
        }
    }
}

//
//  TestCaseDetailsScreen.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/11/23.
//

import SwiftUI

struct TestCaseDetailsScreen: View {
    var testCase: TestCasevar project: Project
    
    @State private var isEditing: Bool = false
    @State private var id: UUID = UUID()
    
    var body: some View {
        NavigationStack {
            Form {
                Section(header: Text("Test Case Details")) {
                    Text("\(testCase.title)")
                }
                
                Button("Edit Test Case") {
                    isEditing = true
                }
            }
            .sheet(isPresented: $isEditing, onDismiss: {
                id = UUID()
            }) {
                EditTestCaseScreen(testCase: testCase)
            }
            #if os(iOS)
            .navigationTitle(Text("\(testCase.title)"))
            #endif
        }
    }
}



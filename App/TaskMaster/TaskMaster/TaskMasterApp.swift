//
//  TaskMasterApp.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/6/23.
//

import SwiftUI
import SwiftData

@main
struct TaskMasterApp: App {
    var sharedModelContainer: ModelContainer = {
        let schema = Schema([
            User.self, Project.self, BugReport.self, IssueReport.self, TestCase.self ])
        let modelConfiguration = ModelConfiguration(schema: schema, isStoredInMemoryOnly: false)

        do {
            return try ModelContainer(for: schema, configurations: [modelConfiguration])
        } catch {
            fatalError("Could not create ModelContainer: \(error)")
        }
    }()

    var body: some Scene {
        WindowGroup {
            ContentView()
        }
        .modelContainer(sharedModelContainer)
    }
}

//
//  ProjectsList.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/11/23.
//

import SwiftUI
import SwiftData

struct ProjectsList: View {
    @Query private var projects: [Project]
    
    @State private var searchTerm: String
    
    init() {
        let user = KeychainManager.retrieveUser()!
        
        _searchTerm = State(wrappedValue: "")
        
        _projects = Query(filter: #Predicate<Project> {
            $0.userId == user.userId
        })
    }
    
    var filteredProjects: [Project] {
        if searchTerm.isEmpty {
            return projects
        }
        
        return projects.filter {$0.name.contains(searchTerm)}
    }
    
    var body: some View {
        List {
            ForEach(filteredProjects) { project in
                NavigationLink(value: project) {
                    ProjectCell(project: project)
                }
            }
        }
        .navigationDestination(for: Project.self, destination: ProjectItemsList.init)
    }
}

//
//  ProjectItemsList.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/11/23.
//

import SwiftUI
import SwiftData

struct ProjectItemsList: View {
    var project: Project
    
    var body: some View {
        List {
            Section(header: Text("Task Items")) {
                ForEach(project.tasks) { taskItem in
                    NavigationLink(destination: {
                        TaskItemDetailsScreen(taskItem: taskItem)
                    }, label: {
                        TaskItemCell(taskItem: taskItem)
                    })
                }
            }
            
            Section(header: Text("Bug Reports")) {
                ForEach(project.bugs) { bugReport in
                    NavigationLink(destination: {
                        BugReportDetailsScreen(bugReport: bugReport)
                    }, label: {
                        BugReportCell(bugReport: bugReport)
                    })
                }
            }
            
            Section(header: Text("Issue Reports")) {
                ForEach(project.issues) { issueReport in
                    NavigationLink(destination: {
                        IssueReportDetailsScreen(issueReport: issueReport)
                    }, label: {
                        IssueReportCell(issueReport: issueReport)
                    })
                }
            }
            
            Section(header: Text("Test Cases")) {
                ForEach(project.testCases) { testCase in
                    NavigationLink(destination: {
                        TestCaseDetailsScreen(testCase: testCase)
                    }, label: {
                        TestCaseCell(testCase: testCase)
                    })
                }
            }
            
            
        }
    }
}



struct BugReportDetailsScreen: View {
    var bugReport: BugReport
    
    var body: some View {
        Text("")
    }
}

struct IssueReportDetailsScreen: View {
    var issueReport: IssueReport
    
    var body: some View {
        Text("")
    }
}







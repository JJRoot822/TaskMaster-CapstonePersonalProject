//
//  IssueReportCell.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/11/23.
//

import SwiftUI

struct IssueReportCell: View {
    var issueReport: IssueReport
    
    var body: some View {
        HStack(spacing: 10) {
            Image(systemName: issueReport.fixed ? "checkmark.circle" : "multiply.circle")
                .resizable()
                .frame(width: 25, height: 25)
                .scaledToFit()

VStack(alignment: .leading) {
    Text("\(issueReport.title)")
        .bold()

    Spacer().frame(height: 5)

    PriorityLevelLabel(priority: issueReport.priority)
    SeverityLevelLabel(severity: issueReport.severity)
}
        }
    }
}

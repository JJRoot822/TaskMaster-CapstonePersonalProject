//
//  BugReportCell.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/11/23.
//

import SwiftUI

struct BugReportCell: View {
    var bugReport: BugReport
    
    var body: some View {
        HStack(spacing: 10) {
            Image(systemName: bugReport.fixed ? "checkmark.circle" : "multiply.circle")
                .resizable()
                .frame(width: 25, height: 25)
                .scaledToFit()

VStack(alignment: .leading) {
    Text("\(bugReport.title)")
        .bold()

    SeverityLevelLabel(severity: bugReport.severity)
}
        }
    }
}

//
//  SeverityLevelLabel.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/11/23.
//

import SwiftUI

struct SeverityLevelLabel: View {
    var severity: Int
    var label: String {
        switch severity {
            case 1:
            return SeverityLevel.low.rawValue
            case 2:
            return SeverityLevel.medium.rawValue
            case 3:
            return SeverityLevel.high.rawValue
            case 4:
            return SeverityLevel.veryHigh.rawValue
            default:
            return "Unknown Severity Level"
        }
    }

    var body: some View {
        Text(label)
            .foregroundStyle(Color.secondary)
    }
}

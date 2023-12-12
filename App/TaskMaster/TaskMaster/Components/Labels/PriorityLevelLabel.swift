//
//  PriorityLevelLabel.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/11/23.
//

import SwiftUI

struct PriorityLevelLabel: View {
    var priority: Int

    var label: String {
        switch priority {
            case 1:
            return PriorityLevel.low.rawValue
            case 2:
            return PriorityLevel.medium.rawValue
            case 3:
            return PriorityLevel.high.rawValue
            default:
            return "Unknown Priority Level"
        }
    }

    var body: some View {
        Text(label)
    }
}

//
//  ProjectCell.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/11/23.
//

import SwiftUI

struct ProjectCell: View {
    var project: Project
    
    var body: some View {
        Label("\(project.name)", systemImage: "folder")
            .padding(.all, 5)
            .tint(ColorUtility.getColorBy(name: project.color))
    }
}

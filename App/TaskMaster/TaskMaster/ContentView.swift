//
//  ContentView.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/6/23.
//

import SwiftUI
import SwiftData

struct ContentView: View {
    var body: some View {
        NavigationSplitView(sidebar: {
            ProjectsList()
        }, content: {
            EmptyView()
        }, detail: {
            EmptyView()
        })
    }

}

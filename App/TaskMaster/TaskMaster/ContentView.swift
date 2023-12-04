//
//  ContentView.swift
//  TaskMaster
//
//  Created by Joshua Root on 11/28/23.
//

import SwiftUI
import SwiftData

struct ContentView: View {
    @Environment(\.dismiss) var dismiss
    // @Environment(\.modelContext) private var modelContext
    // @Query private var items: [Item]
    @State private var isShowingAuthenticationScreen:  Bool = false

    var body: some View {
        NavigationSplitView(sidebar: {
            
        }, content: {
            Text("Nothing to Show Right Now")
                .foregroundStyle(Color.secondary)
        }, detail: {
            EmptyView()
        })
        .sheet(isPresented: $isShowingAuthenticationScreen, onDismiss: loadData, content: AuthenticationScreen.init)
    }

    private func loadData() {
        
    }
}

struct LoginScreen: View {
    var body: some View {
        Text("")
    }
}

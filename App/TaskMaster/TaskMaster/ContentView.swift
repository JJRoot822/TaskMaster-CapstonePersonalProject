//
//  ContentView.swift
//  TaskMaster
//
//  Created by Joshua Root on 11/28/23.
//

import SwiftUI

struct ContentView: View {
    @State private var isShowingError: Bool = false
    @State private var isShowingAuthenticationScreen:  Bool = true
    @State private var projects: [Project] = []
    @State private var username: String? = nil
    
    var body: some View {
        NavigationSplitView(sidebar: {
            Text("Username: \(username ?? "")")
        }, content: {
            Text("Nothing to Show Right Now")
                .foregroundStyle(Color.secondary)
        }, detail: {
            EmptyView()
        })
        .sheet(isPresented: $isShowingAuthenticationScreen, onDismiss: loadData) {
            // interactiveDismissDisabled() prevents the user from dismissing the log in screen being displayed in a sheet overlay with a swipe of there finger down the screen.
            AuthenticationScreen()
                .interactiveDismissDisabled()
        }
    }

    private func loadData() {
        guard let user = KeychainManager.retrieveUser() else {
            isShowingError.toggle()
            return
        }
        
        projects = user.projects
        username = user.username
        
    }
}


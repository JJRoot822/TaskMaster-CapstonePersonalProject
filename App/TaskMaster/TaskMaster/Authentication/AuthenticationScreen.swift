//
//  AuthenticationTypeScreen.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/3/23.
//

import SwiftUI

struct AuthenticationScreen: View {
    @Environment(\.dismiss) var dismiss
    
    @State private var screen: AuthenticationScreenType = .login
    
    var body: some View {
        Picker("Authentication Screen Type", selection: $screen) {
            Text("Log In").tag(AuthenticationScreenType.login)
            Text("Register").tag(AuthenticationScreenType.register)
        }
        .pickerStyle(.segmented)
        .labelsHidden()
        
        if screen == .login {
            LoginScreen() { /* ... */ }
        } else {
            RegisterScreen() { /* ... */ }
        }
    }
}

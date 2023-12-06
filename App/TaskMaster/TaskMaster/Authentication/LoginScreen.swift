//
//  LoginScreen.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/4/23.
//

import SwiftUI

struct LoginScreen: View {
    @Environment(\.dismiss) var dismiss
    @State private var username: String = ""
    @State private var password: String = ""
    @State private var isShowingLoginFailedError: Bool = false
    
    @State private var errorMessage: String? = nil
    var onSuccessfulLogin: (() -> Void)
    
    var body: some View {
        VStack(spacing: 10) {
            Text("Log In")
                .font(.largeTitle)
                .bold()
            
            Form {
                TextField("Username", text: $username)
                PasswordField(placeholderText: "Password", text: $password)
                
                Button("Log In", action: attemptLogin)
            }
        }
    }
    
    private func attemptLogin() {
        
    }
}

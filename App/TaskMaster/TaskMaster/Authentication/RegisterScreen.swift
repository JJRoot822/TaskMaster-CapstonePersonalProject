//
//  RegisterScreen.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/3/23.
//

import SwiftUI

struct RegisterScreen: View {
    @State private var firstName: String = ""
    @State private var lastName: String = ""
    @State private var username: String = ""
    @State private var email: String = ""
    @State private var password: String = ""
    @State private var verifyPassword: String = ""
    @State private var isShowingPassword: Bool = false
    @State private var isShowingVerifyPassword: Bool = false
    @State private var isShowingRegistrationError: Bool = false
    @State private var errorMessage: String? = nil
    
    var onSuccessfulRegistration: (() -> Void)
    
    var body: some View {
        VStack(spacing: 5) {
            Text("Create an Account")
            
                .font(.largeTitle)
                .bold()
            
            Form {
                TextField("First Name", text: $firstName)
                    .frame(width: 200)
                
                TextField("Last name", text: $lastName)
                    .frame(width: 200)
                
                TextField("Username", text: $username)
                    .frame(width: 200)
                
                TextField("Email", text: $email)
                    .frame(width: 200)
                
                PasswordField(placeholderText: "Password", text: $password)
                PasswordField(placeholderText: "Verify Password", text: $verifyPassword)
                
                
                Button("Create Account", action: attemptAccountCreation)
                    .alert(isPresented: $isShowingRegistrationError) {
                        Alert(title: Text("An Error Occurred"), message: Text("\(errorMessage ?? "Some unknown error happened.")"))
                    }
            }
        }
    }
    
    private func attemptAccountCreation() {
        
    }
}

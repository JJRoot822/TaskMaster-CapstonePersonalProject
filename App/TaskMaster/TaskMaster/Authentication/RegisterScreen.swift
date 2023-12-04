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
    @State private var isShowingPasswordsDoNotMatchError: Bool = false
    @State private var isShowingAccountCreationError: Bool = false
    
    var onSuccessfulRegistration: (() -> Void)
    
    var body: some View {
        VStack(spacing: 5) {
            Text("Create an Account")
                .font(.largeTitle)
                .bold()
            
            Form {
                ControlGroup {
                    TextField("First Name", text: $firstName)
                    TextField("Last name", text: $lastName)
                    TextField("Username", text: $username)
                    TextField("Email", text: $email)
                }
                
                ControlGroup {
                    PasswordField(placeholderText: "Password", text: $password)
                    PasswordField(placeholderText: "Verify Password", text: $verifyPassword)
                }
                
                Button("Create Account", action: attemptAccountCreation)
                    .alert(isPresented: $isShowingPasswordsDoNotMatchError) {
                        Alert(title: Text("Invalid Data"), message: Text("The Passwords don't match. Please try again."))
                    }
                    .alert(isPresented: $isShowingAccountCreationError) {
                        Alert(title: Text("Action Failed"), message: Text("Unable to create an account at this time. Please try again later."))
                    }
            }
        }
    }
    
    private func attemptAccountCreation() {
        if password != verifyPassword {
            isShowingPasswordsDoNotMatchError.toggle()
            
            return
        }
        
        Task {
            do {
                try await createAccount(
                    firstName: firstName,
                    lastName: lastName,
                    username: username,
                    email: email,
                    password: password
                )
                
                onSuccessfulRegistration()
            } catch {
                isShowingAccountCreationError.toggle()
            }
        }
    }
    
    private func createAccount(firstName: String, lastName: String, username: String, email: String, password: String) async throws {
        var userAccount = TMCreateUser(
            firstName: firstName,
            lastName: lastName,
            username: username,
            email: email,
            password: password,
            userRoleId: 1
        )
        
        do {
            try await APIRequestService.shared.post(
                urlString: "https://localhost:5001/api/user/create",
                body: userAccount
            )
        } catch {
            throw error
        }
        
    }
}

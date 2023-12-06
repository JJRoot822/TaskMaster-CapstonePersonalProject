//
//  PasswordField.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/3/23.
//

import SwiftUI

struct PasswordField: View {
    var placeholderText: String
    @Binding var text: String
    
    @State private var isShowingPassword: Bool = false
    
    var buttonLabel: String {
        isShowingPassword ? "Hide" : "Show"
    }
    
    var buttonAccessibilityLabel: String {
        isShowingPassword ? "Hide Password" : "Show Password"
    }
    
    var body: some View {
        HStack(spacing: 5) {
            if isShowingPassword {
                TextField(placeholderText, text: $text)
                    .frame(width: 200)
            } else {
                SecureField(placeholderText, text: $text)
                    .frame(width: 200)
            }
            
            Button(buttonLabel, action: toggleShowPassword)
                .accessibilityLabel(Text(buttonAccessibilityLabel))
        }
    }
    
    func toggleShowPassword() {
        isShowingPassword.toggle()
    }
}

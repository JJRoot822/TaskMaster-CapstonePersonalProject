//
//  CreateUser.swift
//  TaskMaster
//
//  Created by Joshua Root on 11/29/23.
//

import Foundation

struct CreateUser: Codable, Identifiable {
    var id: UUID?
    var firstName: String
    var lastName: String
    var username: String
    var email: String
    var password: String
    var userRoleId: Int
    
    private enum CodingKeys: String, CodingKey {
        case id
        case firstName
        case lastName
        case username
        case email
        case password
        case userRoleId
    }
}

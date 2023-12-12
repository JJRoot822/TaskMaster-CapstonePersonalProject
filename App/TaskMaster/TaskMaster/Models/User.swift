//
//  User.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/4/23.
//

import SwiftData
import Foundation

@Model
class User: Codable {
    var userId: UUID
    var firstName: String
    var lastName: String
    var username: String
    var email: String
    var password: String
    var role: String
    var projects: [Project]

    enum CodingKeys: CodingKey {
        case userId, firstName, lastName, username, email, password, role, projects
    }

    init(userId: UUID, firstName: String, lastName: String, username: String, email: String, password: String, role: String, projects: [Project]) {
        self.userId = userId
        self.firstName = firstName
        self.lastName = lastName
        self.username = username
        self.email = email
        self.password = password
        self.role = role
        self.projects = projects
    }

    required init(from decoder: Decoder) throws {
        let container = try decoder.container(keyedBy: CodingKeys.self)
        userId = try container.decode(UUID.self, forKey: .userId)
        firstName = try container.decode(String.self, forKey: .firstName)
        lastName = try container.decode(String.self, forKey: .lastName)
        username = try container.decode(String.self, forKey: .username)
        email = try container.decode(String.self, forKey: .email)
        password = try container.decode(String.self, forKey: .password)
        role = try container.decode(String.self, forKey: .role)
        projects = try container.decode([Project].self, forKey: .projects)
    }

    func encode(to encoder: Encoder) throws {
        var container = encoder.container(keyedBy: CodingKeys.self)
        try container.encode(userId, forKey: .userId)
        try container.encode(firstName, forKey: .firstName)
        try container.encode(lastName, forKey: .lastName)
        try container.encode(username, forKey: .username)
        try container.encode(email, forKey: .email)
        try container.encode(password, forKey: .password)
        try container.encode(role, forKey: .role)
        try container.encode(projects, forKey: .projects)
    }
}


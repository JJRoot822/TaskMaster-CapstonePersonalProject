//
//  ApplicationUser.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/4/23.
//


struct User: Codable, Identifiable {
    var id: UUID?
    var userId: Int
    var firstName: String
    var lastName: String
    var username: String
    var email: String
    var role: String
    var projects: [APIProject]
    
    private enum CodingKeys: String, CodingKey {
        case id
        case userId
        case firstName
        case lastName
        case username
        case email
        case role
        case projects
    }
}


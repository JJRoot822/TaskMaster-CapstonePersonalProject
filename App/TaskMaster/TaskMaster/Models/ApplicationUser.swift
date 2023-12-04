//
//  ApplicationUser.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/4/23.
//

import SwiftData

@Model
class ApplicationUser: Codable {
    var userId: Int
    var firstName: String
    var lastName: String
    var username: String
    var email: String
    var role: String
    var projects: [Project]
}

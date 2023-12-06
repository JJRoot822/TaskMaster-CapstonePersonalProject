//
//  TMAuthenticationCredentials.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/4/23.
//

import Foundation

struct APIAuthenticationCredentials: Codable {
    var username: String
    var password: String
}

struct APICreateUser: Codable {
    var firstName: String
    var lastName: String
    var username: String
    var email: String
    var password: String
    var userRoleId: Int
}

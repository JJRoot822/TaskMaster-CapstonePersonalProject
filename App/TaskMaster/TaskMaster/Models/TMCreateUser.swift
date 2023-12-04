//
//  TMCreateUser.swift
//  TaskMaster
//
//  Created by Joshua Root on 11/29/23.
//

import SwiftUI

struct TMCreateUser: Codable {
    var firstName: String
    var lastName: String
    var username: String
    var email: String
    var password: String
    var userRoleId: Int
}

//
//  TestCase.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/4/23.
//

import Foundation

struct TestCase: Codable, Identifiable {
    var id: UUID?
    var testCaseId: Int
    var title: String
    var details: String
    var userId: Int
    var projectId: Int
    
    private enum CodingKeys: String, CodingKey {
        case id
        case testCaseId
        case title
        case details
        case userId
        case projectId
    }
}

s

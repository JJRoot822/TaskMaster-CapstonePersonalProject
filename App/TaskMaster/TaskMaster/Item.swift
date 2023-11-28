//
//  Item.swift
//  TaskMaster
//
//  Created by Joshua Root on 11/28/23.
//

import Foundation
import SwiftData

@Model
final class Item {
    var timestamp: Date
    
    init(timestamp: Date) {
        self.timestamp = timestamp
    }
}

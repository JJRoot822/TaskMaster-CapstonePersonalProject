//
//  ColorUtility.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/11/23.
//

import SwiftUI

class ColorUtility {
    static let colors: [(name: String, color: Color)] = [
        (name: "Gray", color: Color.gray),
        (name: "Red", color: Color.red),
        (name: "Green", color: Color.green),
        (name: "Blue", color: Color.blue),
        (name: "Orange", color: Color.orange),
        (name: "Yellow", color: Color.yellow),
        (name: "Pink", color: Color.pink),
        (name: "Purple", color: Color.purple)
    ]
    
    static func getColorBy(name: String) -> Color {
        for option in colors {
            if option.name == name {
                return option.color
            }
        }
        
        return Color.blue
    }
    
    static func getColorBy(color: Color) -> String {
        for option in colors {
            if option.color == color {
                return option.name
            }
        }
        
        return "Blue"
    }
}

//
//  TestCaseCell.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/11/23.
//

import SwiftUI

struct TestCaseCell: View {
    var testCase: TestCase
    
    var body: some View {
        VStack(alignment: .leading) {
            Text("\(testCase.title)")
                .bold()
                .padding(.all, 5)
        }
    }
}

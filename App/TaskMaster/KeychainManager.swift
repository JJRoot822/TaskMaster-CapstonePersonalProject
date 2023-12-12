//
//  KeychainManager.swift
//  TaskMaster
//
//  Created by Joshua Root on 12/5/23.
//

import Foundation
import Security

class KeychainManager {
    
    private static let service: String = "com.joshrootdev.TaskMaster"
    
    static func doesUserExist() -> Bool {
        guard let data = retrieveUserData() else {
            return false
        }
        
        do {
            let _ = try JSONDecoder().decode(User.self, from: data)
            // Perform any additional checks if needed
            return true
        } catch {
            print("Error decoding user data: \(error)")
            return false
        }
    }
    
    static func insertUser(_ user: User) -> Bool {
        if doesUserExist() {
            return false
        }
        
        do {
            let data = try JSONEncoder().encode(user)
            return saveUserData(data)
        } catch {
            print("Error encoding user data: \(error)")
            return false
        }
    }
    
    static func deleteUser() -> Bool {
        return deleteUserData()
    }
    
    private static func saveUserData(_ data: Data) -> Bool {
        let query: [CFString: Any] = [
            kSecClass: kSecClassGenericPassword,
            kSecAttrService: service,
            kSecValueData: data
        ]
        
        SecItemDelete(query as CFDictionary)
        
        let status = SecItemAdd(query as CFDictionary, nil)
        return status == errSecSuccess
    }
    
    private static func retrieveUserData() -> Data? {
        let query: [CFString: Any] = [
            kSecClass: kSecClassGenericPassword,
            kSecAttrService: service,
            kSecReturnData: kCFBooleanTrue as Any
        ]
        
        var result: AnyObject?
        let status = SecItemCopyMatching(query as CFDictionary, &result)
        
        guard status == errSecSuccess, let data = result as? Data else {
            return nil
        }
        
        return data
    }
    
    private static func deleteUserData() -> Bool {
        let query: [CFString: Any] = [
            kSecClass: kSecClassGenericPassword,
            kSecAttrService: service
        ]
        
        let status = SecItemDelete(query as CFDictionary)
        return status == errSecSuccess || status == errSecItemNotFound
    }
    
    static func retrieveUser() -> User? {
        guard let data = retrieveUserData() else {
            return nil
        }
        
        do {
            let user = try JSONDecoder().decode(User.self, from: data)
            return user
        } catch {
            print("Error decoding user data: \(error)")
            return nil
        }
    }
}


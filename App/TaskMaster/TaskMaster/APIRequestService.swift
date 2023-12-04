//
//  APIRequestService.swift
//  TaskMaster
//
//  Created by Joshua Root on 11/29/23.
//

import Foundation

enum HTTPMethod: String {
    case post = "POST"
    case patch = "PATCH"
    case  delete = "DELETE""
}

enum APIError: Error {
    case invalidURL
    case requestFailed(Error)
    case invalidResponse
    case decodingError(Error)
    case badRequest
    case notFound
    case unauthorized
    
}

class APIRequestService {
    static let shared: APIRequestService = APIRequestService()
    
    private init() {}
    
    func get<T: Codable>(urlString: String, type: T.Type) async throws -> T {
        guard let url = URL(string: urlString) else {
            throw APIError.invalidURL
        }
        
        do {
            let (data, _) = try await URLSession.shared.data(from: url)
            let decoder = JSONDecoder()
            let result = try decoder.decode(T.self, from: data)
            return result
        } catch {
            throw APIError.requestFailed(error)
        }
    }

    func post<T: Codable, R: Codable>(urlString: String, body: T, responseType: R.Type) async throws -> R? {
        guard let url = URL(string: urlString) else {
            throw APIError.invalidURL
        }
        
        var request = URLRequest(url: url)
        request.httpMethod = HTTPMethod.post.rawValue
        
        do {
            let encoder = JSONEncoder()
            request.httpBody = try encoder.encode(body)
            
            let (data, response) = try await URLSession.shared.data(for: request)
            
            guard let httpResponse = response as? HTTPURLResponse else {
                throw APIError.invalidResponse
            }
            
            switch httpResponse.statusCode {
            case 401:
                throw APIError.unauthorized
            case 201:
                if data.isEmpty {
                    return nil
                } else {
                    let decoder = JSONDecoder()
                    return try decoder.decode(R.self, from: data)
                }
            case 400:
                throw APIError.badRequest
            default:
                throw APIError.requestFailed(<#T##Error#>)
            }
        } catch {
            throw APIError.requestFailed(error)
        }
    }
    
    func post<T: Codable>(urlString: String, body: T) async throws {
        guard let url = URL(string: urlString) else {
            throw APIError.invalidURL
        }
        
        var request = URLRequest(url: url)
        request.httpMethod = HTTPMethod.post.rawValue
        
        do {
            let encoder = JSONEncoder()
            request.httpBody = try encoder.encode(body)
            
            let (_, response) = try await URLSession.shared.data(for: request)
            
            guard let httpResponse = response as? HTTPURLResponse else {
                throw APIError.invalidResponse
            }
            
            switch httpResponse.statusCode {
                case 201:
                // Creation Successful
                break
                
            case 400:
                throw APIError.badRequest
            case 401:
                throw APIError.unauthorized
            default:
                throw APIError.requestFailed(NSError(domain: "", code: httpResponse.statusCode, userInfo: nil))
            }
        } catch {
            throw APIError.requestFailed(error)
        }
    }

    func patch<T: Codable>(urlString: String, body: T) async throws {
        guard let url = URL(string: urlString) else {
            throw APIError.invalidURL
        }
        var request = URLRequest(url: url)
        request.httpMethod = HTTPMethod.patch.rawValue
        
        do {
            let encoder = JSONEncoder()
            request.httpBody = try encoder.encode(body)
            
            // Because we don't use the data returned from the request, it is represented here as an underscore.
            // We do, however, use the response object to get the status code.
            let (_, response) = try await URLSession.shared.data(for: request)
            
            guard let httpResponse = response as? HTTPURLResponse else {
                throw APIError.invalidResponse
            }
            
            switch httpResponse.statusCode {
            case 204:
                // Update was successful
                break
            case 400:
                throw APIError.badRequest
            case 404:
                throw APIError.notFound
            default:
                throw APIError.requestFailed(NSError(domain: "", code: httpResponse.statusCode, userInfo: nil))
            }
        } catch {
            throw APIError.requestFailed(error)
        }
    }

    func delete(urlString: String) async throws {
        guard let url = URL(string: urlString) else {
            throw APIError.invalidURL
        }
        
        var request = URLRequest(url: url)
        request.httpMethod = "DELETE"
        
        do {
            let (_, response) = try await URLSession.shared.data(for: request)
            
            guard let httpResponse = response as? HTTPURLResponse else {
                throw APIError.invalidResponse
            }
            
            switch httpResponse.statusCode {
            case 204:
                // No Content - Successful deletion
                break
            case 404:
                // Not Found - Object not found
                throw APIError.notFound
            default:
                throw APIError.requestFailed(NSError(domain: "", code: httpResponse.statusCode, userInfo: nil))
            }
        } catch {
            throw APIError.requestFailed(error)
        }
    }
}

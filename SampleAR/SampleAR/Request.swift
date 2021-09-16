//
//  Request.swift
//  SampleAR
//
//  Created by 渡辺拓也 on 2019/12/15.
//  Copyright © 2019 渡辺拓也. All rights reserved.
//

import Foundation

class Request {
    let session: URLSession = URLSession.shared
    
    // POST METHOD
    func get(completionHandler: @escaping (Data?, URLResponse?, Error?) -> Void) throws {
        let url:URL = URL(string: "http://10.11.12.213:8000/result")!
        var request:URLRequest = URLRequest(url: url)
        request.httpMethod = "GET"
        session.dataTask(with: request, completionHandler: completionHandler).resume()
    }
}

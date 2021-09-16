//
//  ObjectManager.swift
//  SampleAR
//
//  Created by 渡辺拓也 on 2019/12/14.
//  Copyright © 2019 渡辺拓也. All rights reserved.
//

import Foundation
import UIKit
import ARKit
import SceneKit

class ReserveInfo {
    var position : SCNVector3? = nil
    var level : Int? = nil
    
    init(position: SCNVector3, level: Int) {
        self.position = position
        self.level = level
    }
}

class ObjectManager {
    
    var orientationManager : OrientationManager? = nil
    var mainNode: SCNNode? = nil
    
    // private
    var isShow: Bool = true
    
    // reserveInfo 関連
    
    var reserveInfos: [ReserveInfo] = []
    
    func ResetReserveInfo () {
        self.reserveInfos = []
    }
    
    func AddReserveInfo (reserveInfo: ReserveInfo) {
        reserveInfos.append(reserveInfo)
    }
    
    // 初期化
    
    func Initialize (orientationManager : OrientationManager, mainNode: SCNNode) {
        self.orientationManager = orientationManager
        self.mainNode = mainNode
    }
    
    private func createImage (resolveInfo: ReserveInfo) -> SCNNode {
        var image = UIImage(named: "futsu")
        if (resolveInfo.level == 1) {
            image = UIImage(named: "kokoyabai")
        }
        if (resolveInfo.level == 2) {
            image = UIImage(named: "wakuwaku")
        }
        if (resolveInfo.level == 3) {
            image = UIImage(named: "kanashimi")
        }
        if (resolveInfo.level == 4) {
            image = UIImage(named: "genki")
        }
        let node = SCNNode()
        let scale: CGFloat = 1.5
        let geometry = SCNPlane(width: image!.size.width * scale / image!.size.height,
                                height: scale)
        geometry.firstMaterial?.diffuse.contents = image
        node.geometry = geometry
        node.position = resolveInfo.position!
        return node
    }
    
    private func createBox (reserveInfo: ReserveInfo) -> SCNNode {
        let geometory = SCNBox(width: 1, height: 1, length: 1, chamferRadius: 0)
        let material = SCNMaterial()
        if (reserveInfo.level! == 0) {
            material.diffuse.contents = UIColor.white
        }
        if (reserveInfo.level! == 1) {
            material.diffuse.contents = UIColor.red
        }
        if (reserveInfo.level! == 2) {
            material.diffuse.contents = UIColor.blue
        }
        geometory.firstMaterial = material
        
        let node = SCNNode(geometry: geometory)
        node.position = RotationToOrientation(vector: reserveInfo.position!)
        return node
    }
    
    private func createNode (reserveInfo: ReserveInfo) -> SCNNode {
        return createImage(resolveInfo: reserveInfo)
    }

    func Show () {
        for reserveInfo in reserveInfos {
            self.mainNode!.addChildNode(createNode(reserveInfo: reserveInfo))
        }
        self.isShow = false
    }
    
    func Remove () {
        self.mainNode!.enumerateChildNodes { (node, stop) in
            node.removeFromParentNode()
        }
        self.isShow = true
    }
    
    func RotationToOrientation (vector: SCNVector3) -> SCNVector3{
        let radian: Float = 0
        return SCNVector3Make(
            vector.x * cos(radian) + vector.z * sin(radian),
            vector.y,
            -vector.x * sin(radian) + vector.z * cos(radian))
    }
    
    func IsShow () -> Bool {
        return isShow
    }
}

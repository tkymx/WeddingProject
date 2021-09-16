//
//  ViewController.swift
//  SampleAR
//
//  Created by 渡辺拓也 on 2019/12/14.
//  Copyright © 2019 渡辺拓也. All rights reserved.
//

import UIKit
import SceneKit
import ARKit
import CoreLocation

class OrientationManager {
    // ロケーションマネージャ
    var locationManager: CLLocationManager!
    
    func Initialize (cLLocationManagerDelegate: CLLocationManagerDelegate) {
        
        // ロケーションマネージャ生成
        locationManager = CLLocationManager()
        // ロケーションマネージャのデリゲート設定
        locationManager.delegate = cLLocationManagerDelegate
        // 角度の取得開始
        locationManager.startUpdatingHeading()
    }
    
    func GetRotation () -> CGFloat{
        return rotation
    }
    var rotation: CGFloat = 0
    
    func SetRotation (rotation: CGFloat) {
        self.rotation = rotation
    }
}

class ViewController: UIViewController, ARSCNViewDelegate, CLLocationManagerDelegate {

    let orientationManager: OrientationManager = OrientationManager()
    let objectManager: ObjectManager = ObjectManager()

    @IBOutlet var sceneView: ARSCNView!
    @IBOutlet weak var estimateState: UILabel!

    var timer:Timer? = nil

    override func viewDidLoad() {
        super.viewDidLoad()
        
        // Set the view's delegate
        sceneView.delegate = self
        
        // Show statistics such as fps and timing information
        sceneView.showsStatistics = true
        
        // Create a new scene
        let scene = SCNScene();

        // Set the scene to the view
        sceneView.scene = scene
        
        orientationManager.Initialize(cLLocationManagerDelegate: self)
        objectManager.Initialize(orientationManager: orientationManager, mainNode: sceneView.scene.rootNode)
        
        // オブジェクト追加
        objectManager.AddReserveInfo(reserveInfo: ReserveInfo(position: SCNVector3(7,2.5,-40), level: 0))
        objectManager.AddReserveInfo(reserveInfo: ReserveInfo(position: SCNVector3(-5,2.5,-30), level: 1))
        objectManager.AddReserveInfo(reserveInfo: ReserveInfo(position: SCNVector3(6,2.5,-20), level: 2))
        objectManager.AddReserveInfo(reserveInfo: ReserveInfo(position: SCNVector3(-5,2.5,-10), level: 3))
        objectManager.AddReserveInfo(reserveInfo: ReserveInfo(position: SCNVector3(1,2.5,-5), level: 4))
        objectManager.AddReserveInfo(reserveInfo: ReserveInfo(position: SCNVector3(-5,2.5,-5), level: 4))

        // タイマーを作る
        self.timer = Timer.scheduledTimer(
            timeInterval: 2,
            target: self,
            selector: #selector(ViewController.onUpdate(timer:)),
            userInfo: nil,
            repeats: true)


        sceneView.debugOptions = ARSCNDebugOptions.showFeaturePoints
    }
    
    private let request: Request = Request()
    
    @objc func onUpdate(timer : Timer){
        
        do {
            try request.get(completionHandler: {data, resuponse , error in
                if (error != nil) {
                    return
                }
                do {

                    self.objectManager.ResetReserveInfo()
                    
                    let jsonObject = try JSONSerialization.jsonObject(with: data!, options: JSONSerialization.ReadingOptions.mutableContainers )
                    print(jsonObject)
                    let jsonArray = jsonObject as! Array<Any>
                    let cache: Dictionary<String,Bool> = Dictionary<String,Bool>()
                    for json in jsonArray {
                        let jsonDic = json as! Dictionary<String, Any>
                        let name = jsonDic["device_id"] as! String
                        if (cache.keys.contains(name)) {
                            continue
                        }
                        
                        let position: SCNVector3 = SCNVector3(
                            (jsonDic["coordinate_x"] as! NSNumber).floatValue,
                            (jsonDic["coordinate_y"] as! NSNumber).floatValue,
                            -(jsonDic["coordinate_z"] as! NSNumber).floatValue)
                        
                        var level = 0
                        var maxValue = (jsonDic["calm"] as! NSNumber).floatValue
                        
                        let angerValue = (jsonDic["anger"] as! NSNumber).floatValue
                        if (maxValue < angerValue) {
                            maxValue = angerValue
                            level = 1
                        }
                        
                        let joyValue = (jsonDic["joy"] as! NSNumber).floatValue
                        if (maxValue < joyValue) {
                            maxValue = joyValue
                            level = 2
                        }
                        
                        let sorrowValue = (jsonDic["sorrow"] as! NSNumber).floatValue
                        if (maxValue < sorrowValue) {
                            maxValue = sorrowValue
                            level = 3
                        }
                        
                        let energyValue = (jsonDic["energy"] as! NSNumber).floatValue
                        if (maxValue < energyValue) {
                            maxValue = energyValue
                            level = 4
                        }
                        
                        self.objectManager.AddReserveInfo(reserveInfo: ReserveInfo(position: position, level: level))
                    }
                } catch {
                    
                }
            })
        } catch {
            
        }
        if (!objectManager.IsShow()) {
            objectManager.Remove()
            objectManager.Show()
        }
    }
    
    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(animated)
        
        // Create a session configuration
        let configuration = ARWorldTrackingConfiguration()
        configuration.planeDetection = [.horizontal, .vertical]
        
        // Prevent the screen from being dimmed after a while as users will likely
        // have long periods of interaction without touching the screen or buttons.
        UIApplication.shared.isIdleTimerDisabled = true
        
        // Run the view's session
        sceneView.session.run(configuration)
    }
    
    func renderer(_ renderer: SCNSceneRenderer, didAdd node: SCNNode, for anchor: ARAnchor) {
        if (objectManager.IsShow()) {
            objectManager.Show()
        }
        
        DispatchQueue.main.async {
            self.estimateState.text = "オブジェクト表示完了"
        }
    }
    
    func renderer(_ renderer: SCNSceneRenderer, didUpdate node: SCNNode, for anchor: ARAnchor) {
    }
    
    func renderer(_ renderer: SCNSceneRenderer, didRemove node: SCNNode, for anchor: ARAnchor) {
    }
    
    override func viewWillDisappear(_ animated: Bool) {
        super.viewWillDisappear(animated)
        
        // Pause the view's session
        sceneView.session.pause()
    }

    // MARK: - ARSCNViewDelegate
    
/*
    // Override to create and configure nodes for anchors added to the view's session.
    func renderer(_ renderer: SCNSceneRenderer, nodeFor anchor: ARAnchor) -> SCNNode? {
        let node = SCNNode()
     
        return node
    }
*/
    
    func session(_ session: ARSession, didFailWithError error: Error) {
    }
    
    func sessionWasInterrupted(_ session: ARSession) {
        // Inform the user that the session has been interrupted, for example, by presenting an overlay
        
    }
    
    func sessionInterruptionEnded(_ session: ARSession) {
        // Reset tracking and/or remove existing anchors if consistent tracking is required
        
    }
    
    // 角度の更新で呼び出されるデリゲートメソッド
    func locationManager(_ manager: CLLocationManager, didUpdateHeading newHeading: CLHeading) {
        // コンパスの針の方向計算
        let rot = CGFloat(-newHeading.magneticHeading) * CGFloat.pi / 180
        orientationManager.SetRotation(rotation: rot)
    }
    
    @IBAction func OnGetObjectDebug(_ sender: Any) {
        objectManager.Remove()
        DispatchQueue.main.async {
            self.estimateState.text = "オブジェクト未表示"
        }
    }
}

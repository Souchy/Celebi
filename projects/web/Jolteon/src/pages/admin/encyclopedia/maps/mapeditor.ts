import * as THREE from 'three';
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader.js';
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls.js'; //'three/addons/controls/OrbitControls.js';
import { IEventAggregator, inject } from 'aurelia';
import MapTools, { CellType } from '../../../../jolteon/formulas/maptools';

@inject(IEventAggregator)
export class MapEditor {

    // three.js 3d map
    public readonly size = { width: 900, height: 800 };
    public readonly scene = new THREE.Scene();
    public readonly renderer = new THREE.WebGLRenderer({ alpha: true });
    public readonly loader = new GLTFLoader();
    public readonly camera = new THREE.OrthographicCamera(1, 1, 1, 1, 0.1, 1000); //PerspectiveCamera(75, this.size.width / this.size.height, 0.1, 1000); //
    // public readonly controls = new OrbitControls(this.camera, this.renderer.domElement);
    public readonly axesHelper = new THREE.AxesHelper(10);
    public readonly raycaster = new THREE.Raycaster();
    public readonly plane = new THREE.Plane();
    public readonly intersections = [];
    public selected = null;
    public hovered = null;
    public readonly worldPosition = new THREE.Vector3();
    public readonly objects = [];
    public cursor;

    // svg
    public svg: SVGSVGElement;
    public groupFloor: SVGGElement;

    // db data
    public model: any;

    constructor(private readonly ea: IEventAggregator) { //}, private readonly mapController) {
        console.log("ctor map editor")
        // this.updatePointer = this.updatePointer;
        // three test
        this.playThreeTest();
    }


    private playThreeTest() {
        // set scene
        this.renderer.setSize(this.size.width, this.size.height); // window.innerWidth, window.innerHeight);
        this.scene.add(this.axesHelper);

        // mouse events
        this.renderer.domElement.addEventListener('pointermove', e => this.onPointerMove(e));
        this.renderer.domElement.addEventListener('pointerdown', e => this.onPointerDown(e));
        // this.renderer.domElement.addEventListener( 'pointerup', this.onPointerCancel );
        // this.renderer.domElement.addEventListener( 'pointerleave', this.onPointerCancel );

        // cursor
        const geometry = new THREE.PlaneGeometry(1, 1);
        const material = new THREE.MeshBasicMaterial({ color: 0xff00ff, side: THREE.DoubleSide });
        this.cursor = new THREE.Mesh(geometry, material);
        this.scene.add(this.cursor);

        // map
        this.generateTestMap();
        // load model
        // this.loader.load('G:/Assets/0-blender/map6/map_6.glb', function (gltf) {
        //     this.scene.add(gltf.scene);
        // }, undefined, function (error) {
        //     console.error(error);
        // });

        // move camera
        // this.controls.update();
        this.viewTop();
        this.viewIso();

        // start loop
        this.render();
    }

    public lastFrameTime = 0;
    public render() {
        requestAnimationFrame(() => this.render());

        let now = Date.now();
        if (!this.lastFrameTime) {
            this.lastFrameTime = now - (1000 / 60); // minus the time for 1 framer at 60fps
        }
        var elapsed = now - this.lastFrameTime;
        this.lastFrameTime = now;

        this.animate(elapsed);
        this.renderer.render(this.scene, this.camera);
    }
    public animate(delta: number) {
        // this.rotateCamera(delta);
        // this.controls.update();
    }
    public viewTop() {
        let bonus = 2;
        this.camera.left    = -(MapTools.mapWidth + bonus) / 2;
        this.camera.right   =  (MapTools.mapWidth + bonus) / 2;
        this.camera.top     =  (MapTools.mapHeight + bonus) / 2;
        this.camera.bottom  = -(MapTools.mapHeight + bonus) / 2;
        this.camera.updateProjectionMatrix();
        // -(MapTools.mapWidth + 1)/2, 25/2, 25/2, -25/2
        // this.camera.setViewOffset(MapTools.mapWidth + 2, MapTools.mapHeight + 2, -MapTools.mapWidth / 2 - 1, -MapTools.mapHeight / 2 - 1)
        this.camera.position.set(MapTools.mapWidth / 2, MapTools.mapHeight / 2 - 1, 15)
        this.camera.lookAt(MapTools.mapWidth / 2, MapTools.mapHeight / 2, 0);
        this.camera.up.set(0, 0, 1);
    }
    public viewIso() {
        let bonus = 10;
        this.camera.left    = -(MapTools.mapWidth + bonus) / 2;
        this.camera.right   =  (MapTools.mapWidth + bonus) / 2;
        this.camera.top     =  (MapTools.mapHeight + bonus) / 2;
        this.camera.bottom  = -(MapTools.mapHeight + bonus) / 2;
        this.camera.updateProjectionMatrix();
        // this.camera.setViewOffset(MapTools.mapWidth + 2, MapTools.mapHeight + 2, -MapTools.mapWidth / 2 - 1, -MapTools.mapHeight / 2 - 1)
        this.camera.position.set(MapTools.mapWidth , 0, 20)
        this.camera.lookAt(MapTools.mapWidth / 2, MapTools.mapHeight / 2, 0);
        this.camera.up.set(0, 0, 1);
    }

    public time: number = 13 * 1000;
    public delta: number = 1;
    public period: number = 30 * 1000;
    public rotateCamera(delta: number) {
        const radius = 10;
        // let x = Math.sin(this.time);
        // let y = 0; //Math.sin(this.time);

        this.time += delta;
        if (this.time >= this.period) this.time = 0;
        let radian = ((this.period - this.time) / this.period) * 2 * Math.PI;
        let x = Math.sin(radian) * radius;
        let y = Math.cos(radian) * radius;
        this.camera.position.x = x + MapTools.mapWidth / 2;
        this.camera.position.y = y + MapTools.mapHeight / 2;

        this.time += 1;
        if (this.time >= this.period) this.time = 0;
        // return {
        //     x, y
        // }
    }

    public generateTestMap() {
        this.model = {};
        this.model.cells = [];

        let color0 = 0x121212;
        let color1 = 0x422e1c;
        for (let i = 0; i < MapTools.mapWidth; i++) {
            for (let j = 0; j < MapTools.mapHeight; j++) {
                let cellid = MapTools.getCellIdForPosition(i, j);
                this.model.cells[cellid] = CellType.hole;

                let geometry = new THREE.BoxGeometry(1, 1, 1);
                // let color = THREE.MathUtils.randInt(0, 0xffffff)
                let k = (i + j) % 2;
                let color = k ? color1 : color0;
                let material = new THREE.MeshBasicMaterial({ color: color });
                let mesh = new THREE.Mesh(geometry, material);
                this.objects.push(mesh);
                this.scene.add(mesh);
                mesh.position.set(i + MapTools.cellhalf, j + MapTools.cellhalf, -0.5);
            }
        }
    }





    // ----------------------------------------------------------------------------------------

    public pointer = { x: 0, y: 0 }
    public get getPointerText() {
        //return 'Pointer: ' + this.pointer.x + ', ' + this.pointer.y + "\n" + 
        return "Hovered: " + this.hovered?.position.x + ", " + this.hovered?.position.y;
    }
    public updatePointer(event?) {
        const rect = this.renderer.domElement.getBoundingClientRect();
        this.pointer.x = (event.clientX - rect.left) / rect.width * 2 - 1;
        this.pointer.y = -(event.clientY - rect.top) / rect.height * 2 + 1;
        if (this.hovered) {
            this.cursor.position.set(this.hovered.position.x, this.hovered.position.y, 0.01);
        } 
        // else {
        //     this.cursor.position.set(this.pointer.x, this.pointer.y, 0);
        // }
        // console.log("pointer: " + this.pointer)
    }
    public onPointerMove(event) {
        this.updatePointer(event);
        this.raycaster.setFromCamera(this.pointer, this.camera);

        // hover support
        if (event.pointerType === 'mouse' || event.pointerType === 'pen') {
            this.intersections.length = 0;
            this.raycaster.setFromCamera(this.pointer, this.camera);
            this.raycaster.intersectObjects(this.objects, true, this.intersections);
            if (this.intersections.length > 0) {
                const object = this.intersections[0].object;
                this.plane.setFromNormalAndCoplanarPoint(this.camera.getWorldDirection(this.plane.normal), this.worldPosition.setFromMatrixPosition(object.matrixWorld));
                if (this.hovered !== object && this.hovered !== null) {
                    // scope.dispatchEvent({ type: 'hoveroff', object: this.hovered });
                    this.renderer.domElement.style.cursor = 'auto';
                    this.hovered = null;
                }
                if (this.hovered !== object) {
                    // scope.dispatchEvent({ type: 'hoveron', object: object });
                    this.renderer.domElement.style.cursor = 'pointer';
                    this.hovered = object;
                }
            } else {
                if (this.hovered !== null) {
                    // scope.dispatchEvent({ type: 'hoveroff', object: this.hovered });
                    this.renderer.domElement.style.cursor = 'auto';
                    this.hovered = null;
                }
            }
        }
    }
    public onPointerDown(event) {
        if(!this.hovered) return;
        this.selected = this.hovered;
    }
    public clickView() {
        // let str = this.btnViewText;
        // if(enu) str = enu;
        // if(!str) str = this.btnViewText;
        if(this.btnViewText == 'Top') {
            this.btnViewText = "Iso";
            this.viewTop();
        } else {
            this.btnViewText = "Top";
            this.viewIso();
        }

    }
    public btnViewText: ("Top" | "Iso") = "Top";


    // ----------------------------------------------------------------------------------------

    public onSaveCell(cell: any) {

    }
    public onSaveAsset(asset: any) {

    }
    public addAsset(assetpath: string) {

    }


}

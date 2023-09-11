import { watch } from '@aurelia/runtime-html';
import * as THREE from 'three';
import { GLTFLoader } from 'three/examples/jsm//loaders/GLTFLoader.js'; // three/addons/
// import { Scene } from 'three';
// import { WebGLRenderer } from 'three';
// import 'es6-promise/auto';
import { get, set } from 'idb-keyval/dist/compat'
// import { get, set } from 'idb-keyval/dist/esm-compat';

export default class AssetManager {

    public fileSystem: Dir;
    public currentDirectory: string = "/";

    public readonly size = { width: 500, height: 405 };
    public readonly scene = new THREE.Scene();
    public readonly camera = new THREE.PerspectiveCamera(75, this.size.width / this.size.height, 0.1, 1000);
    public readonly renderer = new THREE.WebGLRenderer();
    public readonly loader = new GLTFLoader();

    public cube: any;

    constructor() {
        // recover fileSystem // impossible to do automatically, requires user gesture
        // this.recoverFileSystem();

        // three test
        // this.playThreeTest();
    }


    private playThreeTest() {
        // set scene
        this.renderer.setSize(this.size.width, this.size.height); // window.innerWidth, window.innerHeight);
        // document.body.appendChild(this.renderer.domElement);

        this.currentDirectory = "";

        // cube
        let geometry = new THREE.BoxGeometry(1, 1, 1);
        let material = new THREE.MeshBasicMaterial({ color: 0x00ff00 });
        this.cube = new THREE.Mesh(geometry, material);
        // this.scene.add(this.cube);

        // load model
        this.loader.load('G:/Assets/0-blender/map6/map_6.glb', function (gltf) {
            this.scene.add(gltf.scene);
        }, undefined, function (error) {
            console.error(error);
        });

        // move camera
        this.camera.position.z = 5;

        // start loop
        this.render();
    }
    public render() {
        requestAnimationFrame(() => this.render());
        this.animate();
        this.renderer.render(this.scene, this.camera);
    }
    public animate() {
        this.cube.rotation.x += 0.01;
        this.cube.rotation.y += 0.01;
    }


    private async recoverFileSystem() {
        let dirHandle: FileSystemDirectoryHandle = await get('celebi:fileSystem');
        if (!dirHandle) {
            console.log("no file system in db");
            return;
        }
        let allowed = await this.verifyPermission(dirHandle);
        if (!allowed) {
            console.log("file system access denied")
            return;
        }
        await this.importDirectory(dirHandle);
        // let result = await this.findFiles(dirHandle);
        // this.fileSystem = result;
        console.log("recovered file system "); // + JSON.stringify(result))
    }

    private async verifyPermission(handle: FileSystemDirectoryHandle | FileSystemFileHandle) {
        let options: FileSystemHandlePermissionDescriptor = {
            mode: 'read'
        }
        if ((await handle.queryPermission(options)) === 'granted') {
            return true;
        }
        if ((await handle.requestPermission(options)) === 'granted') {
            return true;
        }
        return false;
    }

    private clickSelectFiles(files) {
        console.log("selected files:")
        console.log(files);
    }

    private async clickImport(e) {
        console.log("import " + JSON.stringify(e))
        let dirHandle = await window.showDirectoryPicker();
        await set('celebi:fileSystem', dirHandle);

        this.importDirectory(dirHandle);
    }

    private async importDirectory(dirHandle: FileSystemDirectoryHandle) {
        let dir: Dir = new Dir();
        dir.name = dirHandle.name;
        dir.files = await this.findFiles(dirHandle);
        this.fileSystem = dir;
        // await set('celebi:fileSystem', this.fileSystem);
    }

    private async findFiles(dirHandle: FileSystemDirectoryHandle): Promise<(File | Dir)[]> {
        let files: (File | Dir)[] = []
        let promises: Promise<File>[] = [];
        for await (const entry of dirHandle.values()) {
            let permission = await this.verifyPermission(entry);
            console.log("permission: " + permission + ", " + entry.name)
            if (entry.kind === "directory") {
                // console.log(entry.name);
                let dirfiles = await this.findFiles(entry);
                // files.push(...dirfiles);
                let dir: Dir = new Dir();
                dir.name = entry.name;
                dir.files = dirfiles
                files.push(dir);
            }
            if (entry.kind === "file") {
                // (await entry.getFile("")).name;
                let pr = entry.getFile().then(f => {

                    return f;
                });
                promises.push(pr)
            }
        }
        let result = await Promise.all(promises);
        files.push(...result);
        return files;
    }


    private instanceType(file: (File | Dir)) {
        if (!file) return "directory";
        return file["isDir"] ? "directory" : "file";
    }
    private isDir(file: (File | Dir)) {
        return file["isDir"];
    }
    private getPath(file) {
        return URL.createObjectURL(file);
        let arr = [
            "G:/Assets/",
            this.fileSystem.name,
            this.currentDirectory,
            file.name
        ].filter(s => s);
        let path = arr.join("");
        console.log("getPath: " + this.currentDirectory + ", " + JSON.stringify(arr) + ", " + path)
        return path;
    }
    private fileType(file: (File | Dir)) {
        // console.log("fileType for: ")
        // console.log(file);
        return file.type.split("/")[0];
    }
    private isImage(file: File) {
        return file.type.includes('image');
    }
    private clickFile(file?: (File | Dir)) {
        console.log("clickFile1: " + file + ", currDir: " + this.currentDirectory)
        if (!file) {
            let i = this.currentDirectory.lastIndexOf("/", this.currentDirectory.lastIndexOf("/") - 1);
            console.log("i: " + i)
            if (i != -1)
                this.currentDirectory = this.currentDirectory.substring(0, i + 1);
        } else if (this.isDir(file)) {
            this.currentDirectory += file.name + "/";
        } else {

        }
        console.log("clickFile2: " + file + ", currDir: " + this.currentDirectory)
    }

    private get getFilteredFileSystem() {
        if (this.currentDirectory == "/") return this.fileSystem?.files;
        let splits = this.currentDirectory.split("/").map(s => s.trim()).filter(s => s != "");
        let currDir = this.fileSystem;
        for (let s of splits) {
            if (s == "") continue;
            currDir = currDir.files.find(f => f.name == s) as Dir;
        }
        return currDir?.files;
    }
}

export class Dir {
    public name: string;
    public files: (File | Dir)[];
    public readonly isDir = true;
    public readonly type = "directory"
}

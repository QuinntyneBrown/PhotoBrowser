import { Component, ElementRef, Output, EventEmitter } from "@angular/core";

@Component({
    templateUrl: "./drop-zone.component.html",
    styleUrls: ["./drop-zone.component.css"],
    selector: "ce-drop-zone"
})
export class DropZoneComponent { 
    constructor(
        private _elementRef: ElementRef
    ) {
        this.onDragOver = this.onDragOver.bind(this);
        this.onDrop = this.onDrop.bind(this);

        this.onDropped = new EventEmitter<any>();
    }


    public onDragOver(e: DragEvent) {
        e.stopPropagation();
        e.preventDefault();
    }

    public ngAfterViewInit() {
        this._elementRef.nativeElement.addEventListener("dragover", this.onDragOver);
        this._elementRef.nativeElement.addEventListener("drop", this.onDrop, false);
    }

    public ngOnDestroy() {
        this._elementRef.nativeElement.removeEventListener("dragover", this.onDragOver);
        this._elementRef.nativeElement.removeEventListener("drop", this.onDrop, false);
    }

    public onDropped: EventEmitter<any>;
    
    public async onDrop(e: DragEvent) {
        e.stopPropagation();
        e.preventDefault();

        if (e.dataTransfer && e.dataTransfer.files) {
            const packageFiles = function (fileList: FileList) {
                let formData = new FormData();
                for (var i = 0; i < fileList.length; i++) {
                    formData.append(fileList[i].name, fileList[i]);
                }
                return formData;
            }

            const data = packageFiles(e.dataTransfer.files);

            this.onDropped.emit({ data });            
        }        
    }    
}

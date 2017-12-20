import { Component, ElementRef } from "@angular/core";
import { GalleriesService } from "../galleries/galleries.service";
import { DigitalAssetsService } from "../digital-assets/digital-assets.service";

@Component({
    templateUrl: "./home-page.component.html",
    styleUrls: ["./home-page.component.css"],
    selector: "ce-home-page"
})
export class HomePageComponent {
    constructor(
        private _galleriesService: GalleriesService,
        private _digitalAssetsService: DigitalAssetsService
    ) { }

    public ngOnInit() {
        this._galleriesService.get()
            .subscribe(x => this._galleries = x.galleries);

        this._digitalAssetsService.get()
            .subscribe(x => this._galleries = x.galleries);
    }

    public handleOnDropped($event) {
        this._digitalAssetsService.upload({ digitalAssets: $event.data })
            .subscribe(x => { });        
    }

    public _galleries: Array<any> = [];

    public _digitalAssets: Array<any> = [];
}

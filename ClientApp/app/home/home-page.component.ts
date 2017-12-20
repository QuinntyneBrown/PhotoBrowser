import { Component, ElementRef } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { GalleriesService } from "../galleries/galleries.service";
import { DigitalAssetsService } from "../digital-assets/digital-assets.service";

@Component({
    templateUrl: "./home-page.component.html",
    styleUrls: ["./home-page.component.css"],
    selector: "ce-home-page"
})
export class HomePageComponent {
    constructor(
        private _client: HttpClient,
        private _galleriesService: GalleriesService,
        private _digitalAssetsService: DigitalAssetsService
    ) { }

    public handleOnDropped($event) {
        this._client.post<{ digitalAssets: Array<any> }>("/api/digitalassets/upload", $event.data)
            .subscribe(x => { });
    }
}

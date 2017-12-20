import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { DigitalAsset } from "./digital-asset.model";
import { Observable } from "rxjs/Observable";
import { ErrorService } from "../shared/services/error.service";

@Injectable()
export class DigitalAssetsService {
    constructor(
        private _errorService: ErrorService,
        private _client: HttpClient)
    { }

    public upload(options: { digitalAssets: FormData, galleryName?: string }) {

        const headers = new HttpHeaders();
        headers.set("galleryName", options.galleryName);

        return this._client
            .post(`${this._baseUrl}/api/digitalassets/upload`, options.digitalAssets, {
                headers
            })
            .catch(this._errorService.catchErrorResponse);
    }


    public get _baseUrl() { return ""; }
}

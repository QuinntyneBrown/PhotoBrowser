import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { DigitalAsset } from "./digital-asset.model";
import { Observable } from "rxjs/Observable";
import { ErrorService } from "../shared/services/error.service";

@Injectable()
export class DigitalAssetsService {
    constructor(
        private _errorService: ErrorService,
        private _httpClient: HttpClient)
    { }

    public addOrUpdate(options: { digitalAsset: DigitalAsset, correlationId: string }) {
        return this._httpClient
            .post(`${this._baseUrl}/api/digitalAssets/add`, options)
            .catch(this._errorService.catchErrorResponse);
    }

    public get(): Observable<{ digitalAssets: Array<DigitalAsset> }> {
        return this._httpClient
            .get<{ digitalAssets: Array<DigitalAsset> }>(`${this._baseUrl}/api/digitalAssets/get`)
            .catch(this._errorService.catchErrorResponse);
    }

    public getById(options: { id: number }): Observable<{ digitalAsset:DigitalAsset}> {
        return this._httpClient
            .get<{digitalAsset: DigitalAsset}>(`${this._baseUrl}/api/digitalAssets/getById?id=${options.id}`)
            .catch(this._errorService.catchErrorResponse);
    }

    public remove(options: { digitalAsset: DigitalAsset, correlationId: string }) {
        return this._httpClient
            .delete(`${this._baseUrl}/api/digitalAssets/remove?id=${options.digitalAsset.id}&correlationId=${options.correlationId}`)
            .catch(this._errorService.catchErrorResponse);
    }

    public get _baseUrl() { return ""; }
}

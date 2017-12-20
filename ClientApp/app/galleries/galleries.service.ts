import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Gallery } from "./gallery.model";
import { Observable } from "rxjs/Observable";
import { ErrorService } from "../shared/services/error.service";

@Injectable()
export class GalleriesService {
    constructor(
        private _errorService: ErrorService,
        private _httpClient: HttpClient)
    { }

    public addOrUpdate(options: { gallery: Gallery, correlationId: string }) {
        return this._httpClient
            .post(`${this._baseUrl}/api/gallerys/add`, options)
            .catch(this._errorService.catchErrorResponse);
    }

    public get(): Observable<{ gallerys: Array<Gallery> }> {
        return this._httpClient
            .get<{ gallerys: Array<Gallery> }>(`${this._baseUrl}/api/gallerys/get`)
            .catch(this._errorService.catchErrorResponse);
    }

    public getById(options: { id: number }): Observable<{ gallery:Gallery}> {
        return this._httpClient
            .get<{gallery: Gallery}>(`${this._baseUrl}/api/gallerys/getById?id=${options.id}`)
            .catch(this._errorService.catchErrorResponse);
    }

    public remove(options: { gallery: Gallery, correlationId: string }) {
        return this._httpClient
            .delete(`${this._baseUrl}/api/gallerys/remove?id=${options.gallery.id}&correlationId=${options.correlationId}`)
            .catch(this._errorService.catchErrorResponse);
    }

    public get _baseUrl() { return ""; }
}

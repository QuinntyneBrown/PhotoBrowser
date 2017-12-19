import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Injectable()
export class UsersService {
    constructor(
        private _httpClient: HttpClient
    ) { }

    public getCurrent() {
        return this._httpClient.get<{user:any}>("/api/users/current");
    }
}
import { Component } from "@angular/core";
import { Subject } from "rxjs/Subject";

@Component({
    templateUrl: "./top-bar.component.html",
    styleUrls: ["./top-bar.component.css"],
    selector: "ce-top-bar"
})
export class TopBarComponent { 

    private _ngUnsubscribe: Subject<void> = new Subject<void>();

    ngOnDestroy() {
         this._ngUnsubscribe.next();	
    }
}

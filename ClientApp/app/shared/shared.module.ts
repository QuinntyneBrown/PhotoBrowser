import {NgModule, CUSTOM_ELEMENTS_SCHEMA} from '@angular/core';
import {CommonModule} from "@angular/common";
import {RouterModule} from "@angular/router";
import {HttpClientModule,HTTP_INTERCEPTORS} from "@angular/common/http";

import {AuthenticationService} from "./services/authentication.service";
import {CorrelationIdsList} from "./services/correlation-ids-list";
import {LoginRedirectService} from "./services/login-redirect.service";
import {EventHub} from "./services/event-hub";
import {Storage} from "./services/storage.service";
import {ErrorService} from "./services/error.service";
import {PopoverService} from "./services/popover.service";
import {Ruler} from "./services/ruler";
import {Position} from "./services/position";
import {Space} from "./services/space";
import {ModalService} from "./services/modal.service";

import {AuthGuardService} from "./guards/auth-guard.service"
import {TenantGuardService} from "./guards/tenant-guard.service";
import {EventHubConnectionGuardService} from "./guards/event-hub-connection-guard.service";

import {JwtInterceptor} from "./interceptors/jwt.interceptor";
import {AuthInterceptor} from "./interceptors/auth.interceptor";
import {TenantInterceptor} from "./interceptors/tenant.interceptor";

import {HeaderComponent} from "./components/header.component";
import {LeftNavComponent} from "./components/left-nav.component";
import {PagerComponent} from "./components/pager.component";
import {TabContentComponent} from "./components/tab-content.component";
import {TabTitleComponent} from "./components/tab-title.component";
import {TabsComponent} from "./components/tabs.component";
import {ModalWindowComponent} from "./components/modal-window.component";
import {BackdropComponent} from "./components/backdrop.component";

const providers = [
    EventHub,
    AuthGuardService,
    AuthenticationService,
    CorrelationIdsList,
    ErrorService,
    LoginRedirectService,  
    TenantGuardService,  
    EventHubConnectionGuardService,
    Storage,
    PopoverService,
    Space,
    Ruler,
    Position,
    ModalService,
    //{
    //    provide: HTTP_INTERCEPTORS,
    //    useClass: JwtInterceptor,
    //    multi: true
    //},
    {
        provide: HTTP_INTERCEPTORS,
        useClass: AuthInterceptor,
        multi: true
    },
    {
        provide: HTTP_INTERCEPTORS,
        useClass: TenantInterceptor,
        multi: true
    }
];

const declarables = [
    HeaderComponent,
    LeftNavComponent,
    PagerComponent,
    TabContentComponent,
    TabTitleComponent,
    TabsComponent,
    ModalWindowComponent,
    BackdropComponent
];

@NgModule({
    imports: [CommonModule, RouterModule, HttpClientModule],
    entryComponents: [ModalWindowComponent,BackdropComponent],
    declarations: [declarables],
    exports:[declarables],
    providers: providers,
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class SharedModule {}
import { Injectable, ApplicationRef, ComponentFactoryResolver, ComponentFactory, Injector, ComponentRef } from "@angular/core";
import { Position } from "./position";
import { translateXY } from "../utilities/translate-xy";
export interface IPopoverService {

}


@Injectable()
export class PopoverService implements IPopoverService {
    constructor(
        private _applicationRef: ApplicationRef,
        private _componentFactoryResolver: ComponentFactoryResolver,
        private _injector: Injector,
        private _position: Position
    ) { }

    private _componentFactory: ComponentFactory<any>;
    private _componentRef: ComponentRef<any>;

    public async show(options: { component: any, target: HTMLElement }): Promise<any> {
        const containerElement = document.querySelector('body');
        this._componentFactory = this._componentFactoryResolver.resolveComponentFactory(options.component);
        this._componentRef = this._componentFactory.create(this._injector);
        this._applicationRef.attachView(this._componentRef.hostView);

        this.setInitialCss();
        
        await this._position.bottom({
            component: this._componentRef.location.nativeElement,
            target: options.target,
            space: 0
        });
        
        
        containerElement.appendChild(this.nativeElement);
        setTimeout(() => { this.nativeElement.style.opacity = "100"; }, 100);        
    }

    public hide(): Promise<any> {
        return new Promise((resolve) => {
            this._applicationRef.detachView(this._componentRef.hostView);
            this._componentRef.destroy();
        });
    }

    private setInitialCss() {
        this.nativeElement.setAttribute("style", `-webkit-transition: opacity ${this.transitionDurationInMilliseconds}ms ease-in-out;-o-transition: opacity ${this.transitionDurationInMilliseconds}ms ease-in-out;transition: opacity ${this.transitionDurationInMilliseconds}ms ease-in-out;`);
        this.nativeElement.style.opacity = "0";
        this.nativeElement.style.position = "fixed";
        this.nativeElement.style.top = "0";
        this.nativeElement.style.left = "0";
        this.nativeElement.style.display = "block";
    }

    public transitionDurationInMilliseconds: number;

    public get nativeElement(): HTMLElement {
        return this._componentRef.location.nativeElement;
    }

    public templateHTML: string;
}
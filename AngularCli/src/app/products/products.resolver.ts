import { Injectable }       from '@angular/core';
import {
  Resolve,
  Router,
  NavigationEnd,
  ActivatedRouteSnapshot
}                           from '@angular/router';
import { ProductsSandbox }  from './products.sandbox';

@Injectable()
export class PageResolver implements Resolve<any> {

  private pageSubscription;

  constructor(public productsSandbox: ProductsSandbox, public routes: Router) {}

  /**
   * Triggered when application hits product details route.
   * It subscribes to product list data and finds one with id from the route params.  
   *
   * @param route
   */
  public resolve(route: ActivatedRouteSnapshot) {
    if (this.pageSubscription) return;

    this.pageSubscription = this.productsSandbox.model$.subscribe(model => {
      if (!model || model.currentPage < 0) {        
        return;
      }

      this.productsSandbox.loadSuccess(model);
    });

    this.routes.events.subscribe(args => {
      if (args instanceof NavigationEnd) {
        var routeEnd : string = args.urlAfterRedirects.substring(args.urlAfterRedirects.lastIndexOf('/') + 1);
        var parsed = parseInt(routeEnd);
        var requested : number = isNaN(parsed) ? 0 : parsed;

        if (this.productsSandbox.currentPage !== requested) {
          this.productsSandbox.loadProducts(requested - 1);
        }  
      }            
    });
  }
}
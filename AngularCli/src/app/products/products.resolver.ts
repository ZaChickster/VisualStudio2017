import { Injectable }       from '@angular/core';
import {
  Resolve,
  ActivatedRouteSnapshot
}                           from '@angular/router';
import { ProductsSandbox }  from './products.sandbox';

@Injectable()
export class PageResolver implements Resolve<any> {

  private pageSubscription;

  constructor(public productsSandbox: ProductsSandbox) {}

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
        this.productsSandbox.loadProducts(parseInt(route.params.page));
        return;
      }

      this.productsSandbox.loadSuccess(model);
    });
  }
}
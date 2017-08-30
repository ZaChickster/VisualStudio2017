import { Injectable }             from "@angular/core";
import { Store }      	          from '@ngrx/store';
import { Subscription }           from "rxjs";
import { Sandbox } 			          from '../shared/sandbox/base.sandbox';
import { ProductsApiClient }      from './productsApiClient.service';
import * as store     	          from '../shared/store';
import * as productsActions       from '../shared/store/actions/products.action';
import * as productDetailsActions from '../shared/store/actions/product-details.action';
import {
  Restaurant,
  RestaurantModel,
  User
}                                 from '../shared/models';

@Injectable()
export class ProductsSandbox extends Sandbox {

  public model$                 = this.appState$.select(store.getProductsData);
  public productsLoading$       = this.appState$.select(store.getProductsLoading);
  public productDetails$        = this.appState$.select(store.getProductDetailsData);
  public productDetailsLoading$ = this.appState$.select(store.getProductDetailsLoading);
  public loggedUser$            = this.appState$.select(store.getLoggedUser);

  public instances: Restaurant[] = [];
  public total: number = 0;
  public currentPage: number = 0;
  public pageSize: number = 0;

  private subscriptions: Array<Subscription> = [];

  constructor(
    protected appState$: Store<store.State>,
    private productsApiClient: ProductsApiClient
  ) {
    super(appState$);
    this.registerEvents();
  }

  /**
   * Loads products from the server
   */
  public loadProducts(page: number): void {
    this.appState$.dispatch(new productsActions.LoadAction(page))
  }
  
  /**
   * Dispatches an action to show page
   */
  public loadSuccess(model: RestaurantModel): void {
    this.appState$.dispatch(new productsActions.LoadSuccessAction(model))
  }

  /**
   * Loads product details from the server
   */
  public loadProductDetails(id: number): void {
    this.appState$.dispatch(new productDetailsActions.LoadAction(id))
  }

  /**
   * Dispatches an action to select product details
   */
  public selectProduct(product: Restaurant): void {
    this.appState$.dispatch(new productDetailsActions.LoadSuccessAction(product))
  }

  /**
   * Unsubscribes from events
   */
  public unregisterEvents() {
    this.subscriptions.forEach(sub => sub.unsubscribe());
  }

  /**
   * Subscribes to events
   */
  private registerEvents(): void {
    // Subscribes to culture
    this.subscriptions.push(this.culture$.subscribe((culture: string) => this.culture = culture));

    this.subscriptions.push(this.loggedUser$.subscribe((user: User) => {
      if (user.isLoggedIn) {
        this.loadProducts(0);
      }        
    }));

    this.subscriptions.push(this.model$.subscribe((model: RestaurantModel) => {
      if (model) {
        this.instances = model.pageInstances;
        this.total = model.totalRestaurants;
        this.currentPage = model.currentPage;
        this.pageSize = model.pageSize;
      }        
    }));
  }
}
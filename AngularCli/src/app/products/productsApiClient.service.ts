import { Injectable }       from '@angular/core';
import {
  HttpService,
  GET,
  Path,
  Adapter
}                           from '../shared/asyncServices/http';
import { Observable }       from 'rxjs/Observable';
import { ProductsService }  from './products.service';

@Injectable()
export class ProductsApiClient extends HttpService {

  /**
   * Retrieves all products
   */
  @GET("/Restaurants?page={page}")
  @Adapter(ProductsService.gridAdapter)
  public getProducts(@Path("page") page: number): Observable<any> { return null; };

  /**
   * Retrieves product details by a given id
   * 
   * @param id
   */
  @GET("/Restaurant?id={restaurant_id}")
  @Adapter(ProductsService.productDetailsAdapter)
  public getProductDetails(@Path("restaurant_id") restaurant_id: number): Observable<any> { return null; };
}
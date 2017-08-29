import {
  Injectable,
  Inject,
  forwardRef
}                                       from '@angular/core';
import { Restaurant, RestaurantModel }  from '../shared/models';

@Injectable()
export class ProductsService {

  /**
   * Transforms grid data products recieved from the API into array of 'Product' instances
   *
   * @param products
   */
  static gridAdapter(model: any): RestaurantModel {
    return new RestaurantModel(model);
  }

  /**
   * Transforms product details recieved from the API into instance of 'Product'
   *
   * @param product
   */
  static productDetailsAdapter(product: any): Restaurant {
    return new Restaurant(product);
  }
}
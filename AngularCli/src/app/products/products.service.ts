import {
  Injectable,
  Inject,
  forwardRef
}                           from '@angular/core';
import { Restaurant }          from '../shared/models';
import { ProductsSandbox }  from './products.sandbox';

@Injectable()
export class ProductsService {

  private productsSubscription;

  /**
   * Transforms grid data products recieved from the API into array of 'Product' instances
   *
   * @param products
   */
  static gridAdapter(products: any): Array<Restaurant> {
    return products.map(product => new Restaurant(product));
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
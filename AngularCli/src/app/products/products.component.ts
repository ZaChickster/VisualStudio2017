import {
  Component,
  ChangeDetectionStrategy
}                           from '@angular/core';
import { Router }           from '@angular/router';
import { Subscription }     from "rxjs";
import { ProductsSandbox }  from './products.sandbox';
import { Restaurant }       from '../shared/models';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProductsComponent {

  constructor(
    private router: Router,
    public productsSandbox: ProductsSandbox
  ) {}

  /**
   * Callback function for grid select event
   * 
   * @param selected
   */
  public onSelect({ selected }): void {
    this.productsSandbox.selectProduct(selected[0]);
    this.router.navigate(['/product', selected[0].restaurant_id]);
  }

    /**
   * Callback function for grid select event
   * 
   * @param pageInfo
   */
  public onPage({ offset }): void {
    this.productsSandbox.loadProducts(offset)
    this.router.navigate(['/products', offset + 1]);
  }  
}

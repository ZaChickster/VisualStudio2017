import {
  Component,
  OnInit,
  OnDestroy,
  ChangeDetectionStrategy,
  ChangeDetectorRef
}                           from '@angular/core';
import { ActivatedRoute }   from '@angular/router';
import { Subscription }     from "rxjs";
import { ProductsSandbox }  from './products.sandbox';
import { Product }          from '../shared/models';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProductDetailsComponent implements OnInit, OnDestroy {

  public product:        Product;
  private subscriptions: Array<Subscription> = [];

  constructor(
    public productsSandbox: ProductsSandbox,
    private changeDetector: ChangeDetectorRef,
    private activatedRoute: ActivatedRoute) {}

  ngOnInit() {
    this.registerEvents();
  }

  ngOnDestroy() {
    this.subscriptions.forEach(sub => sub.unsubscribe());
  }

  /**
   * Registers events
   */
  private registerEvents(): void {
    // Subscribes to product details
    this.subscriptions.push(this.productsSandbox.productDetails$.subscribe((product: any) => {
      if (product) {
        this.changeDetector.markForCheck();
        this.product = product;
      }
    }));
  }
}

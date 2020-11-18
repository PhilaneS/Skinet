import { IProduct } from 'src/app/shared/models/product';
import { Component, OnInit } from '@angular/core';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
product: IProduct;
  constructor(private shopService: ShopService, private actovatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.loadProduct();
  }
  loadProduct(){
    this.shopService.getProduct(+this.actovatedRoute.snapshot.paramMap.get('id')).subscribe(response => {
      console.log(response);
      this.product = response;
    }, error => {
      console.log(error);
    });
  }

}

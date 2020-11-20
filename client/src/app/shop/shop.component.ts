import { IProductType } from './../shared/models/productType';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IBrand } from '../shared/models/Brand';
import { IProduct } from '../shared/models/product';
import { ShopService } from './shop.service';
import { ShopParams } from '../shared/models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
@ViewChild('search', { static: false }) searchTerm: ElementRef;
products: IProduct[];
brands: IBrand[];
types: IProductType[];
shopParams = new ShopParams();
totalCount: number;
sortOptions = [
  { name: 'Alphabetical', value: 'name'},
  { name: 'Price: Low to High', value: 'priceAsc'},
  { name: 'High to Low', value: 'priceDesc'},

];
  constructor(private shopService: ShopService) { }

  ngOnInit() {
    this.getProduts();
    this.getBrands();
    this.getProductTypes();
  }
  getProduts(){
    this.shopService.getProducts(this.shopParams).subscribe(response =>
      {
        this.products = response.data;
        this.shopParams.pageNumber = response.pageIndex;
        this.shopParams.pageSize = response.pageSize;
        this.totalCount = response.count;
      }, error => {
        console.log(error);
      });
  }
  getBrands(){
    this.shopService.getBrands().subscribe(response => {
      this.brands = [{id: 0, name: 'All'}, ...response];
    }, error => {
      console.log(error);
    });
  }
  getProductTypes(){
    this.shopService.getProductTypes().subscribe(response => {
      this.types = [{id: 0, name: 'All'}, ...response];
    }, error => {
      console.log(error);
    });
  }
  onBrandSelected(brandId: number){
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber = 1;
    this.getProduts();
  }
  onProductTypeSelected(typeId: number){
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber = 1;
    this.getProduts();
  }
  onSortSelected(sort: string){
    this.shopParams.sort = sort;
    this.getProduts();
  }
  onPageChanged(event: any){
    if (this.shopParams.pageNumber !== event){
      this.shopParams.pageNumber = event;
      this.getProduts();
    }
  }
  onSearch(){
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getProduts();
  }
  onReset(){
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProduts();
  }
}

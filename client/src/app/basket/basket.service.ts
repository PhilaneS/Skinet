import { Basket, IBasket, IBasketItem, IBasketTotals } from './../shared/models/basket';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { IProduct } from '../shared/models/product';
import { IDeliveryMethod } from '../shared/models/DeliveryMethod';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
baseUrl = environment.apiUrl;
private basketSource = new BehaviorSubject<IBasket>(null);
private basketTotalsSource = new BehaviorSubject<IBasketTotals>(null);
basket$ = this.basketSource.asObservable();
basketTotals$ = this.basketTotalsSource.asObservable();
shipping = 0;
  constructor(private http: HttpClient) { }

  setShippingPrice(deliveryMethod: IDeliveryMethod ) {
    this.shipping = deliveryMethod.price;
    this.CalculateTotals();
  }
  getBasket(id: string)
  {
    return this.http.get(this.baseUrl + 'basket?id=' + id)
    .pipe(
      map((basket: IBasket) => {
        this.basketSource.next(basket);
        this.CalculateTotals();
      })
    );
  }

  setBasket(basket: IBasket){
    return this.http.post(this.baseUrl + 'basket', basket).subscribe((response: IBasket) => {
      this.basketSource.next(response);
      this.CalculateTotals();
    }, error => {
      console.log(error);
    });
  }
  getCurrentBasketValue(){
    return this.basketSource.value;
  }
addItemToBasket(item: IProduct, quantity = 1){
  const itemToAdd: IBasketItem = this.mapProdctItemToBasketItem(item, quantity);
  const basket = this.getCurrentBasketValue() ?? this.createBasket();
  basket.items = this.AddOrUpdateItem(basket.items, itemToAdd, quantity);
  this.setBasket(basket);
}

incrementQuantity(item: IBasketItem){
  const basket = this.getCurrentBasketValue();
  const foundIndex = basket.items.findIndex(x => x.id === item.id);
  basket.items[foundIndex].quantity++;
  this.setBasket(basket);
}
decrementQuantity(item: IBasketItem){
  const basket = this.getCurrentBasketValue();
  const foundIndex = basket.items.findIndex(x => x.id === item.id);
  if (basket.items[foundIndex].quantity > 1) {
    basket.items[foundIndex].quantity--;
    this.setBasket(basket);
  }
  else{
    this.removeItemFromBasket(item);
  }
}
removeItemFromBasket(item: IBasketItem) {
  const basket = this.getCurrentBasketValue();
  if (basket.items.some(x => x.id === item.id)){
    basket.items = basket.items.filter(x => x.id !== item.id);

    if (basket.items.length > 0){
      this.setBasket(basket);
    }else {
      this.deleteBasket(item);
    }
  }
  }
  deleteLocalBasket(id: string) {
    this.basketSource.next(null);
    this.basketTotalsSource.next(null);
    localStorage.removeItem('basket_id');
  }
  deleteBasket(item: IBasketItem) {
    return this.http.delete(this.baseUrl + 'basket?id=' + item.id).subscribe(() => {
      this.basketSource.next(null);
      this.basketTotalsSource.next(null);
      localStorage.removeItem('basket_id');
    }, error => {
      console.log(error);
    });

  }

private CalculateTotals(){
  const basket = this.getCurrentBasketValue();
  const shipping = this.shipping;
  const subtotal = basket.items.reduce((a, b) => (b.price * b.quantity) + a, 0);
  const total = subtotal + shipping;
  this.basketTotalsSource.next({shipping, total, subtotal });
}
 private AddOrUpdateItem(items: IBasketItem[], itemToAdd: IBasketItem, quantity: number): IBasketItem[] {
    const index = items.findIndex(i => i.id === itemToAdd.id);
    if (index === -1) {
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    } else {
      items[index].quantity += quantity;
    }
    return items;
  }
 private  createBasket(): IBasket {
    const basket = new Basket();
    localStorage.setItem('basket_id', basket.id);
    return basket;
  }
 private mapProdctItemToBasketItem(item: IProduct, quantity: number): IBasketItem {
    return {
      id: item.id,
      productName: item.name,
      price: item.price,
      pictureUrl: item.pictureUrl,
      quantity,
      brand: item.productBrand,
      type: item.productType
    };
  }
}

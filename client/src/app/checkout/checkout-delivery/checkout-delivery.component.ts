import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { CheckoutService } from 'src/app/core/services/checkout.service';
import { IDeliveryMethod } from 'src/app/shared/models/DeliveryMethod';

@Component({
  selector: 'app-checkout-delivery',
  templateUrl: './checkout-delivery.component.html',
  styleUrls: ['./checkout-delivery.component.scss']
})
export class CheckoutDeliveryComponent implements OnInit {
@Input() checkoutForm: FormGroup;
deliveryMethods : IDeliveryMethod[];

  constructor(private checkoutService: CheckoutService) { }

  ngOnInit(): void {
    this.checkoutService.getDeliveryMethods().subscribe((dm : IDeliveryMethod[]) =>{
      this.deliveryMethods = dm;
    }

    );
  }

}

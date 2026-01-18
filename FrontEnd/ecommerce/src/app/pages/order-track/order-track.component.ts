import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-order-track',  
  templateUrl: './order-track.component.html',
  styleUrl: './order-track.component.css'
})
export class OrderTrackComponent implements OnInit {

  order: any;
  steps = [
    'PLACED',
    'CONFIRMED',
    'SHIPPED',
    'OUT_FOR_DELIVERY',
    'DELIVERED'
  ];

  currentStep = 0;

  constructor(
    private route: ActivatedRoute,
    private orderService: OrderService
  ) {}

  ngOnInit() {
    const orderId = this.route.snapshot.paramMap.get('id')!;
    this.orderService.getOrderById(orderId).subscribe(res => {
      this.order = res;
      this.currentStep = this.steps.indexOf(res.status);
    });
  }
}

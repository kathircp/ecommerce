import { Component, Input, OnDestroy, OnInit } from '@angular/core';

@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.css']
})
export class CarouselComponent implements OnInit, OnDestroy {
  @Input() images: string[] = [];
  @Input() interval = 4000; // ms

  current = 0;
  private timer: any;

  ngOnInit(): void {
    if ((this.images || []).length > 1) {
      this.start();
    }
  }

  ngOnDestroy(): void {
    this.stop();
  }

  start() {
    this.stop();
    this.timer = setInterval(() => this.next(), this.interval);
  }

  stop() {
    if (this.timer) {
      clearInterval(this.timer);
      this.timer = null;
    }
  }

  next() {
    if (!this.images || this.images.length === 0) return;
    this.current = (this.current + 1) % this.images.length;
  }

  prev() {
    if (!this.images || this.images.length === 0) return;
    this.current = (this.current - 1 + this.images.length) % this.images.length;
  }

  goTo(index: number) {
    if (!this.images || index < 0 || index >= this.images.length) return;
    this.current = index;
  }
}

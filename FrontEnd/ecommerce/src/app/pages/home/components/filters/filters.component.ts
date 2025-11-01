import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-filters',
  templateUrl: './filters.component.html',
  styleUrls: ['./filters.component.css']
})
export class FiltersComponent implements OnInit {
  categories = ['shoes', 'sports'];

  @Output() showCategory = new EventEmitter<string>();
  @Output() close = new EventEmitter<void>();

  constructor() { }

  ngOnInit(): void {
  }

  onShowCategory(category: string): void {
    this.showCategory.emit(category);
  }

  onClose(): void{
    this.close.emit();
  }

}

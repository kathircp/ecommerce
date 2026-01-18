import { Component, EventEmitter, OnInit, Output, Input } from '@angular/core';

@Component({
  selector: 'app-filters',
  templateUrl: './filters.component.html',
  styleUrls: ['./filters.component.css']
})
export class FiltersComponent implements OnInit {
  colorFilter = ['Red', 'Green', 'Blue'];
  discountFilter = ["10%", "20%"];
  blouseFilter = ["include","Exclude"];
  rangeFilter = ["0-1000", "1000-2000", "2000-3000"];

  showFilterPanel: boolean = true;
  
  @Input() selectedCategory: any;
  @Output() showCategory = new EventEmitter<string>(); 
  @Output() filterApplied = new EventEmitter<any>();
  @Output() closeFilter = new EventEmitter<void>();

  constructor() { }

  ngOnInit(): void {
    // console.log(1222,this.showCategory);
    // console.log(1333,this.showFilterPanel);
    // console.log(23333, this.selectedCategory)
  }

  onShowCategory(category: string): void {    
    console.log('Category selected:', category);
    this.showCategory.emit(category);
  }

  onClose(): void{
    this.closeFilter.emit();
  }
  // toggleFilterPanel(): void {
  //   this.showFilterPanel = !this.showFilterPanel; // Toggles the boolean value
  // }
}